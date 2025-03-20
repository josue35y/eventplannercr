using EventPlannerCR_backend.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using EventPlannerCR_AccesoDatos;
using System.Net.Http.Headers;

namespace EventPlannerCR_backend.Logica
{
    public class LogUsuario
    {

        public ResInsertarUsuario insertar(ReqInsertarUsuario req)
        {
            ResInsertarUsuario res = new ResInsertarUsuario();
            res.error = new List<Error>();
            //Error error = new Error();

            try
            {


                if (req == null)
                {
                    res.error.Add(Error.generarError(enumErrores.requestNulo, "Req Null"));
                    res.resultado = false;
                    return res;
                }
                else
                {

                    if (String.IsNullOrEmpty(req.usuario.Nombre))  //(req .usuario.nombre == null || req.usuario.nombre == "")
                    {
                        res.error.Add(Error.generarError(enumErrores.nombreFaltante, "Nombre vacio."));
                    }
                    if (String.IsNullOrEmpty(req.usuario.Apellidos))
                    {
                        res.error.Add(Error.generarError(enumErrores.apellidoFaltante, "Apellidos vacio."));
                    }
                    if (String.IsNullOrEmpty(req.usuario.Telefono))
                    {
                        res.error.Add(Error.generarError(enumErrores.telefonoFaltante, "Telefono vacio."));
                    }
                    if (String.IsNullOrEmpty(req.usuario.Correo))
                    {
                        res.error.Add(Error.generarError(enumErrores.correoFaltante, "Correo vacio."));
                    }
                    if (String.IsNullOrEmpty(req.usuario.FechaNacimiento.ToString()))
                    {
                        res.error.Add(Error.generarError(enumErrores.FechaNacimiento, "Fecha de nacimiento vacia."));
                    }


                    if (req.usuario.Provincia == null)
                    {
                        res.error.Add(Error.generarError(enumErrores.provinciaFaltante, "Provincia vacia."));
                    }
                    if (req.usuario.Canton == null)
                    {
                        res.error.Add(Error.generarError(enumErrores.cantonFaltante, "Canton vacio."));
                    }
                    if (req.usuario.Distrito == null)
                    {
                        res.error.Add(Error.generarError(enumErrores.distritoFaltante, "Distrito vacio."));
                    }


                    if (req.usuario.Admin == null)
                    {
                        res.error.Add(Error.generarError(enumErrores.AtributoInvalido, "Admin vacio."));
                    }

                    if (String.IsNullOrEmpty(req.usuario.Password))
                    {
                        res.error.Add(Error.generarError(enumErrores.passwordFaltante, "Password vacío"));
                    }

                    if (req.usuario.Vehiculo == null)
                    {
                        res.error.Add(Error.generarError(enumErrores.AtributoInvalido, "Vehiculo vacio."));
                    }







                    if (res.error.Any())
                    {
                        res.resultado = false;
                    }
                    else // No hubo errores.
                    {

                        int? idBd = null;
                        int? idError = null;
                        string errorDescripcion = null;

                        bool estado = true;
                        Guid guid = Guid.NewGuid();

                        using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                        {
                            linq.SP_InsertarUsuario_josue(
                                req.usuario.Nombre,
                                req.usuario.Apellidos,
                                req.usuario.Telefono,
                                req.usuario.Correo,
                                req.usuario.FechaNacimiento,
                                req.usuario.Provincia,
                                req.usuario.Canton,
                                req.usuario.Distrito,
                                req.usuario.Admin,
                                req.usuario.Password,
                                req.usuario.Vehiculo,
                                guid.ToString(),
                                estado,
                                ref idBd,
                                ref idError,
                                ref errorDescripcion);
                        }
                        if (idError < 0)
                        {
                            res.error.Add(Error.generarError(enumErrores.excepcionBaseDatos, errorDescripcion));
                        }

                        if (idBd > 0)
                        {
                            res.resultado = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.error.Add(Error.generarError(enumErrores.excepcionLogica, ex.ToString()));
            }
            return res;
        }




        #region laFactoria!
        //private Usuario FactoryUsuario(SP_InsertarUsuarioResult tc)
        //{
        //    //
        //    Usuario usuario = new Usuario();
        //    usuario.Nombre = tc.NOMBRE;
        //    usuario.Apellidos = tc.APELLIDOS;
        //    usuario.Correo = tc.CORREO_ELECTRONICO;
        //    usuario.FechaRegistro = tc.FECHA_REGISTRO.AddHours(-6);


        //    return usuario;
        //}
        #endregion

        #region helpers

        public bool EsCorreoValido(string correo)
        {
            // Verifica que el correo no sea nulo o vacío.
            if (string.IsNullOrWhiteSpace(correo))
                return false;

            // Patrón simple para validar correo electrónico.
            string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            return Regex.IsMatch(correo, patron);
        }

        public bool EsPasswordSeguro(string password)
        {
            // Verifica que el Password no sea nulo o vacío.
            if (string.IsNullOrWhiteSpace(password))
                return false;

            // Patrón que valida el Password según los criterios mencionados.
            string patron = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";

            return Regex.IsMatch(password, patron);
        }

        public string GenerarPin(int longitud)
        {
            // Crea una instancia de Random.
            Random rnd = new Random();
            // Utiliza StringBuilder para construir el PIN.
            StringBuilder pin = new StringBuilder();

            // Itera la cantidad de veces según la longitud deseada.
            for (int i = 0; i < longitud; i++)
            {
                // Genera un dígito entre 0 y 9 y lo agrega al PIN.
                pin.Append(rnd.Next(0, 10));
            }

            return pin.ToString();
        }

        #endregion
    }
}
