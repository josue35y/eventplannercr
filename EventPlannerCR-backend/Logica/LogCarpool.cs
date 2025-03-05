using EventPlannerCR_AccesoDatos;
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

        public ResInsertarCarpool insertar(ReqInsertarCarpool req)
        {
            ResInsertarCarpool res = new ResInsertarCarpool();
            res.error = new List<Error>();
            Error error = new Error();

            try
            {
                if (req == null)
                {
                    error.ErrorCode = (int)enumErrores.requestNulo;
                    error.Message = "Req Null";
                    res.error.Add(error);
                }
                else
                {
                    if (req.Carpool.Evento.idEvento < 0) //revisar si puede ser null
                    {
                        error.ErrorCode = (int)enumErrores.idFaltante;
                        error.Message = "ID de evento faltante";
                        res.error.Add(error) ;
                    }
                    else if (req.Carpool.CamposDisponibles < 0)
                    {
                        error.ErrorCode = (int)enumErrores.camposDisponiblesFaltante;
                        error.Message = "Id de evento faltante";
                        res.error.Add(error);
                    }
                    else if (req.Carpool.Provincia < 0)
                    {
                        error.ErrorCode = (int)enumErrores.provinciaFaltante;
                        error.Message = "Id de evento faltante";
                        res.error.Add(error);
                    }
                    else if (req.Carpool.Canton < 0)
                    {
                        error.ErrorCode = (int)enumErrores.cantonFaltante;
                        error.Message = "Id de evento faltante";
                        res.error.Add(error);
                    }
                    else if (req.Carpool.Distrito < 0)
                    {
                        error.ErrorCode = (int)enumErrores.distritoFaltante;
                        error.Message = "Id de evento faltante";
                        res.error.Add(error);
                    }

                    int? idBD = 0;
                    int? idError = 0;
                    string errorDescripcion = null;

                    if (res.error.Any())
                    {
                        res.resultado = false;
                    }
                    else 
                    {
                        using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                        {
                            linq.SP_InsertarCarpool(
                                req.Carpool.Evento.idEvento,
                                req.Carpool.CamposDisponibles,
                                req.Carpool.Provincia,
                                req.Carpool.Canton,
                                req.Carpool.Distrito,
                                ref idBD,
                                ref idError,
                                ref errorDescripcion);
                        }
                        if (idBD <= 1)
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


        //buscar

        //borrar

        //editar

    }
}
