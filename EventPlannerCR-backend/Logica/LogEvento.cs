using System;
using System.Collections.Generic;
using System.Linq;
using EventPlannerCR_AccesoDatos;
using EventPlannerCR_backend.Entidades;

namespace EventPlannerCR_backend.Logica
{
    public class LogEvento
    {
        #region Timer task para buscar los eventos cercanos

        public ResEventosCercanos BuscarEventosCercanos()
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
            }
            catch (Exception ex)
            {
                LogBitacora.RegistrarBitacora("LogEvento", "BuscarEventosCercanos", "Info",
                    enumErrores.excepcionBaseDatos.ToString(),
                    ex.Message, null, res.ToString());
                error.Message = ex.Message;
                error.ErrorCode = (int)enumErrores.excepcionBaseDatos;
                res.error.Add(error);
            }

            return res;
        }

        #region Guardar Clima Evento

        public void ActualizarClima(ResEventosCercanos res)
        {
            int? idBd = 0;
            int? idError = 0;
            string errorDescripcion = null;
            if (res.resultado = true)
            {
                try
                {
                    foreach (Evento evento in res.Eventos)
                    {
                        using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                        {
                            linq.SP_Actualizar_Clima(evento.idEvento, evento.Clima, ref idBd, ref idError,
                                ref errorDescripcion);
                        }
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
                idEvento = unTipo.IdEvento,
                FechaInicio = unTipo.FechaInicio,
                lat = unTipo.lat,
                lon = unTipo.lon,
                diasFaltantes = unTipo.DiasParaEvento
            };
            return evento;
        }

        #endregion

        #endregion
    }
}