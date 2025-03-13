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
        //Insertar un grupo carpool
        public ResInsertarCarpool insertar(ReqInsertarCarpool req)
        {
            ResInsertarCarpool res = new ResInsertarCarpool();
            res.error = new List<Error>();
            Error error = new Error(); 
            try
            {
                if (req == null)
                {
                    res.error.Add(Error.generarError(enumErrores.requestNulo, "El request es nulo."));
                }
                else
                {
                    if (req.Carpool.Evento == null || req.Carpool.Evento.idEvento == null || req.Carpool.Evento.idEvento <= 0)
                    {
                        res.error.Add(Error.generarError(enumErrores.idFaltante, "ID de evento faltante, incorrecto o nulo."));
                    }
                    if (req.Carpool.CamposDisponibles <= 0 || req.Carpool.CamposDisponibles == null )
                    {                        
                        res.error.Add(Error.generarError(enumErrores.camposDisponiblesFaltante, "Es necesario indicar la cantidad de campos disponibles."));
                    }
                    if (req.Carpool.Provincia <= 0 || req.Carpool.Provincia == null)
                    {
                        res.error.Add(Error.generarError(enumErrores.provinciaFaltante, "Es necesario indicar la provincia."));
                    }
                    if (req.Carpool.Canton <= 0 || req.Carpool.Canton == null)
                    {
                        res.error.Add(Error.generarError(enumErrores.cantonFaltante, "Es necesario indicar el cantón."));
                    }
                    if (req.Carpool.Distrito <= 0 || req.Carpool.Distrito == null )
                    {
                        res.error.Add(Error.generarError(enumErrores.distritoFaltante, "Es necesario indicar el distrito."));
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
                                req.Carpool.idUsuario,
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
                            res.error.Add(Error.generarError(enumErrores.excepcionBaseDatos, errorDescripcion));
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


        //Buscar por evento
        public ResObtenerCarpoolPorEvento listar(ReqObtenerCarpoolPorEvento req)
        {
            ResObtenerCarpoolPorEvento res = new ResObtenerCarpoolPorEvento();
            res.error = new List<Error>();
            Error error = new Error();

            try
            {
                if (req.Sesion.Estado == enumEstadoSesion.cerrada)
                {
                    res.error.Add(Error.generarError(enumErrores.sessionCerrada, "Sesion expirada."));
                    res.resultado = false;
                    return res;  
                }

                if (req == null)
                {
                    res.error.Add(Error.generarError(enumErrores.requestNulo, "El request es nulo"));
                    res.resultado = false;
                    return res; 
                }

                if (req.idEvento < 0) // Puede haber validaciones adicionales
                {
                    res.error.Add(Error.generarError(enumErrores.idFaltante, "El request es nulo"));
                    res.resultado = false;
                    return res; 
                }

                int? idError = 0;
                string errorDescripcion = null;
                
                List<SP_BuscarCarpoolPorEventoResult> listaCarpoolPorEventoBD = new List<SP_BuscarCarpoolPorEventoResult>();
                using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                {
                    listaCarpoolPorEventoBD = linq.SP_BuscarCarpoolPorEvento(
                        req.idEvento,
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

                res.CarpoolList = listaCarpoolPorEventoBD.Select(factoryCarpool).ToList();
                res.resultado = true;
            }
            catch (Exception ex)
            {
                res.error.Add(Error.generarError(enumErrores.excepcionLogica, ex.Message));
                res.resultado = false;
            }
            return res;
        }

        //editar

        //borrar

        #region laFactoriaa!!
        private Carpool factoryCarpool(SP_BuscarCarpoolPorEventoResult tc/*, ReqObtenerCarpoolPorEvento req*/)
        {
            return new Carpool
            {
                idCarpool = tc.IdCarpool,
                Evento = new Evento { idEvento = tc.IdEvento/*,
                    Nombre = req.idEvento
                */},
                CamposDisponibles = tc.CamposDisponibles,
                Provincia = tc.Provincia,
                Canton = tc.Canton,
                Distrito = tc.Distrito
            };
        }

        #endregion

    }
}
