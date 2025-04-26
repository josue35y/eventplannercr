using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventPlannerCR_backend.Entidades;
using EventPlannerCR_AccesoDatos;
using System.Text.RegularExpressions;
using System.IO;
using System.Security.Cryptography;

namespace EventPlannerCR_backend.Logica
{
    public class LogUsuario
    {
        //Metodo para insertar Usuario
        public ResInsertarUsuario InsertarUsuario(ReqInsertarUsuario req)
        {
            //Creacion de instancias generales del método
            ResInsertarUsuario res = new ResInsertarUsuario();
            res.Error = new List<Error>();

            //inicio de manejo de excepciones
            try
            {
                //validación del request
                if (req == null)
                {
                    Error Error = new Error();

                    Error.ErrorCode = enumErrores.requestNulo;
                    Error.Message = "Req Null";
                    res.Error.Add(Error);
                }

                //Validación del nombre del nuevo Usuario para evitar nulos o espacio en blanco
                if (String.IsNullOrEmpty(req.Usuario.Nombre))
                {
                    Error Error = new Error();

                    //Acumula la respuesta de Error
                    Error.ErrorCode = enumErrores.nombreFaltante;
                    Error.Message = "Nombre nulo o no válido";
                    res.Error.Add(Error);
                }
                else
                {
                    //Validación de caracteres especiales en el nombre
                    String patron = @"^[\p{L}\s'-]+$";
                    bool match = System.Text.RegularExpressions.Regex.IsMatch(req.Usuario.Nombre, patron);
                    //Si no cumple con el patrón, se acumula el Error
                    if (match == false)
                    {
                        Error Error = new Error();

                        Error.ErrorCode = enumErrores.NombreInvalido;
                        Error.Message = "Hay un carácter no válido en el nombre";
                        res.Error.Add(Error);
                    }
                }

                //Validación del apellido del nuevo Usuario para evitar nulos o espacio en blanco
                if (String.IsNullOrEmpty(req.Usuario.Apellidos))
                {
                    Error Error = new Error();

                    //Acumula la respuesta de Error
                    Error.ErrorCode = enumErrores.apellidoFaltante;
                    Error.Message = "Apellidos nulo o no válido";
                    res.Error.Add(Error);
                }
                else
                {
                    //Validación de caracteres especiales en el apellido
                    String patron = @"^[\p{L}\s'-]+$";
                    bool match = System.Text.RegularExpressions.Regex.IsMatch(req.Usuario.Apellidos, patron);


                    //Si no cumple con el patrón, se acumula el Error
                    if (match == false)
                    {
                        Error Error = new Error();

                        Error.ErrorCode = enumErrores.ApellidoInvalido;
                        Error.Message = "Hay un carácter no válido en el apellido";
                        res.Error.Add(Error);
                    }
                }

                //Validación del correo del nuevo Usuario para evitar nulos o espacio en blanco
                if (String.IsNullOrEmpty(req.Usuario.Correo))
                {
                    Error Error = new Error();

                    //Acumula la respuesta de Error
                    Error.ErrorCode = enumErrores.correoFaltante;
                    Error.Message = "Correo nulo o no válido";
                    res.Error.Add(Error);
                }

                //Validación de formato de correo
                else
                {
                    //Validacion del formato de correo
                    String patron = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                    bool match = System.Text.RegularExpressions.Regex.IsMatch(req.Usuario.Correo, patron);

                    //Si no cumple con el patrón, se acumula el Error
                    if (match == false)
                    {
                        Error Error = new Error();

                        Error.ErrorCode = enumErrores.CorreoInvalido;
                        Error.Message = "Formato de correo no válido";
                        res.Error.Add(Error);
                    }
                }

                //Validación de la contraseña del nuevo Usuario para evitar nulos o espacio en blanco
                if (String.IsNullOrEmpty(req.Usuario.Password))
                {
                    Error Error = new Error();

                    Error.ErrorCode = enumErrores.passwordFaltante;
                    Error.Message = "Password nulo o no válido";
                    res.Error.Add(Error);
                }
                else
                {
                    //Validacion de la contraseña
                    String patron = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+\-=\[\]{};':""\\/|,.<>/?])(?=.{8,}).*$";
                    bool match = System.Text.RegularExpressions.Regex.IsMatch(req.Usuario.Password, patron);
                    //Si no cumple con el patrón, se acumula el Error
                    if (match == false)
                    {
                        Error Error = new Error();

                        Error.ErrorCode = enumErrores.passwordInvalido;
                        Error.Message = "La contraseña no cumple con alguna de las siguientes características: \n" +
                                        "1- Mínimo 8 caracteres" + "2- Al menos un simbolo 3- Al menos una mayúscula " +
                                        "4-Al menos una minúscula";
                        res.Error.Add(Error);
                    }
                }

                //Validación de la fecha de nacimiento del nuevo Usuario para evitar nulos
                if (req.Usuario.FechaNacimiento == null || req.Usuario.FechaNacimiento.Date == default)
                {
                    Error Error = new Error();

                    Error.ErrorCode = enumErrores.FechaNacimientoFaltante;
                    Error.Message = "Fecha nula";
                    res.Error.Add(Error);
                }

                //Se valida si hubo errores en todas las validaciones
                if (res.Error.Any())
                {
                    res.Resultado = false;
                }
                //Si no hubo errores se agrega el Usuario a la base de datos
                else
                {
                    string Codigo = "0000";
                    int? idBd = 0;
                    int? idError = 0;
                    string errorDescripcion = null;

                    using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                    {
                        linq.SP_InsertarUsuario(req.Usuario.Nombre, req.Usuario.Apellidos,
                            req.Usuario.Correo,"",req.Usuario.FechaNacimiento, req.Usuario.Password,
                            ref idBd, ref idError, ref errorDescripcion);
                    }

                    if (idBd > 0)
                    {
                        res.Resultado = true;
                    }
                    else
                    {
                        res.Error.Add(Error.generarError(enumErrores.excepcionBaseDatos, "Error en base de datos."));
                    }
                }
            }

            //Manejo de excepciones
            catch (Exception ex)
            {
                Error Error = new Error();

                Error.ErrorCode = enumErrores.excepcionLogica;
                Error.Message = ex.ToString();
                res.Error.Add(Error);
            }

            //Retorno de la respuesta
            return res;
        }

