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
                Error = new List<Error>()
            };

            if (req != null)
            {
                if (req.IdUsuario == null || req.IdUsuario <= 0)
                {
                    Error Error = new Error
                    {
                        ErrorCode = enumErrores.idFaltante,
                        Message = "El id de usuario es nulo o menor a 0."
                    };
                    res.Error.Add(Error);
                }

                if (req.IdDeuda == null || req.IdDeuda <= 0)
                {
                    Error Error = new Error
                    {
                        ErrorCode = enumErrores.idFaltante,
                        Message = "El id de deuda es nulo o menor a 0."
                    };
                    res.Error.Add(Error);
                }

                if (res.Error.Any())
                {
                    Error Error = new Error
                    {
                        ErrorCode = enumErrores.requestIncompleto,
                        Message = "El objeto de solicitud es inválido."
                    };
                    res.Error.Add(Error);
                    res.Resultado = false;
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
                        res.Resultado = true;
                    }
                    else
                    {
                        res.Resultado = false;
                        Error Error = new Error
                        {
                            ErrorCode = enumErrores.datosNoEncontrados,
                            Message = "No se insertó el grupo de cobro."
                        };
                        res.Error.Add(Error);
                    }
                }
                catch (Exception e)
                {
                    Error Error = new Error()
                    {
                        ErrorCode = enumErrores.excepcionLogica,
                        Message = "Error interno: " + e.Message
                    };
                    res.Error.Add(Error);
                    res.Resultado = false;
                }
            }
            else
            {
                Error Error = new Error
                {
                    ErrorCode = enumErrores.requestNulo,
                    Message = "El objeto de solicitud es nulo."
                };
                res.Error.Add(Error);
                res.Resultado = false;
                return res;
            }

            return res;
        }

        public ResBorrarGrupoCobro Borrar(ReqBorrarGrupoCobro req)
        {
            ResBorrarGrupoCobro res = new ResBorrarGrupoCobro
            {
                Error = new List<Error>()
            };

            if (req != null)
            {
                if (req.idDeuda == null || req.idDeuda <= 0)
                {
                    Error Error = new Error
                    {
                        ErrorCode = enumErrores.idFaltante,
                        Message = "El id de deuda es nulo o menor a 0."
                    };
                    res.Error.Add(Error);
                }

                if (req.idUsuario == null || req.idUsuario <= 0)
                {
                    Error Error = new Error
                    {
                        ErrorCode = enumErrores.idFaltante,
                        Message = "El id de usuario es nulo o menor a 0."
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
                        res.Resultado = true;
                    }
                    else
                    {
                        res.Resultado = false;
                        Error Error = new Error
                        {
                            ErrorCode = enumErrores.datosNoEncontrados,
                            Message = "No se borró el grupo de cobro."
                        };
                        res.Error.Add(Error);
                    }
                }
                catch (Exception e)
                {
                    Error Error = new Error
                    {
                        ErrorCode = enumErrores.excepcionLogica,
                        Message = "Error interno: " + e.Message
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
                    Message = "El objeto de solicitud es nulo."
                };
                res.Error.Add(Error);
                res.Resultado = false;
                return res;
            }

            return res;
        }
    }
}