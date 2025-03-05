using EventPlannerCR_AccesoDatos;
using EventPlannerCR_backend.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlannerCR_backend.Logica
{   
    public class LogAsistencia
    {
        public ResInsertarAsistencia insertar(ReqInsertarAsistencia req)
        {
            
            ResInsertarAsistencia res = new ResInsertarAsistencia();
            res.error = new List<Error>();
            Error error = new Error();

            try
            {
                if (req == null)
                {
                    error.ErrorCode = (int)enumErrores.requestNulo;
                    error.Message = "Req Null";
                    res.error.Add(error);

                    //bitacora?

                }
                else
                {

                    if (req.Asistencia.Usuario.idUsuario < 0 )
                    {
                        error.ErrorCode = (int)enumErrores.idFaltante;
                        error.Message = "ID de usuario faltante";
                        res.error.Add(error);
                    }
                    else if (req.Asistencia.Evento.idEvento < 0)
                    {
                        error.ErrorCode = (int)enumErrores.idFaltante;
                        error.Message = "ID de evento faltante";
                        res.error.Add(error);
                    }
                    else if (req.Asistencia.Carpool.idCarpool < 0)
                    {
                        error.ErrorCode = (int)enumErrores.idFaltante;
                        error.Message = "ID de carpool faltante";
                        res.error.Add(error);
                    }

                    int? idBD = 0;
                    int? idError = 0;
                    string errorDescripcion = null;
                    bool status = true;

                    if (res.error.Any())
                    {
                        res.resultado = false;
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
                            res.resultado = true;
                        }
                        else
                        {
                            error.Message = errorDescripcion;
                            res.error.Add(error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                error.ErrorCode = (int)enumErrores.excepcionLogica;
                error.Message = ex.ToString();
                res.error.Add(error);
            }         
            return res;
        }

    }
}