        //Método para buscar un Usuario
        public ResBuscarUsuario BuscarUsuario(ReqBuscarUsuario req)
        {
            ResBuscarUsuario res = new ResBuscarUsuario();
            res.Error = new List<Error>();
            res.ListaUsuarios = new List<Usuario>();

            try
            {
                //Validación del request 
                if (req == null)
                {
                    Error Error = new Error();
                    Error.ErrorCode = enumErrores.requestNulo;
                    Error.Message = "Req Null";
                    res.Error.Add(Error);
                }

                if (!String.IsNullOrEmpty(req.Usuario.Nombre))
                {
                    req.Usuario.Nombre = req.Usuario.Nombre.ToLower();
                    //Validación de caracteres especiales en el nombre
                    String patron = "^[a-zA-Z0-9áéíóúÁÉÍÓÚüÜñÑ]+$";
                    bool match = System.Text.RegularExpressions.Regex.IsMatch(req.Usuario.Nombre, patron);
                    //Si no cumple con el patrón, se acumula el Error
                    if (!match)
                    {
                        Error Error = new Error();

                        Error.ErrorCode = enumErrores.NombreInvalido;
                        Error.Message = "Hay un carácter no válido en el nombre";
                        res.Error.Add(Error);
                    }
                }

                if (!String.IsNullOrEmpty(req.Usuario.Apellidos))
                {
                    req.Usuario.Apellidos = req.Usuario.Apellidos.ToLower();
                    //Validación de caracteres especiales en el nombre
                    String patron = "^[a-zA-Z0-9áéíóúÁÉÍÓÚüÜñÑ]+$";
                    bool match = System.Text.RegularExpressions.Regex.IsMatch(req.Usuario.Apellidos, patron);
                    //Si no cumple con el patrón, se acumula el Error

                    if (!match)
                    {
                        Error Error = new Error();

                        Error.ErrorCode = enumErrores.ApellidoInvalido;
                        Error.Message = "Hay un carácter no válido en el apellido";
                        res.Error.Add(Error);
                    }
                }

                if (res.Error.Any())
                {
                    res.Resultado = false;
                    return res;
                }

                int? idBd = 0;
                int? idError = 0;
                string errorDescripcion = null;

                //Se busca el Usuario en la base de datos
                using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                {
                    LogFactorias Factorias = new LogFactorias();

                    List<SP_Buscar_UsuarioResult> tc = linq.SP_Buscar_Usuario(req.Usuario.IdUsuario,
                        req.Usuario.Correo, req.Usuario.Nombre, req.Usuario.Apellidos
                        , req.Usuario.Admin, ref idBd, ref idError, ref errorDescripcion).ToList();

                    res.ListaUsuarios = Factorias.BuscarUsuario(tc);

                    res.Resultado = true;
                }
            }
            catch (Exception ex)
            {
                Error Error = new Error();

                Error.ErrorCode = enumErrores.excepcionLogica;
                Error.Message = ex.ToString();
                res.Error.Add(Error);
            }

            return res;
        }

