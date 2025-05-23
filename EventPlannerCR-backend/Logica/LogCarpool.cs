﻿using EventPlannerCR_AccesoDatos;
using EventPlannerCR_backend.Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlannerCR_backend.Logica
{
    public class LogCarpool
    {
        #region INSERTAR 
        public ResInsertarCarpool insertar(ReqInsertarCarpool req)
        {
            ResInsertarCarpool res = new ResInsertarCarpool();
            res.Error = new List<Error>();
            Error error = new Error(); 
            try
            {

                if (req == null)
                {
                    res.Error.Add(Error.generarError(enumErrores.requestNulo, "El request es nulo."));
                }
                else
                {
                    if (req.Carpool == null)
                    {
                        res.Error.Add(Error.generarError(enumErrores.requestIncompleto, "La información del carpool está incompleta o es nula."));
                    }
                    if (req.Carpool.Evento == null || req.Carpool.Evento.IdEvento == null || req.Carpool.Evento.IdEvento <= 0)
                    {
                        res.Error.Add(Error.generarError(enumErrores.idFaltante, "ID de evento faltante, incorrecto o nulo."));
                    }
                    if (req.Carpool.CamposDisponibles <= 0 || req.Carpool.CamposDisponibles == null )
                    {                        
                        res.Error.Add(Error.generarError(enumErrores.camposDisponiblesFaltante, "Es necesario indicar la cantidad de campos disponibles."));
                    }
                    if (req.Carpool.Provincia <= 0 || req.Carpool.Provincia == null)
                    {
                        res.Error.Add(Error.generarError(enumErrores.provinciaFaltante, "Es necesario indicar la provincia."));
                    }
                    if (req.Carpool.Canton <= 0 || req.Carpool.Canton == null)
                    {
                        res.Error.Add(Error.generarError(enumErrores.cantonFaltante, "Es necesario indicar el cantón."));
                    }
                    if (req.Carpool.Distrito <= 0 || req.Carpool.Distrito == null )
                    {
                        res.Error.Add(Error.generarError(enumErrores.distritoFaltante, "Es necesario indicar el distrito."));
                    }

                    int? idBD = 0;
                    int? idError = 0;
                    string errorDescripcion = null;

                    if (res.Error.Any())
                    {
                        res.Resultado = false;
                    }
                    else 
                    {
                        using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                        {
                            linq.SP_InsertarCarpool(
                                req.Carpool.Evento.IdEvento,
                                req.Carpool.CamposDisponibles,
                                req.Carpool.Provincia,
                                req.Carpool.Canton,
                                req.Carpool.Distrito,
                                req.Carpool.idUsuario,
                                req.Carpool.NotasCarpool,
                                ref idBD,
                                ref idError,
                                ref errorDescripcion);
                        }
                        if (idBD >= 1)
                        {
                            res.Resultado = true;
                        }
                        else
                        {
                            res.Error.Add(Error.generarError(enumErrores.excepcionBaseDatos, errorDescripcion));
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                res.Error.Add(Error.generarError(enumErrores.excepcionLogica, ex.ToString()));
            }
            return res;
        }
        #endregion

        #region BUSCAR
        public ResObtenerCarpoolPorEvento listar(ReqObtenerCarpoolPorEvento req)
        {
            ResObtenerCarpoolPorEvento res = new ResObtenerCarpoolPorEvento();
            List<ResObtenerCarpoolPorEvento.CarpoolPorEvento_Modelo> ListaCarpools = new List<ResObtenerCarpoolPorEvento.CarpoolPorEvento_Modelo>();  
            res.Error = new List<Error>();

            try
            {
                if (req.Sesion.Estado == enumEstadoSesion.cerrada)
                {
                    res.Error.Add(Error.generarError(enumErrores.sessionCerrada, "Sesion expirada."));
                    res.Resultado = false;
                    return res;  
                }

                if (req == null)
                {
                    res.Error.Add(Error.generarError(enumErrores.requestNulo, "El request es nulo"));
                    res.Resultado = false;
                    return res; 
                }

                if (req.idEvento < 0) // Puede haber validaciones adicionales
                {
                    res.Error.Add(Error.generarError(enumErrores.idFaltante, "El request es nulo"));
                    res.Resultado = false;
                    return res; 
                }

                int? idError = 0;
                string errorDescripcion = null;

                using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                {
                    var listaCarpools = linq.SP_BuscarCarpoolPorEvento2(
                        req.idEvento,
                        ref idError,
                        ref errorDescripcion
                    ).ToList();

                    if (idError != null && idError > 0)
                    {
                        res.Error.Add(Error.generarError(enumErrores.datosNoEncontrados, errorDescripcion)); res.Resultado = false; return res;
                    }

                    int? idError2 = 0;
                    string errorDescripcion2 = null;

                    // Buscar usuarios asociados a cada carpool
                    res.CarpoolList = listaCarpools.Select(carpool =>
                    {
                        var usuarios = linq.SP_BuscarUsuariosPorCarpool(carpool.IdCarpool, ref idError2, ref errorDescripcion2)
                            .Select(u => new ResObtenerCarpoolPorEvento.UsuariosCarpool
                            {
                                idUsuario = u.ID_USUARIO.ToString(),
                                NombreApellido = $"{u.NombreApellido}"
                            }).ToList();
                        return FactoryCarpool(carpool, usuarios);
                    }).ToList();

                    if (idError2 != null && idError2 > 0)
                    {
                        res.Error.Add(Error.generarError(enumErrores.datosNoEncontrados, errorDescripcion2)); res.Resultado = false; return res;
                    }

                    res.Resultado = true;
                }
            }
            catch (Exception ex)
            {
                res.Error.Add(Error.generarError(enumErrores.excepcionLogica, ex.Message));
                res.Resultado = false;
            }
            return res;
        }

        public ResObtenerCarpoolPorUsuario listarUsuario(ReqObtenerCarpoolPorUsuario req)
        {
            ResObtenerCarpoolPorUsuario res = new ResObtenerCarpoolPorUsuario();
            List<ResObtenerCarpoolPorUsuario.CarpoolPorUsuario_Modelo> ListaCarpools = new List<ResObtenerCarpoolPorUsuario.CarpoolPorUsuario_Modelo>();
            res.Error = new List<Error>();

            try
            {
                if (req.Sesion.Estado == enumEstadoSesion.cerrada)
                {
                    res.Error.Add(Error.generarError(enumErrores.sessionCerrada, "Sesion expirada."));
                    res.Resultado = false;
                    return res;
                }

                if (req == null)
                {
                    res.Error.Add(Error.generarError(enumErrores.requestNulo, "El request es nulo"));
                    res.Resultado = false;
                    return res;
                }

                if (req.idUsuario < 0 || req.idUsuario == null) 
                {
                    res.Error.Add(Error.generarError(enumErrores.idFaltante, "El request es nulo"));
                    res.Resultado = false;
                    return res;
                }

                int? idError = 0;
                string errorDescripcion = null;

                using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                {
                    var listaCarpools = linq.SP_BuscarCarpoolPorUsuario2(
                        req.idUsuario,
                        ref idError,
                        ref errorDescripcion
                    ).ToList();

                    if (idError != null && idError > 0)
                    {
                        res.Error.Add(Error.generarError(enumErrores.datosNoEncontrados, errorDescripcion));
                        res.Resultado = false;
                        return res;
                    }

                    int? idError2 = 0;
                    string errorDescripcion2 = null;

                    // Buscar usuarios asociados a cada carpool
                    res.CarpoolList = listaCarpools.Select(carpool =>
                    {
                        var usuarios = linq.SP_BuscarUsuariosPorCarpool(carpool.IdCarpool, ref idError2, ref errorDescripcion2)
                            .Select(u => new ResObtenerCarpoolPorUsuario.UsuariosCarpool
                            {
                                idUsuario = u.ID_USUARIO.ToString(),
                                NombreApellido = $"{u.NombreApellido}"
                            }).ToList();

                        return FactoryCarpoolUsuario(carpool, usuarios);
                    }).ToList();

                    if (idError2 != null && idError2 > 0)
                    {
                        res.Error.Add(Error.generarError(enumErrores.datosNoEncontrados, errorDescripcion2));
                        res.Resultado = false;
                        return res;
                    }

                    res.Resultado = true;
                }
            }
            catch (Exception ex)
            {
                res.Error.Add(Error.generarError(enumErrores.excepcionLogica, ex.Message));
                res.Resultado = false;
            }
            return res;
        }

        #endregion

        #region EDITAR

        //public ResEditarCarpool 

        public ResEditarCarpool Editar(ReqEditarCarpool req)
        {
            ResEditarCarpool res = new ResEditarCarpool();
            res.Error = new List<Error>();

            //Validaciones
            int IdUsuario = req.IdUsuario;
            int IdEvento = req.IdEvento;

            //Opcionales, pueden no ser nulos si el idUsuario es el dueño del carpool.
            int? IdUsuarioOcupante = req.IdUsuarioOcupante;
            string NotasCarpool = req.NotasCarpool;
            int? Provincia = req.Provincia;
            int? Canton = req.Canton;
            int? Distrito = req.Distrito;
            DateTime? HoraSalida = req.HoraSalida;

            //Opcional, puede ser nulo si el idUsuario no es el dueño del carpool.
            int? idCarpool_Nuevo = req.IdCarpool_Nuevo;

            int? ErrorID = 0;
            string ErrorDescripcion = null;

            using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
            {
                linq.SP_EditarCarpool(
                    IdUsuario, 
                    IdEvento, 
                    IdUsuarioOcupante, 
                    NotasCarpool, 
                    Provincia, 
                    Canton, 
                    Distrito, 
                    HoraSalida, 
                    idCarpool_Nuevo, 
                    ref ErrorID, 
                    ref ErrorDescripcion);
            }

            if (ErrorID == null)
            {
                res.Error.Add(Error.generarError(enumErrores.excepcionBaseDatos, ErrorDescripcion));
                res.Resultado = false;
            }
            else
            {
                res.Resultado = true;
                res.Error.Add(Error.generarError(enumErrores.excepcionBaseDatos, ErrorDescripcion)); //borrar esto
            }

            return res;
        }


        #endregion


        #region BORRAR
        //borrar

        #endregion

        #region laFactoriaa!!

        public static ResObtenerCarpoolPorEvento.CarpoolPorEvento_Modelo FactoryCarpool(SP_BuscarCarpoolPorEvento2Result tc, List<ResObtenerCarpoolPorEvento.UsuariosCarpool> usuarios)
        {
            return new ResObtenerCarpoolPorEvento.CarpoolPorEvento_Modelo
            {
                idCarpool = tc.IdCarpool,
                NombreCompletoDueno = tc.NombreCompletoDueno,
                CamposDisponibles = tc.CamposDisponibles,
                NombreEvento = tc.NombreEvento,
                DireccionOrigen = tc.DireccionOrigen,
                DireccionDestino = tc.DireccionDestino,
                HoraSalida = tc.HoraSalida,
                NotasCarpool = tc.NotasCarpool,
                CarpoolUsuarios = usuarios // Se llena la lista de usuarios del carpool
            };
        }

        public static ResObtenerCarpoolPorUsuario.CarpoolPorUsuario_Modelo FactoryCarpoolUsuario(SP_BuscarCarpoolPorUsuario2Result tc, List<ResObtenerCarpoolPorUsuario.UsuariosCarpool> usuarios)
        {
            return new ResObtenerCarpoolPorUsuario.CarpoolPorUsuario_Modelo
            {
                idCarpool = tc.IdCarpool,
                idEvento = tc.IdEvento,
                
                NombreCompletoDueno = tc.NombreCompletoDueno,
                CamposDisponibles = tc.CamposDisponibles,
                CamposRestantes = tc.CamposRestantes,
                CarpoolUsuarios = usuarios, // Se llena la lista de usuarios del carpool

                NombreEvento = tc.NombreEvento,
                NotasCarpool = tc.NotasCarpool,
                DireccionOrigen = tc.DireccionOrigen,
                DireccionDestino = tc.DireccionDestino,
                HoraSalida = tc.HoraSalida,
            };
        }
        #endregion

    }
}
