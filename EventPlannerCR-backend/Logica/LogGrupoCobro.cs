using System;
using System.Collections.Generic;
using System.Linq;
using EventPlannerCR_AccesoDatos;
using EventPlannerCR_backend.Entidades;

namespace EventPlannerCR_backend.Logica
{
    public class LogGrupoCobro
    {
        public ResInsertarGrupoCobro Insertar(ReqInsertarGrupoCobro req)
        {
            ResInsertarGrupoCobro res = new ResInsertarGrupoCobro
            {
                error = new List<Error>()
            };

            if (req != null)
            {
                if (req.IdUsuario == null || req.IdUsuario <= 0)
                {
                    Error error = new Error
                    {
                        ErrorCode = enumErrores.idFaltante,
                        Message = "El id de usuario es nulo o menor a 0."
                    };
                    res.error.Add(error);
                }

                if (req.IdDeuda == null || req.IdDeuda <= 0)
                {
                    Error error = new Error
                    {
                        ErrorCode = enumErrores.idFaltante,
                        Message = "El id de deuda es nulo o menor a 0."
                    };
                    res.error.Add(error);
                }

                if (res.error.Any())
                {
                    Error error = new Error
                    {
                        ErrorCode = enumErrores.requestIncompleto,
                        Message = "El objeto de solicitud es inválido."
                    };
                    res.error.Add(error);
                    res.resultado = false;
                    return res;
                }

                try
                {
                    int? idBd = 0;
                    int? idError = 0;
                    String mensajeError = "";

                    using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                    {
                        linq.SP_InsertarGrupoDeCobro(req.IdUsuario, req.IdDeuda, ref idBd, ref idError,
                            ref mensajeError);
                    }

                    if (idBd == 200)
                    {
                        res.resultado = true;
                    }
                    else
                    {
                        res.resultado = false;
                        Error error = new Error
                        {
                            ErrorCode = enumErrores.datosNoEncontrados,
                            Message = "No se insertó el grupo de cobro."
                        };
                        res.error.Add(error);
                    }
                }
                catch (Exception e)
                {
                    Error error = new Error()
                    {
                        ErrorCode = enumErrores.excepcionLogica,
                        Message = "Error interno: " + e.Message
                    };
                    res.error.Add(error);
                    res.resultado = false;
                }
            }
            else
            {
                Error error = new Error
                {
                    ErrorCode = enumErrores.requestNulo,
                    Message = "El objeto de solicitud es nulo."
                };
                res.error.Add(error);
                res.resultado = false;
                return res;
            }

            return res;
        }

        public ResBorrarGrupoCobro Borrar(ReqBorrarGrupoCobro req)
        {
            ResBorrarGrupoCobro res = new ResBorrarGrupoCobro
            {
                error = new List<Error>()
            };

            if (req != null)
            {
                if (req.idDeuda == null || req.idDeuda <= 0)
                {
                    Error error = new Error
                    {
                        ErrorCode = enumErrores.idFaltante,
                        Message = "El id de deuda es nulo o menor a 0."
                    };
                    res.error.Add(error);
                }

                if (req.idUsuario == null || req.idUsuario <= 0)
                {
                    Error error = new Error
                    {
                        ErrorCode = enumErrores.idFaltante,
                        Message = "El id de usuario es nulo o menor a 0."
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
                    int? idBd = 0;
                    int? idError = 0;
                    String mensajeError = "";

                    using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                    {
                        linq.SP_BorrarGrupoDeCobro(req.idUsuario, req.idDeuda, ref idBd, ref idError,
                            ref mensajeError);
                    }

                    if (idBd > 0)
                    {
                        res.resultado = true;
                    }
                    else
                    {
                        res.resultado = false;
                        Error error = new Error
                        {
                            ErrorCode = enumErrores.datosNoEncontrados,
                            Message = "No se borró el grupo de cobro."
                        };
                        res.error.Add(error);
                    }
                }
                catch (Exception e)
                {
                    Error error = new Error
                    {
                        ErrorCode = enumErrores.excepcionLogica,
                        Message = "Error interno: " + e.Message
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
                    Message = "El objeto de solicitud es nulo."
                };
                res.error.Add(error);
                res.resultado = false;
                return res;
            }

            return res;
        }
    }
}