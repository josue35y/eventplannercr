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
                error = new List<Error>()
            };

            if (req != null)
            {
                if (req.Deuda.Usuario.IdUsuario <= 0 || req.Deuda.Usuario.IdUsuario == null)
                {
                    Error error = new Error
                    {
                        ErrorCode = enumErrores.idFaltante,
                        Message = "Id Usuario Faltante"
                    };
                    res.error.Add(error);
                }

                {
                    Error error = new Error
                    {
                        ErrorCode = enumErrores.idFaltante,
                        Message = "Usuario Faltante"
                    };
                    res.error.Add(error);
                }

                if (String.IsNullOrEmpty(req.Deuda.Motivo))
                {
                    Error error = new Error
                    {
                        ErrorCode = enumErrores.DescripcionFaltante,
                        Message = "Motivo Faltante"
                    };
                    res.error.Add(error);
                }

                if (req.Deuda.Total <= 0 || req.Deuda.Total == null)
                {
                    Error error = new Error
                    {
                        ErrorCode = enumErrores.DescripcionFaltante,
                        Message = "Monto total Faltante"
                    };
                    res.error.Add(error);
                }

                if (res.error.Any())
                {
                    res.resultado = false;
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
                }
                catch (Exception e)
                {
                    res.resultado = false;
                    res.error.Add(Error.generarError(enumErrores.excepcionBaseDatos, errorMessage + " " + e.Message));
                    return res;
                }
            }
            else
            {
                Error error = new Error
                {
                    ErrorCode = enumErrores.requestNulo,
                    Message = "Req Null"
                };
                res.error.Add(error);
                res.resultado = false;
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
                error = new List<Error>()
            };
            if (req != null)
            {
                if (req.Deuda.idDeuda <= 0 || req.Deuda.idDeuda == null)
                {
                    Error error = new Error
                    {
                        ErrorCode = enumErrores.idFaltante,
                        Message = "Id Deuda Faltante"
                    };
                    res.error.Add(error);
                }

                if (res.error.Any())
                {
                    res.resultado = false;
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
                }
                catch (Exception e)
                {
                    res.resultado = false;
                    res.error.Add(Error.generarError(enumErrores.excepcionBaseDatos, errorMessage + " " + e.Message));
                    return res;
                }
            }
            else
            {
                Error error = new Error
                {
                    ErrorCode = enumErrores.requestNulo,
                    Message = "Req Null"
                };
                res.error.Add(error);
                res.resultado = false;
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
                error = new List<Error>(),
                Deudor = new List<Deudor>()
            };

            if (req != null)
            {
                if (req.Deuda.idDeuda <= 0 || req.Deuda.idDeuda == null)
                {
                    Error error = new Error
                    {
                        ErrorCode = enumErrores.idFaltante,
                        Message = "Id Deudor Faltante"
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
                    LogFactorias Factorias = new LogFactorias();
                    using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                    {
                        List<SP_BuscarDeudaResult> tc = linq.SP_BuscarDeuda(req.Deuda.idDeuda).ToList();
                        res.Deudor = Factorias.BuscarDeuda(tc);
                    }

                    if (res.Deudor.Any())
                    {
                        res.resultado = true;
                    }
                    else
                    {
                        res.resultado = false;
                        Error error = new Error
                        {
                            ErrorCode = enumErrores.datosNoEncontrados,
                            Message = "No se encontraron datos"
                        };
                        res.error.Add(error);
                    }
                }
                catch (Exception e)
                {
                    res.resultado = false;
                    res.error.Add(Error.generarError(enumErrores.excepcionBaseDatos, e.Message));
                    return res;
                }
            }
            else
            {
                Error error = new Error
                {
                    ErrorCode = enumErrores.requestNulo,
                    Message = "Req Null"
                };
                res.error.Add(error);
                res.resultado = false;
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
                error = new List<Error>(),
                Deuda = new List<Deuda>()
            };

            if (req != null)
            {
                if (req.idUsuario <= 0 || req.idUsuario == null)
                {
                    Error error = new Error
                    {
                        ErrorCode = enumErrores.idFaltante,
                        Message = "Id Dueño Faltante"
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
                    LogFactorias Factorias = new LogFactorias();

                    using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                    {
                        List<SP_BuscarDeudasDueñoResult> tc = linq.SP_BuscarDeudasDueño(req.idUsuario).ToList();
                        res.Deuda = Factorias.BuscarDeudaDueno(tc);
                    }

                    if (res.Deuda.Any())
                    {
                        res.resultado = true;
                    }
                    else
                    {
                        res.resultado = false;
                        Error error = new Error
                        {
                            ErrorCode = enumErrores.datosNoEncontrados,
                            Message = "No se encontraron datos"
                        };
                        res.error.Add(error);
                    }
                }
                catch (Exception e)
                {
                    res.resultado = false;
                    res.error.Add(Error.generarError(enumErrores.excepcionBaseDatos, e.Message));
                    return res;
                }
            }
            else
            {
                Error error = new Error
                {
                    ErrorCode = enumErrores.requestNulo,
                    Message = "Req Null"
                };
                res.error.Add(error);
                res.resultado = false;
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
                error = new List<Error>(),
                Deudas = new List<Deudor>()
            };

            if (req != null)
            {
                if (req.idUsuario == null || req.idUsuario <= 0)
                {
                    Error error = new Error
                    {
                        ErrorCode = enumErrores.idFaltante,
                        Message = "Id Usuario Faltante"
                    };
                    res.error.Add(error);
                    res.resultado = false;
                    return res;
                }

                try
                {
                    LogFactorias Factorias = new LogFactorias();

                    using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                    {
                        List<SP_BuscarDeudasPorUsuarioResult> tc = linq.SP_BuscarDeudasPorUsuario(req.idUsuario).ToList();
                        res.Deudas = Factorias.BuscarDeudaPorUsuario(tc);
                    }
                }
                catch (Exception e)
                {
                    res.resultado = false;
                    res.error.Add(Error.generarError(enumErrores.excepcionBaseDatos, e.Message));
                    return res;
                }
            }
            else
            {
                Error error = new Error
                {
                    ErrorCode = enumErrores.requestNulo,
                    Message = "Req Null"
                };
                res.error.Add(error);
                res.resultado = false;
                return res;
            }

            return res;
        }

        #endregion
        
    }
}