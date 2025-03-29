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
                
                res.resultado = res.Eventos.Count != 0;
                if (res.resultado)
                {
                    // Se construye el objeto tipo OpenWeather
                    foreach (Evento resEvento in res.Eventos)
                    {
                        OpenWeatherForecastRequest owreq = new OpenWeatherForecastRequest()
                        {
                            lat = resEvento.lat,
                            lon = resEvento.lon,
                            cnt = resEvento.diasFaltantes
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
                            linq.SP_Actualizar_Clima(evento.idEvento, jsonRes, ref idBd, ref idError,
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
                //idEvento = unTipo.IdEvento,
                //FechaInicio = unTipo.FechaInicio,
                //lat = unTipo.lat,
                //lon = unTipo.lon,
                //diasFaltantes = unTipo.DiasParaEvento
            };
            return evento;
        }

        #endregion
        

        #endregion
    }
}