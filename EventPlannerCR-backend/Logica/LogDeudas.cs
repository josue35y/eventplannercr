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
                    Error error = new Error();
                    error.ErrorCode = enumErrores.idFaltante;
                    error.Message = "Id Usuario Faltante";
                    res.error.Add(error);
                }

                {
                    Error error = new Error();
                    error.ErrorCode = enumErrores.idFaltante;
                    error.Message = "Usuario Faltante";
                    res.error.Add(error);
                }

                if (String.IsNullOrEmpty(req.Deuda.Motivo))
                {
                    Error error = new Error();
                    error.ErrorCode = enumErrores.DescripcionFaltante;
                    error.Message = "Motivo Faltante";
                    res.error.Add(error);
                }

                if (req.Deuda.Total <= 0 || req.Deuda.Total == null)
                {
                    Error error = new Error();
                    error.ErrorCode = enumErrores.DescripcionFaltante;
                    error.Message = "Monto total Faltante";
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
                Error error = new Error();
                error.ErrorCode = enumErrores.requestNulo;
                error.Message = "Req Null";
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
                    Error error = new Error();
                    error.ErrorCode = enumErrores.idFaltante;
                    error.Message = "Id Deuda Faltante";
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
                Error error = new Error();
                error.ErrorCode = enumErrores.requestNulo;
                error.Message = "Req Null";
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
            
        }

        #endregion

        #region factoria
        
        private 

        #endregion
    }
}