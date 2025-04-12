using System;
using System.Collections.Generic;
using System.Linq;
using EventPlannerCR_AccesoDatos;
using EventPlannerCR_backend.Entidades;

namespace EventPlannerCR_backend.Logica
{
    public class LogPagos
    {
        public ResActualizarPagoDeudor ActualizarPagoDeudor(ReqActualizarPagoDeudor req)
        {
            ResActualizarPagoDeudor res = new ResActualizarPagoDeudor
            {
                error = new List<Error>(),
            };

            if (req != null)
            {
                if (req.idDeuda == null || req.idDeuda <= 0)
                {
                    Error error = new Error
                    {
                        ErrorCode = enumErrores.requestIncompleto,
                        Message = "El idDeuda es nulo o menor a 0"
                    };
                    res.error.Add(error);
                }

                if (req.idUsuario == null || req.idUsuario <= 0)
                {
                    Error error = new Error
                    {
                        ErrorCode = enumErrores.requestIncompleto,
                        Message = "El idUsuario es nulo o menor a 0"
                    };
                    res.error.Add(error);
                }

                if (res.error.Any())
                {
                    res.resultado = false;
                    return res;
                }

                try
                {
                    int? idBD = 0;
                    int? idError = 0;
                    String errorMessage = "";

                    using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                    {
                        linq.SP_Actualizar_Pago_Deudor(req.idDeuda, req.idUsuario, ref idBD, ref idError,
                            ref errorMessage);
                    }

                    if (idBD == 200)
                    {
                        res.resultado = true;
                    }
                    else
                    {
                        Error error = new Error
                        {
                            ErrorCode = enumErrores.excepcionBaseDatos,
                            Message = "Error al actualizar el pago del deudor: " + errorMessage
                        };
                        res.error.Add(error);
                        res.resultado = false;
                    }
                }
                catch (Exception e)
                {
                    Error error = new Error
                    {
                        ErrorCode = enumErrores.excepcionLogica,
                        Message = "Error al actualizar el pago del deudor: " + e.Message
                    };
                    res.error.Add(error);
                    res.resultado = false;
                    return res;
                }
            }
            else
            {
                Error error = new Error
                {
                    ErrorCode = enumErrores.requestNulo,
                    Message = "El request es nulo"
                };
                res.error.Add(error);
                res.resultado = false;
                return res;
            }

            return res;
        }

        public ResActualizarPagoDueno ActualizarPagoDueno(ReqActualizarPagoDueno req)
        {
            ResActualizarPagoDueno res = new ResActualizarPagoDueno
            {
                error = new List<Error>(),
            };
            
            if (req != null)
            {
                if (req.idDeuda == null || req.idDeuda <= 0)
                {
                    Error error = new Error
                    {
                        ErrorCode = enumErrores.requestIncompleto,
                        Message = "El idDeuda es nulo o menor a 0"
                    };
                    res.error.Add(error);
                }

                if (req.idUsuario == null || req.idUsuario <= 0)
                {
                    Error error = new Error
                    {
                        ErrorCode = enumErrores.requestIncompleto,
                        Message = "El idUsuario es nulo o menor a 0"
                    };
                    res.error.Add(error);
                }

                if (res.error.Any())
                {
                    res.resultado = false;
                    return res;
                }

                try
                {
                    int? idBD = 0;
                    int? idError = 0;
                    String errorMessage = "";

                    using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                    {
                        linq.SP_Actualizar_Pago_Dueño(req.idDeuda, req.idUsuario, ref idBD, ref idError,
                            ref errorMessage);
                    }

                    if (idBD == 200)
                    {
                        res.resultado = true;
                    }
                    else
                    {
                        Error error = new Error
                        {
                            ErrorCode = enumErrores.excepcionBaseDatos,
                            Message = "Error al actualizar el pago del dueño: " + errorMessage
                        };
                        res.error.Add(error);
                        res.resultado = false;
                    }
                }
                catch (Exception e)
                {
                    Error error = new Error
                    {
                        ErrorCode = enumErrores.excepcionLogica,
                        Message = "Error al actualizar el pago del dueño: " + e.Message
                    };
                    res.error.Add(error);
                    res.resultado = false;
                    return res;
                }
            }
            else
            {
                Error error = new Error
                {
                    ErrorCode = enumErrores.requestNulo,
                    Message = "El request es nulo"
                };
                res.error.Add(error);
                res.resultado = false;
                return res;
            }

            return res;
        }

        public ResBuscarPagosPendientes PagosPendientes(ReqBuscarPagosPendientes req)
        {
            ResBuscarPagosPendientes res = new ResBuscarPagosPendientes
            {
                error = new List<Error>(),
                PagosPendientes = new List<Pagos>()
            };

            if (req != null)
            {
                if (req.idUsuario == null || req.idUsuario <= 0)
                {
                    Error error = new Error
                    {
                        ErrorCode = enumErrores.requestIncompleto,
                        Message = "El idUsuario es nulo o menor a 0"
                    };
                    res.error.Add(error);
                    res.resultado = false;
                    return res;
                }

                try
                {
                    LogFactorias logFactorias = new LogFactorias();
                    using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                    {
                        List<SP_BuscarPagosPendientesResult> tc = linq.SP_BuscarPagosPendientes(req.idUsuario).ToList();
                        res.PagosPendientes = logFactorias.BuscarPagos(tc);
                    }

                    if (res.PagosPendientes.Any())
                    {
                        res.resultado = true;
                    }
                    else
                    {
                        Error error = new Error
                        {
                            ErrorCode = enumErrores.excepcionBaseDatos,
                            Message = "No se encontraron pagos pendientes"
                        };
                        res.error.Add(error);
                        res.resultado = false;
                        return res;
                    }

                }
                catch (Exception e)
                {
                    Error error = new Error
                    {
                        ErrorCode = enumErrores.excepcionLogica,
                        Message = "Error al buscar los pagos pendientes: " + e.Message
                    };
                    res.error.Add(error);
                    res.resultado = false;
                    return res;
                }
            }
            else
            {
                Error error = new Error
                {
                    ErrorCode = enumErrores.requestNulo,
                    Message = "El request es nulo"
                };
                res.error.Add(error);
                res.resultado = false;
                return res;
            }

            return res;
        }
    }
}