        //Retornar toda la lista de usuarios
        public ResListaUsuarios ListaUsuarios(ReqListaUsuarios req)
        {
            ResListaUsuarios res = new ResListaUsuarios();
            res.ListaUsuarios = new List<Usuario>();
            res.Error = new List<Error>();

            try
            {
                using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                {
                    LogFactorias Factorias = new LogFactorias();

                    List<SP_Lista_UsuariosResult> tc = linq.SP_Lista_Usuarios().ToList();

                    res.ListaUsuarios = Factorias.ListaUsuarios(tc);

                    res.Resultado = true;
                }
            }
            catch (Exception ex)
            {
                Error Error = new Error();

                Error.ErrorCode = enumErrores.excepcionListaUsuarios;
                Error.Message = ex.ToString();
                res.Error.Add(Error);
            }

            return res;
        }

        //Elimina un Usuario
        public ResEliminarUsuario EliminarUsuario(ReqEliminarUsuario req)
        {
            ResEliminarUsuario res = new ResEliminarUsuario();
            res.Error = new List<Error>();

            try
            {
                //Validación del request 
                if (req == null)
                {
                    Error Error = new Error();
                    Error.ErrorCode = enumErrores.requestNulo;
                    Error.Message = "Req Null";
                    res.Error.Add(Error);
                }

                //Validacion del formato de correo
                String patron = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                bool match = System.Text.RegularExpressions.Regex.IsMatch(req.Usuario.Correo, patron);

                //Si no cumple con el patrón, se acumula el Error
                if (match == false)
                {
                    Error Error = new Error();

                    Error.ErrorCode = enumErrores.CorreoInvalido;
                    Error.Message = "Formato de correo no válido";
                    res.Error.Add(Error);
                }

                // verifico si hubo errores en todas las validaciones
                if (res.Error.Any())
                {
                    res.Resultado = false;
                    return res;
                }
                else
                {
                    //al no haber errores, se procede a eliminar el Usuario
                    using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                    {
                        int? idBd = 0;
                        int? idError = 0;
                        string errorDescripcion = null;
                        linq.SP_Eliminar_Usuario(req.Usuario.IdUsuario, req.Usuario.Correo.ToLower(),
                            ref idBd, ref idError, ref errorDescripcion);
                    }

                    res.Resultado = true;
                }
            }
            catch (Exception ex)
            {
                Error Error = new Error();
                Error.ErrorCode = enumErrores.excepcionEliminarUsuario;
                Error.Message = ex.ToString();
                res.Error.Add(Error);
            }

            return res;
        }

