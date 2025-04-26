using System;
using System.Collections.Generic;
using System.Linq;
using EventPlannerCR_AccesoDatos;
using EventPlannerCR_backend.Entidades;

namespace EventPlannerCR_backend.Logica
{
    public class LogDeudas
    {
        #region Insertar

        public ResInsertarDeuda Insertar(ReqInsertarDeuda req)
        {
            ResInsertarDeuda res = new ResInsertarDeuda
            {
                Error = new List<Error>()
            };

            Usuario usuario = new Usuario
            {
                IdUsuario = req.Deuda.Usuario.IdUsuario
            };

            if (req != null)
            {
                if (req.Deuda.Usuario.IdUsuario <= 0 || req.Deuda.Usuario.IdUsuario == null)
                {
                    Error Error = new Error
                    {
                        ErrorCode = enumErrores.idFaltante,
                        Message = "Id Usuario Faltante"
                    };
                    res.Error.Add(Error);
                }

                if (String.IsNullOrEmpty(req.Deuda.Motivo))
                {
                    Error Error = new Error
                    {
                        ErrorCode = enumErrores.DescripcionFaltante,
                        Message = "Motivo Faltante"
                    };
                    res.Error.Add(Error);
                }

                if (req.Deuda.Total <= 0 || req.Deuda.Total == null)
                {
                    Error Error = new Error
                    {
                        ErrorCode = enumErrores.DescripcionFaltante,
                        Message = "Monto total Faltante"
                    };
                    res.Error.Add(Error);
                }

                if (res.Error.Any())
                {
                    res.Resultado = false;
                    return res;
                }

                int? idReturn = 0;
                int? errorId = 0;
                String errorMessage = "";

                try
                {
                    using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                    {
                        linq.SP_InsertarDeuda(req.Deuda.Usuario.IdUsuario, req.Deuda.Total, req.Deuda.Motivo,
                            ref idReturn, ref errorId, ref errorMessage);
                    }

                    if (idReturn != null && idReturn > 0)
                    {
                        res.idDeuda = idReturn;
                        res.Resultado = true;
                    }
                    else
                    {
                        res.Resultado = false;
                        Error Error = new Error
                        {
                            ErrorCode = enumErrores.datosNoEncontrados,
                            Message = "No se encontraron datos"
                        };
                        res.Error.Add(Error);
                    }
                }
                catch (Exception e)
                {
                    res.Resultado = false;
                    res.Error.Add(Error.generarError(enumErrores.excepcionBaseDatos, errorMessage + " " + e.Message));
                    return res;
                }
            }
            else
            {
                Error Error = new Error
                {
                    ErrorCode = enumErrores.requestNulo,
                    Message = "Req Null"
                };
                res.Error.Add(Error);
                res.Resultado = false;
                return res;
            }

            return res;
        }

        #endregion

        #region Borrar

        public ResBorrarDeuda Borrar(ReqBorrarDeuda req)
        {
            ResBorrarDeuda res = new ResBorrarDeuda
            {
                Error = new List<Error>()
            };
            if (req != null)
            {
                if (req.Deuda.idDeuda <= 0 || req.Deuda.idDeuda == null)
                {
                    Error Error = new Error
                    {
                        ErrorCode = enumErrores.idFaltante,
                        Message = "Id Deuda Faltante"
                    };
                    res.Error.Add(Error);
                }

                if (res.Error.Any())
                {
                    res.Resultado = false;
                    return res;
                }

                int? idReturn = 0;
                int? errorId = 0;
                String errorMessage = "";

                try
                {
                    using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                    {
                        linq.SP_BorrarDeuda(req.Deuda.idDeuda, ref idReturn, ref errorId, ref errorMessage);
                    }

                    if (errorId != null && errorId == 0)
                    {
                        res.Resultado = true;
                    }
                    else
                    {
                        res.Resultado = false;
                        Error Error = new Error
                        {
                            ErrorCode = enumErrores.datosNoEncontrados,
                            Message = "No se encontraron datos"
                        };
                        res.Error.Add(Error);
                    }
                }
                catch (Exception e)
                {
                    res.Resultado = false;
                    res.Error.Add(Error.generarError(enumErrores.excepcionBaseDatos, errorMessage + " " + e.Message));
                    return res;
                }
            }
            else
            {
                Error Error = new Error
                {
                    ErrorCode = enumErrores.requestNulo,
                    Message = "Req Null"
                };
                res.Error.Add(Error);
                res.Resultado = false;
                return res;
            }


            return res;
        }

        #endregion

        #region BuscarDeuda

