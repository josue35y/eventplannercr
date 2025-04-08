using System;
using System.Collections.Generic;
using System.Linq;
using EventPlannerCR_AccesoDatos;
using EventPlannerCR_backend.Entidades;
using EventPlannerCR_Gateway.Controllers;
using EventPlannerCR_Gateway.Models.Request;
using EventPlannerCR_Gateway.Models.Response;
using Newtonsoft.Json;

namespace EventPlannerCR_backend.Logica
{
    public class LogEvento
    {
        #region Timer task para buscar los eventos cercanos

        public void BuscarEventosCercanos()
        {
            ResEventosCercanos res = new ResEventosCercanos
            {
                error = new List<Error>(),
                Eventos = new List<Evento>()
            };
            Error error = new Error();
            try
            {
                using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                {
                    List<SP_EventosCercanosResult> complejo = new List<SP_EventosCercanosResult>();
                    complejo = linq.SP_EventosCercanos().ToList();
                    foreach (SP_EventosCercanosResult unTipo in complejo)
                    {
                        res.Eventos.Add(this.FactoriaEvento(unTipo));
                    }
                }
                
                res.Resultado = res.Eventos.Count != 0;
                if (res.Resultado)
                {
                    // Se construye el objeto tipo OpenWeather
                    foreach (Evento resEvento in res.Eventos)
                    {
                        OpenWeatherForecastRequest owreq = new OpenWeatherForecastRequest()
                        {
                            lat = resEvento.Latitud,
                            lon = resEvento.Longitud,
                            cnt = resEvento.DiasFaltantes
                        };
                        EventoController eventoController = new EventoController();
                        ActualizarClima(eventoController.ConsultarEventosCercanos(owreq), resEvento);
                    }
                }
            }
            catch (Exception ex)
            {
                LogBitacora.RegistrarBitacora("LogEvento", "BuscarEventosCercanos", "Info",
                    enumErrores.excepcionBaseDatos.ToString(),
                    ex.Message, null, res.ToString());
                LogBitacora.RegistrarBitacora("LogEvento", "BuscarEventosCercanos", "Info", enumErrores.excepcionBaseDatos.ToString(),
                ex.Message, null, res.ToString());
                error.Message = ex.Message;
                error.ErrorCode = enumErrores.excepcionBaseDatos;
                res.error.Add(error);
            }
        }

        public ResInsertarEvento InsertarEvento(ReqInsertarEvento req) {

            //Creacion de instancias generales del método
            ResInsertarEvento res = new ResInsertarEvento();
            res.error = new List<Error>();

            //inicio de manejo de excepciones
            try
            {

                //validación del request
                if (req == null)
                {
                    Error error = new Error();

                    error.ErrorCode = enumErrores.requestNulo;
                    error.Message = "Req Null";
                    res.error.Add(error);
                }

                //Validación del nombre del nuevo usuario para evitar nulos o espacio en blanco
                if (req != null && String.IsNullOrEmpty(req.Evento.Nombre))
                {
                    Error error = new Error();

                    //Acumula la respuesta de error
                    error.ErrorCode = enumErrores.nombreFaltante;
                    error.Message = "Nombre nulo o en blanco";
                    res.error.Add(error);
                }
                
                 //Validación de la fecha de inicio del nuevo usuario para evitar nulos o default (que pasa si es una fecha anterior al día de hoy)
                if (req.Evento.FechaInicio == null || req.Evento.FechaInicio == default || 
                    req.Evento.FechaInicio < DateTime.Now.Date)
                {
                    Error error = new Error();

                    error.ErrorCode = enumErrores.FechaInicioEventoFaltante;
                    error.Message = "Fecha inicio de evento nula o no válida";
                    res.error.Add(error);
                }

                //Validación de la fecha de nacimiento del nuevo usuario para evitar nulos
                if (req.Evento.FechaFin == null || req.Evento.FechaFin == default 
                    || req.Evento.FechaFin <= DateTime.Now.Date)
                {
                    Error error = new Error();

                    error.ErrorCode = enumErrores.FechaFinalEventoFaltante;
                    error.Message = "Fecha fin de evento nula o no válida";
                    res.error.Add(error);
                }

                //Validación del correo del nuevo usuario para evitar nulos o espacio en blanco
                if (String.IsNullOrEmpty(req.Evento.Lugar))
                {
                    Error error = new Error();

                    //Acumula la respuesta de error
                    error.ErrorCode = enumErrores.LugarFaltante;
                    error.Message = "Lugar Nulo o faltante";
                    res.error.Add(error);
                }

                //Validación del apellido del nuevo usuario para evitar nulos o espacio en blanco
                if (String.IsNullOrEmpty(req.Evento.Descripcion))
                {
                    Error error = new Error();

                    //Acumula la respuesta de error
                    error.ErrorCode = enumErrores.DescripcionFaltante;
                    error.Message = "Descripcion del evento nula o faltante";
                    res.error.Add(error);
                }

                //Se valida si hubo errores en todas las validaciones
                if (res.error.Any())
                {
                    res.Resultado = false;
                }
                //Si no hubo errores se agrega el usuario a la base de datos
                else
                {

                    int? idBd = 0;
                    int? idError = 0;
                    string errorDescripcion = null;

                    using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                    {
                        linq.SP_InsertarEvento(
                            req.Evento.Nombre, req.Evento.FechaInicio, req.Evento.FechaFin,
                            req.Evento.Lugar, req.Evento.Provincia, req.Evento.Canton, 
                            req.Evento.Distrito, (decimal)req.Evento.Latitud, (decimal)req.Evento.Longitud, 
                            ref idBd, ref idError, ref errorDescripcion);
                    }

                    res.Resultado = true;

                }
            }

            //Manejo de excepciones
            catch (Exception ex)
            {
                Error error = new Error();

                error.ErrorCode = enumErrores.excepcionLogica;
                error.Message = ex.ToString();
                res.error.Add(error);
            }

            //Retorno de la respuesta
            return res;
        }

        #region Guardar Clima Evento

        private void ActualizarClima(OpenWeatherForecastResponse res, Evento evento)
        {
            int? idBd = 0;
            int? idError = 0;
            string errorDescripcion = null;
            if (res.cod == "200")
            {
                try
                {
                    string jsonRes = JsonConvert.SerializeObject(res);
                        using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                        {
                            linq.SP_Actualizar_Clima(evento.IdEvento, jsonRes, ref idBd, ref idError,
                                ref errorDescripcion);
                        }
                }
                catch (Exception ex)
                {
                    LogBitacora.RegistrarBitacora("LogEvento", "ActualizarClima", "WARNING",
                        enumErrores.excepcionBaseDatos.ToString(),
                        ex.Message, null, res.ToString());
                }
            }
        }

        #endregion


        #region Factoria Evento Cercano

        private Evento FactoriaEvento(SP_EventosCercanosResult unTipo)
        {
            
            Evento evento = new Evento
            {
                IdEvento = unTipo.IdEvento,
                FechaInicio = unTipo.FechaInicio,
                Latitud = unTipo.lat,
                Longitud = unTipo.lon,
                DiasFaltantes = unTipo.DiasParaEvento
            };
            return evento;
        }

        #endregion
        

        #endregion
    }
}