        public ResActualizarUsuario ActualizarUsuario(ReqActualizarUsuario req)
        {
            //Creacion de instancias generales del método
            ResActualizarUsuario res = new ResActualizarUsuario();
            res.Error = new List<Error>();

            //inicio de manejo de excepciones
            try
            {
                //validación del request
                if (req == null)
                {
                    Error Error = new Error();

                    Error.ErrorCode = enumErrores.requestNulo;
                    Error.Message = "Req Null";
                    res.Error.Add(Error);
                }


                if (!String.IsNullOrEmpty(req.Usuario.Nombre))
                {
                    //Validación de caracteres especiales en el nombre
                    String patron = @"^([\p{L}\s'-]*|\s*)$";
                    bool match = System.Text.RegularExpressions.Regex.IsMatch(req.Usuario.Nombre, patron);
                    //Si no cumple con el patrón, se acumula el Error
                    if (match == false)
                    {
                        Error Error = new Error();

                        Error.ErrorCode = enumErrores.NombreInvalido;
                        Error.Message = "Hay un carácter no válido en el nombre";
                        res.Error.Add(Error);
                    }
                }

                if (!String.IsNullOrEmpty(req.Usuario.Apellidos))
                {
                    //Validación de caracteres especiales en el apellido
                    String patron = @"^([\p{L}\s'-]*|\s*)$";
                    bool match = System.Text.RegularExpressions.Regex.IsMatch(req.Usuario.Apellidos, patron);

                    //Si no cumple con el patrón, se acumula el Error
                    if (match == false)
                    {
                        Error Error = new Error();

                        Error.ErrorCode = enumErrores.ApellidoInvalido;
                        Error.Message = "Hay un carácter no válido en el apellido";
                        res.Error.Add(Error);
                    }
                }

                if (!String.IsNullOrEmpty(req.Usuario.Correo))
                {
                    //Validacion del formato de correo
                    String patron = @"^$|^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                    bool match = System.Text.RegularExpressions.Regex.IsMatch(req.Usuario.Correo, patron);

                    //Si no cumple con el patrón, se acumula el Error
                    if (match == false)
                    {
                        Error Error = new Error();

                        Error.ErrorCode = enumErrores.CorreoInvalido;
                        Error.Message = "Formato de correo no válido";
                        res.Error.Add(Error);
                    }
                }

                if (!String.IsNullOrEmpty(req.Usuario.Password))
                {
                    //Validacion de la contraseña
                    String patron =
                        @"^$|^(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+\-=\[\]{};':""\\/|,.<>/?])(?=.{8,}).*$";
                    bool match = System.Text.RegularExpressions.Regex.IsMatch(req.Usuario.Password, patron);
                    //Si no cumple con el patrón, se acumula el Error
                    if (match == false)
                    {
                        Error Error = new Error();

                        Error.ErrorCode = enumErrores.passwordInvalido;
                        Error.Message = "La contraseña no cumple con alguna de las siguientes características: \n" +
                                        "1- Mínimo 8 caracteres" + "2- Al menos un simbolo 3- Al menos una mayúscula " +
                                        "4-Al menos una minúscula";
                        res.Error.Add(Error);
                    }
                }

                //Se valida si hubo errores en todas las validaciones
                if (res.Error.Any())
                {
                    res.Resultado = false;
                }
                //Si no hubo errores se agrega el Usuario a la base de datos
                else
                {
                    int? idBd = 0;
                    int? idError = 0;
                    string errorDescripcion = null;

                    using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                    {
                        linq.SP_ActualizarUsuario(req.Usuario.IdUsuario, req.Usuario.Nombre, req.Usuario.Apellidos,
                            req.Usuario.Telefono, req.Usuario.Telefono_Verificado, req.Usuario.Cod_Ver_Tel,
                            req.Usuario.Correo, req.Usuario.Correo_Verificado, req.Usuario.Cod_Ver_Cor,
                            req.Usuario.Admin, req.Usuario.Password, req.Usuario.Vehiculo, ref idBd, ref idError,
                            ref errorDescripcion);
                    }
                }
            }

            //Manejo de excepciones
            catch (Exception ex)
            {
                Error Error = new Error();

                Error.ErrorCode = enumErrores.excepcionLogica;
                Error.Message = ex.ToString();
                res.Error.Add(Error);
            }

            //Retorno de la respuesta
            return res;
        }

        public ResRevisarTelefono RevisarTelefono(ReqRevisarTelefono req)
        {
            ResRevisarTelefono res = new ResRevisarTelefono
            {
                Error = new List<Error>()
            };

            if (req != null)
            {
                if (req.idUsuario == null || req.idUsuario <= 0)
                {
                    Error Error = new Error
                    {
                        ErrorCode = enumErrores.requestIncompleto,
                        Message = "Telefono nulo o no válido"
                    };
                    res.Error.Add(Error);
                    res.Resultado = false;
                    return res;
                }

                try
                {
                    bool? verif = null;
                    using (ConexionLinqDataContext linqDataContext = new ConexionLinqDataContext())
                    {
                        linqDataContext.SP_RevisarValidacionTelefono(req.idUsuario, ref verif);
                    }

                    if (verif == true)
                    {
                        res.Resultado = true;
                        res.verificacion = true;
                    }
                    else
                    {
                        res.Resultado = true;
                        res.verificacion = false;
                    }
                }
                catch (Exception e)
                {
                    Error Error = new Error
                    {
                        ErrorCode = enumErrores.excepcionLogica,
                        Message = "Ha ocurrido un Error " + e.Message
                    };
                    res.Error.Add(Error);
                    res.Resultado = false;
                    return res;
                }
            }
            else
            {
                Error Error = new Error
                {
                    ErrorCode = enumErrores.requestNulo,
                    Message = "Req Null"
                };
                res.Error.Add(Error);
                res.Resultado = false;
                return res;
            }

            return res;
        }
    }
}