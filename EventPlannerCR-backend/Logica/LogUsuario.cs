using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventPlannerCR_backend.Entidades;
using EventPlannerCR_AccesoDatos;
using System.Text.RegularExpressions;
using System.IO;

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
                    Error error = new Error();

                    error.ErrorCode = enumErrores.requestNulo;
                    error.Message = "Req Null";
                    res.Error.Add(error);
                }

                //Validación del nombre del nuevo Usuario para evitar nulos o espacio en blanco
                if (String.IsNullOrEmpty(req.Usuario.Nombre))
                {
                    Error error = new Error();

                    //Acumula la respuesta de Error
                    error.ErrorCode = enumErrores.nombreFaltante;
                    error.Message = "Nombre nulo o no válido";
                    res.Error.Add(error);
                }
                else
                {

                    //Validación de caracteres especiales en el nombre
                    String patron = @"^[\p{L}\s'-]+$";
                    bool match = System.Text.RegularExpressions.Regex.IsMatch(req.Usuario.Nombre, patron);
                    //Si no cumple con el patrón, se acumula el Error
                    if (match == false)
                    {
                        Error error = new Error();

                        error.ErrorCode = enumErrores.NombreInvalido;
                        error.Message = "Hay un carácter no válido en el nombre";
                        res.Error.Add(error);
                    }
                }

                //Validación del apellido del nuevo Usuario para evitar nulos o espacio en blanco
                if (String.IsNullOrEmpty(req.Usuario.Apellidos))
                {
                    Error error = new Error();

                    //Acumula la respuesta de Error
                    error.ErrorCode = enumErrores.apellidoFaltante;
                    error.Message = "Apellidos nulo o no válido";
                    res.Error.Add(error);
                }
                else
                {

                    //Validación de caracteres especiales en el apellido
                    String patron = @"^[\p{L}\s'-]+$";
                    bool match = System.Text.RegularExpressions.Regex.IsMatch(req.Usuario.Apellidos, patron);


                    //Si no cumple con el patrón, se acumula el Error
                    if (match == false)
                    {
                        Error error = new Error();

                        error.ErrorCode = enumErrores.ApellidoInvalido;
                        error.Message = "Hay un carácter no válido en el apellido";
                        res.Error.Add(error);
                    }
                }

                //Validación del correo del nuevo Usuario para evitar nulos o espacio en blanco
                if (String.IsNullOrEmpty(req.Usuario.Correo))
                {
                    Error error = new Error();

                    //Acumula la respuesta de Error
                    error.ErrorCode = enumErrores.correoFaltante;
                    error.Message = "Correo nulo o no válido";
                    res.Error.Add(error);
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
                        Error error = new Error();

                        error.ErrorCode = enumErrores.CorreoInvalido;
                        error.Message = "Formato de correo no válido";
                        res.Error.Add(error);
                    }
                }

                //Validación de la contraseña del nuevo Usuario para evitar nulos o espacio en blanco
                if (String.IsNullOrEmpty(req.Usuario.Password))
                {
                    Error error = new Error();

                    error.ErrorCode = enumErrores.passwordFaltante;
                    error.Message = "Password nulo o no válido";
                    res.Error.Add(error);
                }
                else
                {
                    //Validacion de la contraseña
                    String patron = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+\-=\[\]{};':""\\/|,.<>/?])(?=.{8,}).*$";
                    bool match = System.Text.RegularExpressions.Regex.IsMatch(req.Usuario.Password, patron);
                    //Si no cumple con el patrón, se acumula el Error
                    if (match == false)
                    {
                        Error error = new Error();

                        error.ErrorCode = enumErrores.passwordInvalido;
                        error.Message = "La contraseña no cumple con alguna de las siguientes características: \n" +
                            "1- Mínimo 8 caracteres" + "2- Al menos un simbolo 3- Al menos una mayúscula " +
                            "4-Al menos una minúscula";
                        res.Error.Add(error);
                    }
                }

                //Validación de la fecha de nacimiento del nuevo Usuario para evitar nulos
                if (req.Usuario.FechaNacimiento == null || req.Usuario.FechaNacimiento.Date == default)
                {
                    Error error = new Error();

                    error.ErrorCode = enumErrores.FechaNacimientoFaltante;
                    error.Message = "Fecha nula";
                    res.Error.Add(error);
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
                        linq.SP_InsertarUsuario(
                            req.Usuario.Nombre, req.Usuario.Apellidos,
                            req.Usuario.Correo, Codigo, req.Usuario.FechaNacimiento,
                            req.Usuario.Password, ref idBd, ref idError,
                            ref errorDescripcion);
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
                Error error = new Error();

                error.ErrorCode = enumErrores.excepcionLogica;
                error.Message = ex.ToString();
                res.Error.Add(error);
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
                    Error error = new Error();
                    error.ErrorCode = enumErrores.requestNulo;
                    error.Message = "Req Null";
                    res.Error.Add(error);
                }

                if (!String.IsNullOrEmpty(req.Usuario.Nombre)) {

                    req.Usuario.Nombre = req.Usuario.Nombre.ToLower();
                    //Validación de caracteres especiales en el nombre
                    String patron = "^[a-zA-Z0-9áéíóúÁÉÍÓÚüÜñÑ]+$";
                    bool match = System.Text.RegularExpressions.Regex.IsMatch(req.Usuario.Nombre, patron);
                    //Si no cumple con el patrón, se acumula el Error
                    if (!match)
                    {
                        Error error = new Error();

                        error.ErrorCode = enumErrores.NombreInvalido;
                        error.Message = "Hay un carácter no válido en el nombre";
                        res.Error.Add(error);
                    }
                }

                if (!String.IsNullOrEmpty(req.Usuario.Apellidos)) {

                    req.Usuario.Apellidos = req.Usuario.Apellidos.ToLower();
                    //Validación de caracteres especiales en el nombre
                    String patron = "^[a-zA-Z0-9áéíóúÁÉÍÓÚüÜñÑ]+$";
                    bool match = System.Text.RegularExpressions.Regex.IsMatch(req.Usuario.Apellidos, patron);
                    //Si no cumple con el patrón, se acumula el Error

                    if (!match)
                    {
                        Error error = new Error();

                        error.ErrorCode = enumErrores.ApellidoInvalido;
                        error.Message = "Hay un carácter no válido en el apellido";
                        res.Error.Add(error);
                    }
                }

                if (res.Error.Any()) {

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
                        , req.Usuario.Admin, ref idBd, ref idError,ref errorDescripcion).ToList();

                    res.ListaUsuarios = Factorias.Buscarusuario(tc);

                    res.Resultado = true;
                }
            }
            catch (Exception ex)
            {
                Error error = new Error();

                error.ErrorCode = enumErrores.excepcionLogica;
                error.Message = ex.ToString();
                res.Error.Add(error);
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

                    List <SP_Lista_UsuariosResult> tc = linq.SP_Lista_Usuarios().ToList();

                    res.ListaUsuarios = Factorias.ListaUsuarios(tc);

                    res.Resultado = true;
                }
            }
            catch (Exception ex)
            {

                Error error = new Error();

                error.ErrorCode = enumErrores.excepcionListaUsuarios;
                error.Message = ex.ToString();
                res.Error.Add(error);
            }

            return res;
        }

        //Elimina un Usuario
        public ResEliminarUsuario EliminarUsuario(ReqEliminarUsuario req) { 
        
            ResEliminarUsuario res = new ResEliminarUsuario();
            res.Error = new List<Error>();

            try {

                //Validación del request 
                if (req == null)
                {
                    Error error = new Error();
                    error.ErrorCode = enumErrores.requestNulo;
                    error.Message = "Req Null";
                    res.Error.Add(error);
                }

                //Validacion del formato de correo
                String patron = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                bool match = System.Text.RegularExpressions.Regex.IsMatch(req.Usuario.Correo, patron);

                //Si no cumple con el patrón, se acumula el Error
                if (match == false)
                {
                    Error error = new Error();

                    error.ErrorCode = enumErrores.CorreoInvalido;
                    error.Message = "Formato de correo no válido";
                    res.Error.Add(error);
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
                Error error = new Error();
                error.ErrorCode = enumErrores.excepcionEliminarUsuario;
                error.Message = ex.ToString();
                res.Error.Add(error);
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
                    Error error = new Error();

                    error.ErrorCode = enumErrores.requestNulo;
                    error.Message = "Req Null";
                    res.Error.Add(error);
                }

                

                if (!String.IsNullOrEmpty(req.Usuario.Nombre)) {

                    //Validación de caracteres especiales en el nombre
                    String patron = @"^([\p{L}\s'-]*|\s*)$";
                    bool match = System.Text.RegularExpressions.Regex.IsMatch(req.Usuario.Nombre, patron);
                    //Si no cumple con el patrón, se acumula el Error
                    if (match == false)
                    {
                        Error error = new Error();

                        error.ErrorCode = enumErrores.NombreInvalido;
                        error.Message = "Hay un carácter no válido en el nombre";
                        res.Error.Add(error);
                    }

                }

                if (!String.IsNullOrEmpty(req.Usuario.Apellidos)) {

                    //Validación de caracteres especiales en el apellido
                    String patron = @"^([\p{L}\s'-]*|\s*)$";
                    bool match = System.Text.RegularExpressions.Regex.IsMatch(req.Usuario.Apellidos, patron);

                    //Si no cumple con el patrón, se acumula el Error
                    if (match == false)
                    {
                        Error error = new Error();

                        error.ErrorCode = enumErrores.ApellidoInvalido;
                        error.Message = "Hay un carácter no válido en el apellido";
                        res.Error.Add(error);
                    }
                }

                if (!String.IsNullOrEmpty(req.Usuario.Correo)) {

                    //Validacion del formato de correo
                    String patron = @"^$|^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                    bool match = System.Text.RegularExpressions.Regex.IsMatch(req.Usuario.Correo, patron);

                    //Si no cumple con el patrón, se acumula el Error
                    if (match == false)
                    {
                        Error error = new Error();

                        error.ErrorCode = enumErrores.CorreoInvalido;
                        error.Message = "Formato de correo no válido";
                        res.Error.Add(error);
                    }
                }

                if (!String.IsNullOrEmpty(req.Usuario.Password)) {

                    //Validacion de la contraseña
                    String patron = @"^$|^(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+\-=\[\]{};':""\\/|,.<>/?])(?=.{8,}).*$";
                    bool match = System.Text.RegularExpressions.Regex.IsMatch(req.Usuario.Password, patron);
                    //Si no cumple con el patrón, se acumula el Error
                    if (match == false)
                    {
                        Error error = new Error();

                        error.ErrorCode = enumErrores.passwordInvalido;
                        error.Message = "La contraseña no cumple con alguna de las siguientes características: \n" +
                            "1- Mínimo 8 caracteres" + "2- Al menos un simbolo 3- Al menos una mayúscula " +
                            "4-Al menos una minúscula";
                        res.Error.Add(error);
                    }
                }

                ////Validación de la fecha de nacimiento del nuevo Usuario para evitar nulos
                //if (req.Usuario.FechaNacimiento == null)
                //{
                //    Error Error = new Error();

                //    Error.ErrorCode = enumErrores.FechaNacimientoFaltante;
                //    Error.Message = "Fecha nula";
                //    res.Error.Add(Error);
                //}

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

                        linq.SP_ActualizarUsuario( req.Usuario.IdUsuario, req.Usuario.Nombre, req.Usuario.Apellidos, 
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
                Error error = new Error();

                error.ErrorCode = enumErrores.excepcionLogica;
                error.Message = ex.ToString();
                res.Error.Add(error);
            }

            //Retorno de la respuesta
            return res;
        }
    }
}
