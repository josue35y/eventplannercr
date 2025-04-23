using EventPlannerCR_AccesoDatos;
using EventPlannerCR_backend.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace EventPlannerCR_backend.Logica
{   
    public class LogAsistencia
    {
        #region INSERTAR
        public ResInsertarAsistencia Insertar(ReqInsertarAsistencia req)
        {
            
            ResInsertarAsistencia res = new ResInsertarAsistencia();
            res.Error = new List<Error>();
            Error error = new Error();

            try
            {
                if (req == null)
                {
                    res.Error.Add(Error.generarError(enumErrores.requestNulo, "Req nulo."));
                }
                else
                {
                    if (req.Asistencia.Usuario == null)
                    {
                        req.Asistencia.Usuario = new Usuario { IdUsuario = 0 };
                    }
                    if (req.Asistencia.Evento == null)
                    {
                        req.Asistencia.Evento = new Evento { IdEvento = 0 };
                    }
                    if (req.Asistencia.Carpool == null)
                    {
                        req.Asistencia.Carpool = new Carpool { idCarpool = 0 };
                    }
                    if (req.Asistencia.Usuario.IdUsuario <= 0)
                    {
                        res.Error.Add(Error.generarError(enumErrores.idFaltante, "ID de Usuario faltante o invalido."));
                    }
                    if (req.Asistencia.Evento.IdEvento <= 0)
                    {
                        res.Error.Add(Error.generarError(enumErrores.idFaltante, "ID de evento faltante o invalido."));
                    }
                    int? idBD = 0;
                    int? idError = 0;
                    string errorDescripcion = null;
                    bool status = true;
                    if (res.Error.Any())
                    {
                        res.Resultado = false;
                        return res;
                    }
                    else
                    {
                        using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                        {
                            linq.SP_InsertarAsistencia(
                                req.Asistencia.Usuario.IdUsuario,
                                req.Asistencia.Evento.IdEvento,
                                status,
                                req.Asistencia.Carpool.idCarpool,
                                ref idBD,
                                ref idError,
                                ref errorDescripcion);
                        }
                        if (idBD >= 1)
                        {
                            if (idError == 4)
                            {
                                res.Resultado = false;
                                res.Error.Add(Error.generarError(enumErrores.excepcionBaseDatos, "El Usuario ya se encuentra inscrito en el evento."));
                                return res;
                            }
                            res.Resultado = true;
                        }
                        else
                        {
                            res.Resultado = false;
                            res.Error.Add(Error.generarError(enumErrores.excepcionBaseDatos, errorDescripcion));
                            return res;
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
        public ResBuscarAsistenciaUsuario BuscarAsistenciaUsuario(ReqBuscarAsistenciaUsuario req)
        {
            ResBuscarAsistenciaUsuario res = new ResBuscarAsistenciaUsuario();
            res.Error = new List<Error>();

            try
            {
                int? ErrorId = 0;
                string ErrorDescripcion = null;

                List<SP_BuscarAsistenciaPorUsuarioResult> listaAsistenciaUsuarioBD;

                if (req == null)
                {
                    res.Error.Add(Error.generarError(enumErrores.requestNulo, "Req nulo."));
                }
                else
                {
                    if (req.idUsuario <= 0)
                    {
                        res.Error.Add(Error.generarError(enumErrores.idFaltante, "ID de Usuario faltante o invalido."));
                    }
                    if (res.Error.Any())
                    {
                        res.Resultado = false;
                        return res;
                    }
                    else
                    {
                        using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                        {

                            listaAsistenciaUsuarioBD = linq.SP_BuscarAsistenciaPorUsuario(
                                req.idUsuario, 
                                ref ErrorId, 
                                ref ErrorDescripcion).ToList();
                        }
                        if (listaAsistenciaUsuarioBD == null || !listaAsistenciaUsuarioBD.Any())
                        {
                            res.Error.Add(Error.generarError(enumErrores.datosNoEncontrados, "No se encontraron asistencias."));
                            res.Resultado = false;
                            return res;
                        }
                        if (ErrorId != null && ErrorId > 0)
                        {
                            res.Error.Add(Error.generarError(enumErrores.excepcionBaseDatos, ErrorDescripcion));
                            res.Resultado = false;
                            return res;
                        }
                        else
                        {
                            res.ListaAsistenciaUsuario = listaAsistenciaUsuarioBD
                           .Select(tc =>
                           {
                               try
                               {
                                   return FactoryBuscarAsistenciaUsuario(tc);
                               }
                               catch (Exception innerEx)
                               {
                                   res.Error.Add(Error.generarError(enumErrores.errorConversion, $"Error al convertir asistencia: {innerEx.Message}"));
                                   return null;
                               }
                           })
                           .Where(a => a != null)
                           .ToList();
                        }
                        res.Resultado = true;
                    }
                }
            }
            catch (Exception ex)
            {
                res.Error.Add(Error.generarError(enumErrores.excepcionLogica, ex.ToString()));
            }
            return res;
        }
        public ResBuscarAsistenciaEvento BuscarAsistenciaEvento(ReqBuscarAsistenciaEvento req)
        {
            ResBuscarAsistenciaEvento res = new ResBuscarAsistenciaEvento();
            res.Error = new List<Error>();

            try
            {
                int? ErrorId = 0;
                string ErrorDescripcion = null;

                List<SP_BuscarAsistenciaPorEventoResult> listaAsistenciaEventoBD;

                if (req == null)
                {
                    res.Error.Add(Error.generarError(enumErrores.requestNulo, "Req nulo."));
                }
                else
                {
                    if (req.idEvento <= 0)
                    {
                        res.Error.Add(Error.generarError(enumErrores.idFaltante, "ID de evento faltante o invalido."));
                    }
                    if (res.Error.Any())
                    {
                        res.Resultado = false;
                        return res;
                    }
                    else
                    {
                        using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                        {

                            listaAsistenciaEventoBD = linq.SP_BuscarAsistenciaPorEvento(
                                req.idEvento,
                                ref ErrorId,
                                ref ErrorDescripcion).ToList();
                        }
                        if (listaAsistenciaEventoBD == null || !listaAsistenciaEventoBD.Any())
                        {
                            res.Error.Add(Error.generarError(enumErrores.datosNoEncontrados, "No se encontraron asistencias."));
                            res.Resultado = false;
                            return res;
                        }
                        if (ErrorId != null && ErrorId > 0)
                        {
                            res.Error.Add(Error.generarError(enumErrores.excepcionBaseDatos, ErrorDescripcion));
                            res.Resultado = false;
                            return res;
                        }
                        else
                        {
                            res.ListaAsistenciaEvento = listaAsistenciaEventoBD
                           .Select(tc =>
                           {
                               try
                               {
                                   return FactoryBuscarAsistenciaEvento(tc);
                               }
                               catch (Exception innerEx)
                               {
                                   res.Error.Add(Error.generarError(enumErrores.errorConversion, $"Error al convertir asistencia: {innerEx.Message}"));
                                   return null;
                               }
                           })
                           .Where(a => a != null)
                           .ToList();
                        }
                        res.Resultado = true;
                    }
                }
            }
            catch (Exception ex)
            {
                res.Error.Add(Error.generarError(enumErrores.excepcionLogica, ex.ToString()));
            }
            return res;
        }


        public ResObtenerIdEventoDesdeAsistencia ObtenerIdEventoDesdeAsistencia(ReqObtenerIdEventoDesdeAsistencia req)
        {
            ResObtenerIdEventoDesdeAsistencia res = new ResObtenerIdEventoDesdeAsistencia();
            res.Error = new List<Error>();
            try
            {
                int? ErrorId = 0;
                string ErrorDescripcion = null;
                if (req == null)
                {
                    res.Error.Add(Error.generarError(enumErrores.requestNulo, "Req nulo."));
                }
                else
                {
                    if (req.IdAsistencia <= 0 || req.IdAsistencia == null)
                    {
                        res.Error.Add(Error.generarError(enumErrores.requestIncompleto, "ID de asistencia faltante o invalido."));
                    }
                    if (res.Error.Any())
                    {
                        res.Resultado = false;
                        return res;
                    }
                    else
                    {
                        res.IdEvento = ObtenerIdEventoDesdeAsistencia(req.IdAsistencia);
                        if (res.IdEvento <= 0)
                        {
                            res.Error.Add(Error.generarError(enumErrores.datosNoEncontrados, "No se encontraron datos para la asistencia editada."));
                            res.Resultado = false;
                            return res;
                        }
                        else
                        {
                            res.Resultado = true;
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


        public int ObtenerIdEventoDesdeAsistencia(int idAsistencia)
        {
            int? idEvento = 0;

            using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
            {
                linq.SP_ObtenerEventoDesdeAsistencia(
                     idAsistencia,
                     ref idEvento);
            }

            return (int)idEvento;
        }

        #endregion

        #region EDITAR
        public ResEditarAsistencia Editar(ReqEditarAsistencia req)
        {

            ResEditarAsistencia res = new ResEditarAsistencia();
            res.Error = new List<Error>();

            try
            {
                int? ErrorId = 0;
                string ErrorDescripcion = null;

                if (req == null)
                {
                    res.Error.Add(Error.generarError(enumErrores.requestNulo, "Req nulo."));
                    res.Resultado = false;
                    return res;
                }
                else 
                {
                    if (req.idAsistencia <= 0 || req.idAsistencia == null)
                    {
                        res.Error.Add(Error.generarError(enumErrores.requestIncompleto, "Asistencia nula."));
                        res.Resultado = false;
                        res.idAsistencia = req.idAsistencia;
                        return res;
                    }
                    else
                    {
                        if (req.Estado == false)
                        {
                            //Si el Usuario no asistirá al evento, se le asigna un valor nulo a la carpool.
                            req.idCarpool = null;
                        }
                        
                        if (req.Estado == null)
                        {
                            res.Error.Add(Error.generarError(enumErrores.requestIncompleto, "El valor de estado es requerido."));
                            res.Resultado = false;
                            res.idAsistencia = req.idAsistencia;
                            return res;
                        }
                        else
                        {
                            SP_EditarAsistenciaResult AsistenciaBD;
                            using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                            {
                                var resultado = linq.SP_EditarAsistencia(
                                    req.idAsistencia,
                                    req.Estado,
                                    req.idCarpool,
                                    ref ErrorId,
                                    ref ErrorDescripcion
                                );
                                AsistenciaBD = resultado.FirstOrDefault();
                            }
                            if (AsistenciaBD != null)
                            {
                                res.Resultado = true;
                                res = FactoryEditarAsistencia(AsistenciaBD);
                            }
                            else
                            {
                                res.Error.Add(Error.generarError(enumErrores.datosNoEncontrados, "No se encontraron datos para la asistencia editada."));
                            }
                        }
                    }                                  
                }
            }
            catch(Exception ex)
            {
                res.Error.Add(Error.generarError(enumErrores.excepcionLogica, ex.ToString()));
            }
            return res;
        }
        #endregion

        #region BORRAR
        public ResBorrarAsistencia Borrar(ReqBorrarAsistencia req)
        {

            ResBorrarAsistencia res = new ResBorrarAsistencia();
            res.Error = new List<Error>();
            _ = new Error();

            try
            {
                int? ErrorId = 0;
                string ErrorDescripcion = null;

                if (req.Sesion.Usuario.Admin == false)
                {
                    res.Error.Add(Error.generarError(enumErrores.noAutorizado, "El Usuario no tiene permisos para realizar esta acción."));
                    res.Resultado = false;
                    return res;
                }
		
                if (req == null)
                {
                    res.Error.Add(Error.generarError(enumErrores.requestNulo, "Req nulo."));
                    res.Resultado = false;
                    return res;
                }
                else 
                {
                    if (req.idAsistencia <= 0 || req.idAsistencia == null)
                    {
                        res.Error.Add(Error.generarError(enumErrores.requestIncompleto, "Asistencia nula."));
                        res.Resultado = false;
                        return res;
                    }
                    else
                    {
                        using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                        {
                            var resultado = linq.SP_BorrarAsistencia(
                                req.idAsistencia,
                                ref ErrorId,
                                ref ErrorDescripcion
                            );
                        }
                        if (ErrorId == null || ErrorId == 0)
                        {
                            res.Resultado = true;
                        }
                        else
                        {
                            res.Error.Add(Error.generarError(enumErrores.datosNoEncontrados, "No se encontraron datos para la asistencia editada."));
                        } 
                    }                                  
                }
            }
            catch(Exception ex)
            {
                res.Error.Add(Error.generarError(enumErrores.excepcionLogica, ex.ToString()));
            }
            return res;
        }
        #endregion

        #region laFactoriaa!!
        public static ResBuscarAsistenciaUsuario.ResBuscarAsistenciaUsuario_Modelo FactoryBuscarAsistenciaUsuario(SP_BuscarAsistenciaPorUsuarioResult tc)
        {
            if (tc == null)
            {
                Error.generarError(enumErrores.requestIncompleto, "El Resultado de SP_BuscarAsistenciaUsuario es nulo.");
            }

            return new ResBuscarAsistenciaUsuario.ResBuscarAsistenciaUsuario_Modelo
            {
                idAsistencia = tc.IdAsistencia,
                NombreCompleto = tc.NombreCompleto,
                NombreEvento = tc.NombreEvento,
                DescripcionEvento = tc.DescripcionEvento,
                FechaEvento = tc.FechaEvento,
                LugarEvento = tc.LugarEvento,
                Trasnporte = tc.Transporte,
                Estado = tc.Estado,
                ConfirmacionAsistencia = (DateTime)tc.ConfirmacionAsistencia,
            };
        }
        public static ResBuscarAsistenciaEvento.ResBuscarAsistenciaEvento_Modelo FactoryBuscarAsistenciaEvento(SP_BuscarAsistenciaPorEventoResult tc)
        {
            if (tc == null)
            {
                Error.generarError(enumErrores.requestIncompleto, "El Resultado de SP_BuscarAsistenciaEvento es nulo.");
            }

            return new ResBuscarAsistenciaEvento.ResBuscarAsistenciaEvento_Modelo
            {
                idAsistencia = tc.IdAsistencia,
                NombreCompleto = tc.NombreCompleto,
                Trasnporte = tc.Transporte,
                ConfirmacionAsistencia = (DateTime)tc.ConfirmacionAsistencia,
                Estado = tc.Estado,
            };
        }
        public static ResEditarAsistencia FactoryEditarAsistencia(SP_EditarAsistenciaResult tc)
        {
            if (tc == null)
            {
                Error.generarError(enumErrores.requestIncompleto, "El Resultado de SP_EditarAsistencia es nulo.");
            }
            return new ResEditarAsistencia
            {
                idAsistencia = tc.IdAsistencia,
                NombreCompleto = tc.NombreCompleto,
                NombreEvento = tc.NombreEvento,
                DescripcionEvento = tc.DescripcionEvento,
                FechaEvento = tc.FechaEvento,
                LugarEvento = tc.LugarEvento,
                Trasnporte = tc.Transporte, 
                Estado = tc.Estado,
                ConfirmacionAsistencia = (DateTime)tc.ConfirmacionAsistencia,
            };
        }

        #endregion
    }
}
