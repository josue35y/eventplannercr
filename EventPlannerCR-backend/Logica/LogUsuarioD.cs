using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acceso_de_datos;
using EventPlannerCR_backend.Entidades;

namespace EventPlannerCR_backend.Logica
{
    public class LogUsuarioD
    {
        //Método para inerstar usuario
        public ResInsertarUsuarioD Insertar(ReqInsertarUsuarioD req) {

            //Creacion de instancias generales del método
            ResInsertarUsuarioD res = new ResInsertarUsuarioD();
            res.error = new List<Error>();

            //inicio de manejo de excepciones
            try {

                //validación del request
                if (req == null)
                {
                    Error error = new Error();

                    error.ErrorCode = enumErrores.requestNulo;
                    error.Message = "Req Null";
                    res.error.Add(error);
                }

                //Validación del nombre del nuevo usuario para evitar nulos o espacio en blanco
                if (String.IsNullOrEmpty(req.usuario.Nombre))
                {
                    Error error = new Error();

                    //Acumula la respuesta de error
                    error.ErrorCode = enumErrores.nombreFaltante;
                    error.Message = "Nombre nulo o no válido";
                    res.error.Add(error);
                }
                else
                {

                    //Validación de caracteres especiales en el nombre
                    String patron = @"^[\p{L}\s'-]+$";
                    bool match = System.Text.RegularExpressions.Regex.IsMatch(req.usuario.Nombre, patron);
                    //Si no cumple con el patrón, se acumula el error
                    if (match == false)
                    {
                        Error error = new Error();

                        error.ErrorCode = enumErrores.NombreInvalido;
                        error.Message = "Hay un carácter no válido en el nombre";
                        res.error.Add(error);
                    }
                }

                //Validación del apellido del nuevo usuario para evitar nulos o espacio en blanco
                if (String.IsNullOrEmpty(req.usuario.Apellidos))
                {
                    Error error = new Error();

                    //Acumula la respuesta de error
                    error.ErrorCode = enumErrores.apellidoFaltante;
                    error.Message = "Apellidos nulo o no válido";
                    res.error.Add(error);
                }
                else
                {

                    //Validación de caracteres especiales en el apellido
                    String patron = @"^[\p{L}\s'-]+$";
                    bool match = System.Text.RegularExpressions.Regex.IsMatch(req.usuario.Apellidos, patron);
                    
                    
                    //Si no cumple con el patrón, se acumula el error
                    if (match == false)
                    {
                        Error error = new Error();

                        error.ErrorCode = enumErrores.ApellidoInvalido;
                        error.Message = "Hay un carácter no válido en el nombre";
                        res.error.Add(error);
                    }
                }

                //Validación del correo del nuevo usuario para evitar nulos o espacio en blanco
                if (String.IsNullOrEmpty(req.usuario.Correo))
                {
                    Error error = new Error();

                    //Acumula la respuesta de error
                    error.ErrorCode = enumErrores.correoFaltante;
                    error.Message = "Correo nulo o no válido";
                    res.error.Add(error);
                }
                
                //Validación de formato de correo
                else
                {
                    //Validacion del formato de correo
                    String patron = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                    bool match = System.Text.RegularExpressions.Regex.IsMatch(req.usuario.Correo, patron);

                    //Si no cumple con el patrón, se acumula el error
                    if (match == false)
                    {
                        Error error = new Error();

                        error.ErrorCode = enumErrores.CorreoInvalido;
                        error.Message = "Formato de correo no válido";
                        res.error.Add(error);
                    }
                }

                //Validación de la contraseña del nuevo usuario para evitar nulos o espacio en blanco
                if (String.IsNullOrEmpty(req.usuario.Password))
                {
                    Error error = new Error();

                    error.ErrorCode = enumErrores.passwordFaltante;
                    error.Message = "Password nulo o no válido";
                    res.error.Add(error);
                }
                else
                {
                    //Validacion de la contraseña
                    String patron = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+\-=\[\]{};':""\\/|,.<>/?])(?=.{8,}).*$";
                    bool match = System.Text.RegularExpressions.Regex.IsMatch(req.usuario.Password, patron);
                    //Si no cumple con el patrón, se acumula el error
                    if (match == false)
                    {
                        Error error = new Error();

                        error.ErrorCode = enumErrores.passwordInvalido;
                        error.Message = "La contraseña no cumple con alguna de las siguientes características: \n" +
                            "1- Mínimo 8 caracteres" +"2- Al menos un simbolo 3- Al menos una mayúscula " +
                            "4-Al menos una minúscula";
                        res.error.Add(error);
                    }
                }

                //Validación de la fecha de nacimiento del nuevo usuario para evitar nulos
                if (req.usuario.FechaNacimiento==null)
                {
                    Error error = new Error();

                    error.ErrorCode = enumErrores.FechaNacimientoFaltante;
                    error.Message = "Fecha nula";
                    res.error.Add(error);
                }

                //Se valida si hubo errores en todas las validaciones
                if (res.error.Any())
                {
                    res.resultado = false;
                }
                //Si no hubo errores se agrega el usuario a la base de datos
                else {

                    int? idBd = 0;
                    int? idError = 0;
                    string errorDescripcion = null;

                    using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                    {
                        linq.SP_InsertarUsuarios(
                            req.usuario.Nombre, req.usuario.Apellidos,
                            req.usuario.Correo, req.usuario.FechaNacimiento,
                            req.usuario.Password, ref idBd, ref idError,
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
                res.error.Add(error);
            }

            //Retorno de la respuesta
            return res;
        }

        //Método para insertar usuario
        public ResBuscarUsuario BuscarUsuarioPorCorreo(ReqBuscarUsuario req) { 
        
            ResBuscarUsuario res = new ResBuscarUsuario();
            res.error = new List<Error>();

            try
            {

            }
            catch (Exception ex) 
            {
                Error error = new Error();

                error.ErrorCode = enumErrores.excepcionLogica;
                error.Message = ex.ToString();
                res.error.Add(error);
            }

            return res;
        }

        public ResListaUsuarios ListaUsuarios(ReqListaUsuarios req) {

            ResListaUsuarios res = new ResListaUsuarios();
            res.ListaUsuarios = new List<UsuarioD>();
            res.error = new List<Error>();
            int? idBd = 0;
            int? idError = 0;
            string errorDescripcion = null;

            try 
            {

                using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                {
                    Factorias Factorias = new Factorias();

                    res.ListaUsuarios = Factorias.ListaUsuarios((SP_Lista_UsuariosResult)linq.SP_Lista_Usuarios(ref idBd, ref idError, ref errorDescripcion)).ToList();
                }
            }
            catch (Exception ex) 
            {

                Error error = new Error();

                error.ErrorCode = enumErrores.excepcionListaUsuarios;
                error.Message = ex.ToString();
                res.error.Add(error);
            }

            return res;
        }
    }
}