        public ResBuscarDeuda Buscar(ReqBuscarDeuda req)
        {
            ResBuscarDeuda res = new ResBuscarDeuda
            {
                Error = new List<Error>(),
                Deudor = new List<Deudor>()
            };

            if (req != null)
            {
                if (req.Deuda.idDeuda <= 0 || req.Deuda.idDeuda == null)
                {
                    Error Error = new Error
                    {
                        ErrorCode = enumErrores.idFaltante,
                        Message = "Id Deudor Faltante"
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
                    LogFactorias Factorias = new LogFactorias();
                    using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                    {
                        List<SP_BuscarDeudaResult> tc = linq.SP_BuscarDeuda(req.Deuda.idDeuda).ToList();
                        res.Deudor = Factorias.BuscarDeuda(tc);
                    }

                    if (res.Deudor.Any())
                    {
                        res.Resultado = true;
                    }
                    else
                    {
                        res.Resultado = false;
                        Error Error = new Error
                        {
                            ErrorCode = enumErrores.datosNoEncontrados,
                            Message = "No se encontraron datos"
                        };
                        res.Error.Add(Error);
                    }
                }
                catch (Exception e)
                {
                    res.Resultado = false;
                    res.Error.Add(Error.generarError(enumErrores.excepcionBaseDatos, e.Message));
                    return res;
                }
            }
            else
            {
                Error Error = new Error
                {
                    ErrorCode = enumErrores.requestNulo,
                    Message = "Req Null"
                };
                res.Error.Add(Error);
                res.Resultado = false;
                return res;
            }

            return res;
        }

        #endregion

        #region BuscarDeudaDueño

        public ResBuscarDeudaDueno BuscarDueno(ReqBuscarDeudaDueno req)
        {
            ResBuscarDeudaDueno res = new ResBuscarDeudaDueno
            {
                Error = new List<Error>(),
                Deuda = new List<Deuda>()
            };

            if (req != null)
            {
                if (req.idUsuario <= 0 || req.idUsuario == null)
                {
                    Error Error = new Error
                    {
                        ErrorCode = enumErrores.idFaltante,
                        Message = "Id Dueño Faltante"
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
                    LogFactorias Factorias = new LogFactorias();

                    using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                    {
                        List<SP_BuscarDeudasDueñoResult> tc = linq.SP_BuscarDeudasDueño(req.idUsuario).ToList();
                        res.Deuda = Factorias.BuscarDeudaDueno(tc);
                    }

                    if (res.Deuda.Any())
                    {
                        res.Resultado = true;
                    }
                    else
                    {
                        res.Resultado = false;
                        Error Error = new Error
                        {
                            ErrorCode = enumErrores.datosNoEncontrados,
                            Message = "No se encontraron datos"
                        };
                        res.Error.Add(Error);
                    }
                }
                catch (Exception e)
                {
                    res.Resultado = false;
                    res.Error.Add(Error.generarError(enumErrores.excepcionBaseDatos, e.Message));
                    return res;
                }
            }
            else
            {
                Error Error = new Error
                {
                    ErrorCode = enumErrores.requestNulo,
                    Message = "Req Null"
                };
                res.Error.Add(Error);
                res.Resultado = false;
                return res;
            }

            return res;
        }

        #endregion

        #region BuscarDeudaPorUsuario

        public ResBuscarDeudaUsuario BuscarDeudaUsuario(ReqBuscarDeudaUsuario req)
        {
            ResBuscarDeudaUsuario res = new ResBuscarDeudaUsuario
            {
                Error = new List<Error>(),
                Deudas = new List<Deudor>()
            };

            if (req != null)
            {
                if (req.idUsuario == null || req.idUsuario <= 0)
                {
                    Error Error = new Error
                    {
                        ErrorCode = enumErrores.idFaltante,
                        Message = "Id Usuario Faltante"
                    };
                    res.Error.Add(Error);
                    res.Resultado = false;
                    return res;
                }

                try
                {
                    LogFactorias Factorias = new LogFactorias();

                    using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                    {
                        List<SP_BuscarDeudasPorUsuarioResult> tc = linq.SP_BuscarDeudasPorUsuario(req.idUsuario)
                            .ToList();
                        res.Deudas = Factorias.BuscarDeudaPorUsuario(tc);
                    }
                }
                catch (Exception e)
                {
                    res.Resultado = false;
                    res.Error.Add(Error.generarError(enumErrores.excepcionBaseDatos, e.Message));
                    return res;
                }
            }
            else
            {
                Error Error = new Error
                {
                    ErrorCode = enumErrores.requestNulo,
                    Message = "Req Null"
                };
                res.Error.Add(Error);
                res.Resultado = false;
                return res;
            }

            return res;
        }

        #endregion
    }
}