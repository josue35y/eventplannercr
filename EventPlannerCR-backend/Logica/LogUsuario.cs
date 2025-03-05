﻿using EventPlannerCR_backend.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using EventPlannerCR_AccesoDatos;

namespace EventPlannerCR_backend.Logica
{
    public class LogUsuario
    {

        public ResInsertarUsuario insertar(ReqInsertarUsuario req)
        {
            ResInsertarUsuario res = new ResInsertarUsuario();
            res.error = new List<Error>();
            Error error = new Error();

            try
            {


                if (req == null)
                {
                    error.ErrorCode = (int)enumErrores.requestNulo;
                    error.Message = "Req Null";
                    res.error.Add(error);

                    //Enviar error a bitacora:
                    //object LogObject = RegistrarBitacora("LogUsuario", "insertar", "Error", "400", "El request es nulo", null, null);


                }
                else
                {
                    if (String.IsNullOrEmpty(req.usuario.Nombre))  //(req .usuario.nombre == null || req.usuario.nombre == "")
                    {
                        error.ErrorCode = (int)enumErrores.nombreFaltante;
                        error.Message = "Nombre vacio";
                        res.error.Add(error);
                    }
                    if (String.IsNullOrEmpty(req.usuario.Apellidos))
                    {
                        error.ErrorCode = (int)enumErrores.apellidoFaltante;
                        error.Message = "Apellido vacio";
                        res.error.Add(error);
                    }
                    if (String.IsNullOrEmpty(req.usuario.Correo))
                    {
                        error.ErrorCode = (int)enumErrores.correoFaltante;
                        error.Message = "Correo vacio";
                        res.error.Add(error);
                    }
                    if (String.IsNullOrEmpty(req.usuario.Correo)) // CAMBIAR POR CO
                    {
                        error.ErrorCode = (int)enumErrores.correoFaltante;
                        error.Message = "Correo vacio";
                        res.error.Add(error);
                    }
                    if (String.IsNullOrEmpty(req.usuario.Password))
                    {
                        error.ErrorCode = (int)enumErrores.passwordFaltante;
                        error.Message = "Password vacio";
                        res.error.Add(error);
                    }
                    if (String.IsNullOrEmpty(req.usuario.Password))
                    {
                        error.ErrorCode = (int)enumErrores.passwordFaltante;
                        error.Message = "Password vacio";
                        res.error.Add(error);
                    }

                    //
                    int? idUsuario = 0;
                    int? idBD = 0;
                    int? idError = 0;
                    string errorDescripcion = null;

                    if (res.error.Any())
                    {
                        // hay al menos 1 error.
                        res.resultado = false;
                    }
                    else     // No hubo errores.
                    {

                        using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                        {
                            linq.SP_InsertarUsuario(
                                req.usuario.Nombre,
                                req.usuario.Apellidos,
                                req.usuario.Telefono,
                                req.usuario.Correo,
                                req.usuario.FechaNacimiento,
                                req.usuario.Provincia,
                                req.usuario.Admin,
                                req.usuario.Password,
                                req.usuario.Vehiculo,

                                ref idBD,
                                ref idError,
                                ref errorDescripcion);
                        }


                        if (idBD >= 1)
                        {
                            res.resultado = true;

                        }
                        else
                        {
                            error.Message = errorDescripcion; //MALA Practica.
                            res.error.Add(error);
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                error.ErrorCode = (int)enumErrores.excepcionLogica; //CAMBIAR
                error.Message = ex.ToString();
                res.error.Add(error);

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
