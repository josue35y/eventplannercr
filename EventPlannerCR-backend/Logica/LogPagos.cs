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
                Error = new List<Error>(),
            };

            if (req != null)
            {
                if (req.idDeuda == null || req.idDeuda <= 0)
                {
                    Error Error = new Error
                    {
                        ErrorCode = enumErrores.requestIncompleto,
                        Message = "El idDeuda es nulo o menor a 0"
                    };
                    res.Error.Add(Error);
                }

                if (req.idUsuario == null || req.idUsuario <= 0)
                {
                    Error Error = new Error
                    {
                        ErrorCode = enumErrores.requestIncompleto,
                        Message = "El idUsuario es nulo o menor a 0"
                    };
                    res.Error.Add(Error);
                }

                if (res.Error.Any())
                {
                    res.Resultado = false;
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
                        res.Resultado = true;
                    }
                    else
                    {
                        Error Error = new Error
                        {
                            ErrorCode = enumErrores.excepcionBaseDatos,
                            Message = "Error al actualizar el pago del deudor: " + errorMessage
                        };
                        res.Error.Add(Error);
                        res.Resultado = false;
                    }
                }
                catch (Exception e)
                {
                    Error Error = new Error
                    {
                        ErrorCode = enumErrores.excepcionLogica,
                        Message = "Error al actualizar el pago del deudor: " + e.Message
                    };
                    res.Error.Add(Error);
                    res.Resultado = false;
                    return res;
                }
            }
            else
            {
                Error Error = new Error
                {
                    ErrorCode = enumErrores.requestNulo,
                    Message = "El request es nulo"
                };
                res.Error.Add(Error);
                res.Resultado = false;
                return res;
            }

            return res;
        }

        public ResActualizarPagoDueno ActualizarPagoDueno(ReqActualizarPagoDueno req)
        {
            ResActualizarPagoDueno res = new ResActualizarPagoDueno
            {
                Error = new List<Error>(),
            };
            
            if (req != null)
            {
                if (req.idDeuda == null || req.idDeuda <= 0)
                {
                    Error Error = new Error
                    {
                        ErrorCode = enumErrores.requestIncompleto,
                        Message = "El idDeuda es nulo o menor a 0"
                    };
                    res.Error.Add(Error);
                }

                if (req.idUsuario == null || req.idUsuario <= 0)
                {
                    Error Error = new Error
                    {
                        ErrorCode = enumErrores.requestIncompleto,
                        Message = "El idUsuario es nulo o menor a 0"
                    };
                    res.Error.Add(Error);
                }

                if (res.Error.Any())
                {
                    res.Resultado = false;
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
                        res.Resultado = true;
                    }
                    else
                    {
                        Error Error = new Error
                        {
                            ErrorCode = enumErrores.excepcionBaseDatos,
                            Message = "Error al actualizar el pago del dueño: " + errorMessage
                        };
                        res.Error.Add(Error);
                        res.Resultado = false;
                    }
                }
                catch (Exception e)
                {
                    Error Error = new Error
                    {
                        ErrorCode = enumErrores.excepcionLogica,
                        Message = "Error al actualizar el pago del dueño: " + e.Message
                    };
                    res.Error.Add(Error);
                    res.Resultado = false;
                    return res;
                }
            }
            else
            {
                Error Error = new Error
                {
                    ErrorCode = enumErrores.requestNulo,
                    Message = "El request es nulo"
                };
                res.Error.Add(Error);
                res.Resultado = false;
                return res;
            }

            return res;
        }

        public ResBuscarPagosPendientes PagosPendientes(ReqBuscarPagosPendientes req)
        {
            ResBuscarPagosPendientes res = new ResBuscarPagosPendientes
            {
                Error = new List<Error>(),
                PagosPendientes = new List<Pagos>()
            };

            if (req != null)
            {
                if (req.idUsuario == null || req.idUsuario <= 0)
                {
                    Error Error = new Error
                    {
                        ErrorCode = enumErrores.requestIncompleto,
                        Message = "El idUsuario es nulo o menor a 0"
                    };
                    res.Error.Add(Error);
                    res.Resultado = false;
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
                        res.Resultado = true;
                    }
                    else
                    {
                        Error Error = new Error
                        {
                            ErrorCode = enumErrores.excepcionBaseDatos,
                            Message = "No se encontraron pagos pendientes"
                        };
                        res.Error.Add(Error);
                        res.Resultado = false;
                        return res;
                    }

                }
                catch (Exception e)
                {
                    Error Error = new Error
                    {
                        ErrorCode = enumErrores.excepcionLogica,
                        Message = "Error al buscar los pagos pendientes: " + e.Message
                    };
                    res.Error.Add(Error);
                    res.Resultado = false;
                    return res;
                }
            }
            else
            {
                Error Error = new Error
                {
                    ErrorCode = enumErrores.requestNulo,
                    Message = "El request es nulo"
                };
                res.Error.Add(Error);
                res.Resultado = false;
                return res;
            }

            return res;
        }
    }
}