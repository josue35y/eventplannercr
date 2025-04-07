using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EventPlannerCR_backend.Entidades;
using EventPlannerCR_AccesoDatos;

namespace EventPlannerCR_backend.Logica
{
    public class Utilitarios_josue
    {


        public ResLogin_josue login(ReqLogin_josue req)
        {
            ResLogin_josue res = new ResLogin_josue();
            res.error = new List<Error>();






            if (res.error.Count == 0) // Procede solo si no hay errores previos
            {

                int? idReturn = 0;
                int? idErrorId = 0;
                string errorBD = "";
                bool UsuarioValido = this.ConfirmarUsuarioNuevo(req.Usuario); 
                Guid guid = Guid.NewGuid();
                String token = guid.ToString();

                if (UsuarioValido)
                {

                    //using (ConexionLinqDataContext ConexionProyecto = new ConexionLinqDataContext())
                    //{
                    //    ConexionProyecto.SP_InsertarUsuario_josue(


                    //            req.usuario.Nombre,
                    //            req.usuario.Apellidos,
                    //            req.usuario.Telefono,
                    //            req.usuario.Correo,
                    //            req.usuario.FechaNacimiento,
                    //            req.usuario.Provincia,
                    //            req.usuario.Canton,
                    //            req.usuario.Distrito,
                    //            req.usuario.Admin,
                    //            req.usuario.Password,
                    //            req.usuario.Vehiculo,
                    //            guid.ToString(),
                    //            estado,
                    //            ref idBd,
                    //            ref idError,
                    //            ref errorDescripcion);
                    //}
                }
                if (idReturn > 0)
                {
                    String url = "https://localhost:44316/Administrador/ValidarCuenta?token=" + token;



                    if (EnviarCorreos(req.Usuario, url))
                    {
                        res.resultado = true;
                    }
                    else
                    {
                        res.resultado = false;
                        res.error.Add(Error.generarError(enumErrores.errorConversion, "No se envío el correo"));
                    }
                }
                else
                {

                    res.resultado = false;
                    res.error.Add(Error.generarError(enumErrores.excepcionLogica, errorBD));
                }
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
                    // Registrar el error o implementar lógica de reintento según sea necesario
                    error = Error.generarError(enumErrores.correoNoEnviado, $"Error al enviar el correo: {ex.Message}");
                    return false;
                }
                catch (Exception ex)
                {
                    error = Error.generarError(enumErrores.excepcionLogica, $"Ocurrió un error inesperado: {ex.Message}");
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
            bool? EsEstudiante = false;
            bool? EsProfesor = false;
            int? IDReturn = 0;
            int? IDError = 0;
            String ErrorDescripcion = "";

            //using (ConexionLinqDataContext ConexionProyecto = new ConexionLinqDataContext())
            //{
            //    ConexionProyecto.SP_VERIFICAR_ESTUDIANTE_NUEVO(
            //        ref Usuario, 
            //        ref EsEstudiante,
            //        ref IDReturn, 
            //        ref IDError, 
            //        ref ErrorDescripcion);
            //}
            if (EsEstudiante.Value == true || EsProfesor.Value == true)
            {
                Confirmacion = false;
            }
            return Confirmacion;
        }


    }
}
