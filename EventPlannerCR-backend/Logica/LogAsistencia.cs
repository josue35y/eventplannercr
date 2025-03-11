using EventPlannerCR_AccesoDatos;
using EventPlannerCR_backend.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EventPlannerCR_backend.Logica
{   
    public class LogAsistencia
    {
        #region INSERTAR
        public ResInsertarAsistencia Insertar(ReqInsertarAsistencia req)
        {
            
            ResInsertarAsistencia res = new ResInsertarAsistencia();
            res.error = new List<Error>();
            Error error = new Error();

            try
            {
                if (req == null)
                {
                    res.error.Add(Error.generarError(enumErrores.requestNulo, "Req nulo."));
                }
                else
                {
                    if (req.Asistencia.Usuario == null)
                    {
                        req.Asistencia.Usuario = new Usuario { idUsuario = 0 };
                    }
                    if (req.Asistencia.Evento == null)
                    {
                        req.Asistencia.Evento = new Evento { idEvento = 0 };
                    }
                    if (req.Asistencia.Carpool == null)
                    {
                        req.Asistencia.Carpool = new Carpool { idCarpool = 0 };
                    }
                    if (req.Asistencia.Usuario.idUsuario <= 0)
                    {
                        res.error.Add(Error.generarError(enumErrores.idFaltante, "ID de usuario faltante o invalido."));
                    }
                    if (req.Asistencia.Evento.idEvento <= 0)
                    {
                        res.error.Add(Error.generarError(enumErrores.idFaltante, "ID de evento faltante o invalido."));
                    }
                    int? idBD = 0;
                    int? idError = 0;
                    string errorDescripcion = null;
                    bool status = true;
                    if (res.error.Any())
                    {
                        res.resultado = false;
                        return res;
                    }
                    else
                    {
                        using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                        {
                            linq.SP_InsertarAsistencia(
                                req.Asistencia.Usuario.idUsuario,
                                req.Asistencia.Evento.idEvento,
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
                                res.resultado = false;
                                res.error.Add(Error.generarError(enumErrores.excepcionBaseDatos, "El usuario ya se encuentra inscrito en el evento."));
                                return res;
                            }
                            res.resultado = true;
                        }
                        else
                        {
                            res.resultado = false;
                            res.error.Add(Error.generarError(enumErrores.excepcionBaseDatos, errorDescripcion));
                            return res;
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
        #endregion

        #region BUSCAR
        public ResBuscarAsistencia Buscar(ReqBuscarAsistencia req)
        {
            ResBuscarAsistencia res = new ResBuscarAsistencia();
            res.error = new List<Error>();

            try
            {
                if (req == null)
                {
                    res.error.Add(Error.generarError(enumErrores.requestNulo, "Req Null"));
                    return res;
                }
                else
                {
                    if (req.Asistencia.Usuario == null)
                    {
                        res.error.Add(Error.generarError(enumErrores.requestIncompleto, "El objeto Usuario dentro de Asistencia es nulo."));
                        req.Asistencia.Usuario = new Usuario { idUsuario = 0 };
                    }
                    if (req.Asistencia.Evento == null)
                    {
                        res.error.Add(Error.generarError(enumErrores.requestIncompleto, "El objeto Evento dentro de Asistencia es nulo."));
                        req.Asistencia.Evento = new Evento { idEvento = 0 };
                    }
                    if (req.Asistencia.Carpool == null)
                    {
                        res.error.Add(Error.generarError(enumErrores.requestIncompleto, "El objeto Carpool dentro de Asistencia es nulo."));
                        req.Asistencia.Carpool = new Carpool{ idCarpool = 0 };
                    }
                }
                int? IdUsuario = req.Asistencia.Usuario?.idUsuario;
                int? IdEvento = req.Asistencia.Evento?.idEvento;
                int? FkCarpool = req.Asistencia.Carpool?.idCarpool;
                int? idError = 0;
                string errorDescripcion = null;
                List<SP_BuscarAsistenciaAV2Result> listaAsistenciaBD;
                using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                {
                    listaAsistenciaBD = linq.SP_BuscarAsistenciaAV2(
                        IdUsuario, IdEvento, FkCarpool,
                        ref idError,
                        ref errorDescripcion
                    ).ToList();
                }
                if (idError != null && idError > 0)
                {
                    res.error.Add(Error.generarError(enumErrores.excepcionBaseDatos, errorDescripcion));
                    res.resultado = false;
                    return res;
                }
                else
                {
                    res.ListaAsistencia = listaAsistenciaBD
                   .Select(tc =>
                   {
                       try
                       {
                           return factoryAsistencia(tc);
                       }
                       catch (Exception innerEx)
                       {
                           res.error.Add(Error.generarError(enumErrores.errorConversion, $"Error al convertir asistencia: {innerEx.Message}"));
                           return null;
                       }
                   })
                   .Where(a => a != null)
                   .ToList();
                }
                if (listaAsistenciaBD == null || !listaAsistenciaBD.Any())
                {
                    res.error.Add(Error.generarError(enumErrores.datosNoEncontrados, "No se encontraron asistencias."));
                    res.resultado = false;
                    return res;
                }
                res.resultado = true;
            }
            catch (Exception ex)
            {
                res.error.Add(Error.generarError(enumErrores.excepcionLogica, ex.ToString()));
            }
            return res;
        }
        #endregion




        #region BORRAR
        //borrar

        #endregion




        #region EDITAR
        //editar

        #endregion




        #region laFactoriaa!!
        public static Asistencia factoryAsistencia(SP_BuscarAsistenciaAV2Result tc)
        {
            if (tc == null)
            {
                Error.generarError(enumErrores.requestIncompleto, "El resultado de SP_BuscarAsistenciaAVResult es nulo.");
            }

            if (tc.FkCarpool == null)
            {
                return new Asistencia
                {
                    idAsistencia = tc.IdAsistencia,
                    Status = tc.Status,
                    Usuario = tc.IdUsuario != 0
                        ? new Usuario { idUsuario = tc.IdUsuario }
                        : null,
                    Evento = tc.IdEvento != 0
                        ? new Evento { idEvento = tc.IdEvento }
                        : null,
                    Carpool = null,
                };
            }
            else
            {
                return new Asistencia
                {
                    idAsistencia = tc.IdAsistencia,
                    Status = tc.Status,
                    Usuario = tc.IdUsuario != 0
                        ? new Usuario { idUsuario = tc.IdUsuario }
                        : null,
                    Evento = tc.IdEvento != 0
                        ? new Evento { idEvento = tc.IdEvento }
                        : null,
                    Carpool = tc.FkCarpool != 0
                        ? new Carpool { idCarpool = (int)tc.FkCarpool }
                        : null,
                };
            }
        }
        #endregion
    }
}
