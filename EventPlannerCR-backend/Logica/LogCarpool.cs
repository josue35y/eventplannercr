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


        //buscar por evnento

        #region mi version
        //public ResObtenerCarpoolPorEvento listar(ReqObtenerCarpoolPorEvento req)
        //{
        //    ResObtenerCarpoolPorEvento res = new ResObtenerCarpoolPorEvento();
        //    res.error = new List<Error>();
        //    Error error = new Error();

        //    try
        //    {
        //        if (req.Sesion.Estado == (int)enumEstadoSesion.cerrada)
        //        {
        //            error.ErrorCode = (int)enumErrores.sessionCerrada;
        //            error.Message = "Sesion expirada";

        //            res.error.Add(error);
        //        }
        //        else
        //        {

        //            if (req == null)
        //            {
        //                error.ErrorCode = (int)enumErrores.requestNulo;
        //                error.Message = "Req Null";
        //                res.error.Add(error);
        //            }
        //            else
        //            {
        //                if (req.idEvento < 0) //revisar si puede ser null
        //                {
        //                    error.ErrorCode = (int)enumErrores.idFaltante;
        //                    error.Message = "ID de evento faltante o incorrecto";
        //                    res.error.Add(error);
        //                }


        //                if (res.error.Any())
        //                {
        //                    res.resultado = false;
        //                }

        //                List<SP_BuscarCarpoolPorEventoResult> listaCarpoolPorEventoBD = new List<SP_BuscarCarpoolPorEventoResult>();
        //                int? idError = 0;
        //                string errorDescripcion = null;

        //                using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
        //                {
        //                    linq.SP_BuscarCarpoolPorEvento(
        //                        req.idEvento,
        //                        ref idError,
        //                        ref errorDescripcion
        //                    );
        //                    listaCarpoolPorEventoBD = linq.SP_BuscarCarpoolPorEvento(
        //                        req.idEvento,
        //                        ref idError,
        //                        ref errorDescripcion).ToList();
        //                }

        //                res.CarpoolList = new List<Carpool>();
        //                foreach (SP_BuscarCarpoolPorEventoResult unCarpoolPorEvento in listaCarpoolPorEventoBD)
        //                {
        //                    res.CarpoolList.Add(this.factoryCarpool(unCarpoolPorEvento));
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        error.ErrorCode = (int)enumErrores.excepcionLogica;
        //        error.Message = ex.Message.ToString();
        //        res.error.Add(error);
        //    }
        //    return res;
        //}
        #endregion

        public ResObtenerCarpoolPorEvento listar(ReqObtenerCarpoolPorEvento req)
        {
            ResObtenerCarpoolPorEvento res = new ResObtenerCarpoolPorEvento();
            res.error = new List<Error>();
            Error error = new Error();

            try
            {
                if (req.Sesion.Estado == (int)enumEstadoSesion.cerrada)
                {
                    error.ErrorCode = (int)enumErrores.sessionCerrada;
                    error.Message = "Sesión expirada";
                    res.error.Add(error);
                    res.resultado = false;
                    return res;  // Salimos temprano
                }

                if (req == null)
                {
                    error.ErrorCode = (int)enumErrores.requestNulo;
                    error.Message = "El request es nulo";
                    res.error.Add(error);
                    res.resultado = false;
                    return res; // Salimos temprano
                }

                if (req.idEvento < 0) // Puede haber validaciones adicionales
                {
                    error.ErrorCode = (int)enumErrores.idFaltante;
                    error.Message = "ID de evento faltante o incorrecto";
                    res.error.Add(error);
                    res.resultado = false;
                    return res; // Salimos temprano
                }

                // Variables para manejar la respuesta del SP
                int? idError = 0;
                string errorDescripcion = null;
                List<SP_BuscarCarpoolPorEventoResult> listaCarpoolPorEventoBD;

                // Llamada al SP usando LINQ
                using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                {
                    listaCarpoolPorEventoBD = linq.SP_BuscarCarpoolPorEvento(
                        req.idEvento,
                        ref idError,
                        ref errorDescripcion
                    ).ToList();
                }

                // Si el SP devuelve error, lo manejamos
                if (idError != null && idError > 0)
                {
                    error.ErrorCode = idError.Value;
                    error.Message = errorDescripcion;
                    res.error.Add(error);
                    res.resultado = false;
                    return res;
                }

                // Convertimos los resultados del SP en objetos Carpool
                res.CarpoolList = listaCarpoolPorEventoBD.Select(factoryCarpool).ToList();
                res.resultado = true;
            }
            catch (Exception ex)
            {
                error.ErrorCode = (int)enumErrores.excepcionLogica;
                error.Message = ex.Message;
                res.error.Add(error);
                res.resultado = false;
            }

            return res;
        }

        //borrar

        //editar

        #region laFactoriaa!!
        private Carpool factoryCarpool(SP_BuscarCarpoolPorEventoResult tc)
        {
            //Carpool carpool = new Carpool();

            //carpool.idCarpool = tc.IdCarpool;
            //carpool.Evento.idEvento = tc.IdEvento;
            //carpool.CamposDisponibles = tc.CamposDisponibles;
            //carpool.Provincia = tc.Provincia;
            //carpool.Canton = tc.Canton;
            //carpool.Distrito = tc.Distrito;

            //return carpool;
            return new Carpool
            {
                idCarpool = tc.IdCarpool,
                //Evento = new Evento { idEvento = tc.IdEvento }, 
                CamposDisponibles = tc.CamposDisponibles,
                Provincia = tc.Provincia,
                Canton = tc.Canton,
                Distrito = tc.Distrito
            };
        }

        #endregion

    }
}
