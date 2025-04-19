using EventPlannerCR_AccesoDatos;
using EventPlannerCR_backend.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace EventPlannerCR_backend.Logica
{
    public class LogUtilitarios
    {
        public ResLogin Login(ReqLogin req)
        {
            ResLogin res = new ResLogin();
            res.Error = new List<Error>();

            if (!string.IsNullOrEmpty(req.Usuario.Correo) && !string.IsNullOrEmpty(req.Usuario.Password))
            {
                int? UsuarioDB = 0;
                string nombreBD = "";
                string apellidosBD = "";
                bool? admin = false;
                int? idError = 0;
                string errorDescripcion = "";

                using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                {
                    linq.SP_LoginUsuario(
                        req.Usuario.Correo,
                        req.Usuario.Password,
                        ref UsuarioDB,
                        ref nombreBD,
                        ref apellidosBD,
                        ref admin,
                        ref idError,
                        ref errorDescripcion);
                }

                if (UsuarioDB > 0)
                {
                    res.Usuario = new Usuario
                    {
                        IdUsuario = UsuarioDB.Value,
                        Nombre = nombreBD,
                        Apellidos = apellidosBD,
                        Correo = req.Usuario.Correo,
                        Password = req.Usuario.Password,
                        Admin = admin ?? false
                    };

                    res.Resultado = true;
                    
                    using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                    {
                        int? idBd = 0;
                        LogFactorias Factorias = new LogFactorias();
                        List<SP_Buscar_UsuarioResult> tc = linq.SP_Buscar_Usuario(
                            res.Usuario.IdUsuario,
                            res.Usuario.Correo,
                            res.Usuario.Nombre,
                            res.Usuario.Apellidos,
                            res.Usuario.Admin,
                            ref idBd,
                            ref idError,
                            ref errorDescripcion).ToList();

                        if (tc.Any())
                        {
                            res.Usuario = Factorias.BuscarUsuario(tc).FirstOrDefault();
                        }
                    }
                }
                else
                {
                    res.Resultado = false;
                    res.Error.Add(Error.generarError(enumErrores.LoginIncorrecto, errorDescripcion));
                }
            }
            else
            {
                res.Resultado = false;
                res.Error.Add(Error.generarError(enumErrores.LoginIncorrecto, "Faltan valores para iniciar sesión."));
            }

            return res;
        }

        public ResRegistrarUsuario RegistrarUsuario(ReqRegistrarUsuario req)
        {
            //Se inicializan el response y la logica de crud de usuario
            ResRegistrarUsuario res = new ResRegistrarUsuario();
            LogUsuario logUsuario = new LogUsuario();

            //Se inserta el usuario en la base de datos.
            logUsuario.InsertarUsuario(new ReqInsertarUsuario
            { 
                Usuario = req.Usuario 
            });

            // Aquí se busca el usuario recién insertado y se instancia como "UsuarioAConfirmar" 
            ResBuscarUsuario ResBuscarUsuario = logUsuario.BuscarUsuario(new ReqBuscarUsuario
            {
                Usuario = new Usuario
                {
                    Correo = req.Usuario.Correo
                }
            });
            ResBuscarUsuario.ListaUsuarios = ResBuscarUsuario.ListaUsuarios.Where(x => x.Correo == req.Usuario.Correo).ToList();
            Usuario UsuarioAConfirmar = new Usuario();
            foreach (var item in ResBuscarUsuario.ListaUsuarios)
            {
                if (item.Correo == req.Usuario.Correo)
                {
                    UsuarioAConfirmar = item;
                }
                else
                {
                    res.Resultado = false;
                    res.Error.Add(Error.generarError(enumErrores.excepcionBaseDatos, "Error al buscar el usuario."));
                }
            }

            //Se genera el código de verificación y se asigna al usuario "UsuarioAConfirmar "
            var random = new Random();
            var code = random.Next(1000, 9999).ToString();
            UsuarioAConfirmar.Cod_Ver_Cor = code;

            //El codigo se guarda en la base de datos.
            ResActualizarUsuario resActualizarUsuario = logUsuario.ActualizarUsuario(new ReqActualizarUsuario
            {
                Usuario = UsuarioAConfirmar
            });

            //Se genera la URL de verificación.
            string url = $"https://localhost:44373/api/utilitarios/ConfirmarUsuario?cod={code}&correo={UsuarioAConfirmar.Correo}";

            //Se envía el correo de verificación.
            if (!EnviarCorreos(UsuarioAConfirmar.Correo, url))
            {
                res.Resultado = false;
                res.Error.Add(Error.generarError(enumErrores.correoNoEnviado, "Error al enviar el correo de verificación."));
                return res;
            }
            else
            {
                res.Resultado = true;
            }

            //ConfirmarUsuarioNuevo(UsuarioAConfirmar.Correo);
            res.Usuario = UsuarioAConfirmar;

            return res;
        }

        public ResVerificarCuenta VerificarCuenta(ReqVerificarCuenta req)
        {

            ResVerificarCuenta res = new ResVerificarCuenta();
            res.Error = new List<Error>();

            try
            {
                //Validación del request 
                if (req == null)
                {
                    res.Resultado = false;
                    res.Error.Add(Error.generarError(enumErrores.requestNulo, "Req Null"));
                }

                int? idError = 0;
                string errorDescripcion = null;

                //Se busca el Usuario en la base de datos
                using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                {
                    List<SP_VerificarCuentaResult> tc = linq.SP_VerificarCuenta(
                        req.Correo,
                        req.Codigo,
                        ref idError,
                        ref errorDescripcion).ToList();

                    LogFactorias Factorias = new LogFactorias();
                    List<Usuario> ListaUsuarios = new List<Usuario>();
                    ListaUsuarios = FactoryVerificarCuenta(tc);

                    foreach (var item in ListaUsuarios)
                    {
                        if (item.Correo == req.Correo)
                        {
                            if (ConfirmarUsuarioNuevo(req.Correo))
                            {
                                res.Resultado = true;
                            }
                        }
                        else
                        {
                            res.Resultado = false;
                            res.Error.Add(Error.generarError(enumErrores.excepcionLogica, "Excepcion en logica"));
                        }
                    }
                    res.Resultado = true;
                }
            }
            catch (Exception ex)
            {
                res.Error.Add(Error.generarError(enumErrores.excepcionBaseDatos, "Excepcion en Logica"));
            }
            return res;
        }

        public static bool EnviarCorreos(string destinatario, string url)
        {

            Error error = new Error();

            // Validaciones previas
            if (string.IsNullOrWhiteSpace(destinatario))
            {
                error = Error.generarError(enumErrores.correoIncorrecto, "El destinatario no puede estar vacío.");
            }
            if (string.IsNullOrWhiteSpace(url))
            {
                error = Error.generarError(enumErrores.urlInvlaida, "La URL no puede estar vacía.");
            }

            #region credenciales
            const string remitente = "josue35@gmail.com";
            const string password = "anvn ufwc rmhp onqy";
            #endregion

            // Configuración del mensaje
            var mail = new MailMessage
            {
                From = new MailAddress(remitente),
                Subject = "Por favor, verifique su cuenta",
                Body = GenerarCuerpoCorreo(url),
                IsBodyHtml = true
            };
            mail.To.Add(destinatario);

            // Configuración del cliente SMTP
            using (var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(remitente, password),
                EnableSsl = true
            })
            {
                try
                {
                    smtp.Send(mail);
                    return true;
                }
                catch (SmtpException ex)
                {
                    // Manejo de excepciones específicas del envío de correo
                    // Registrar el Error o implementar lógica de reintento según sea necesario
                    error = Error.generarError(enumErrores.correoNoEnviado, $"Error al enviar el correo: {ex.Message}");
                    return false;
                }
                catch (Exception ex)
                {
                    error = Error.generarError(enumErrores.excepcionLogica, $"Ocurrió un Error inesperado: {ex.Message}");
                }
            }
            return true;
        }
        private static string GenerarCuerpoCorreo(string url)
        {
            return $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='utf-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            line-height: 1.6;
                            color: #333333;
                            max-width: 600px;
                            margin: 0 auto;
                            padding: 20px;
                        }}
                        .container {{
                            background-color: #f8f9fa;
                            border-radius: 8px;
                            padding: 30px;
                            margin-top: 20px;
                            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
                        }}
                        .header {{
                            text-align: center;
                            padding-bottom: 20px;
                            border-bottom: 2px solid #e9ecef;
                            margin-bottom: 20px;
                        }}
                        .header h2 {{
                            color: #2c3e50;
                            margin: 0;
                        }}
                        .button {{
                            display: inline-block;
                            background-color: #007bff;
                            color: white !important;
                            padding: 12px 25px;
                            text-decoration: none;
                            border-radius: 5px;
                            margin: 20px 0;
                            font-weight: bold;
                        }}
                        .button:hover {{
                            background-color: #0056b3;
                        }}
                        .footer {{
                            margin-top: 30px;
                            font-size: 12px;
                            color: #666666;
                            text-align: center;
                            border-top: 1px solid #e9ecef;
                            padding-top: 20px;
                        }}
                        .url-text {{
                            background-color: #f1f3f4;
                            padding: 10px;
                            border-radius: 4px;
                            word-break: break-all;
                            font-size: 14px;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <h2>Verificación de Cuenta</h2>
                        </div>
                        <p>Usuario,</p>
                        <p>Por favor, para activar su cuenta, haga clic en el siguiente botón:</p>
                        <div style='text-align: center;'>
                            <a href='{url}' class='button'>Verificar</a>
                        </div>
                        <p>Si el botón no funciona, copie y pegue el siguiente enlace en su navegador:</p>
                        <p class='url-text'>{url}</p>
                        <div class='footer'>
                            <p>Este es un correo automático, por favor no responda.</p>
                            <p>Si no solicitó esta verificación, puede ignorar este mensaje.</p>
                            <p>© {DateTime.Now.Year}</p>
                        </div>
                    </div>
                </body>
                </html>";
        }
        public bool ConfirmarUsuarioNuevo(String Usuario)
        {
            bool Confirmacion = true;
            LogUsuario logUsuario = new LogUsuario();
            ReqBuscarUsuario ReqBuscarUsuario = new ReqBuscarUsuario
            {
                Usuario = new Usuario
                {
                    Correo = Usuario
                }
            };
            ResBuscarUsuario ResBuscarUsuario = logUsuario.BuscarUsuario(ReqBuscarUsuario);
            ResBuscarUsuario.ListaUsuarios = ResBuscarUsuario.ListaUsuarios.Where(x => x.Correo == Usuario).ToList();
            Usuario UsuarioAConfirmar = new Usuario();
            foreach (var item in ResBuscarUsuario.ListaUsuarios)
            {
                UsuarioAConfirmar = item;
            }

            ReqActualizarUsuario ReqActualizarUsuario = new ReqActualizarUsuario
            {
                Usuario = UsuarioAConfirmar

            };
            ResActualizarUsuario resActualizarUsuario = logUsuario.ActualizarUsuario(ReqActualizarUsuario);



            return Confirmacion;
        }
        public static Usuario UsuarioFactory(SP_VerificarCuentaResult data)
        {
            return new Usuario
            {
                IdUsuario = data.ID_USUARIO,
                Nombre = data.NOMBRE,
                Apellidos = data.APELLIDOS,
                Telefono = data.TELEFONO,
                Telefono_Verificado = data.TELEFONO_VERIFICADO,
                Cod_Ver_Tel = data.COD_VER_TEL,
                Correo = data.CORREO,
                Correo_Verificado = data.CORREO_VERIFICADO,
                FechaNacimiento = data.FECHANACIMIENTO,
                Admin = data.ADMIN,
                Password = data.PASSWORD,
                FechaRegistro = data.FECHAREGISTRO,
                Vehiculo = data.VEHICULO
            };
        }
        public List<Usuario> FactoryVerificarCuenta(List<SP_VerificarCuentaResult> tc)
        {
            List<Usuario> ListaUsuarios = new List<Usuario>();

            foreach (var item in tc)
            {
                // Crear un nuevo objeto para cada elemento
                Usuario usuario = new Usuario();

                // Asignar propiedades
                usuario.Nombre = item.NOMBRE;
                usuario.Apellidos = item.APELLIDOS;
                usuario.Correo = item.CORREO;
                usuario.FechaRegistro = item.FECHAREGISTRO;
                usuario.Telefono = item.TELEFONO;
                usuario.FechaNacimiento = item.FECHANACIMIENTO;
                usuario.Vehiculo = (bool)item.VEHICULO;

                // Añadir a la lista
                ListaUsuarios.Add(usuario);
            }

            return ListaUsuarios;
        }
    }
}
