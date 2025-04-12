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

        //Logica insertar evento
        public ResInsertarEvento InsertarEvento(ReqInsertarEvento req)
        {

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
                    res.resultado = false;
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

                        if (idBd < 0)
                        {

                            Error error = new Error();
                            error.ErrorCode = enumErrores.excepcionInsertarEvento;
                            error.Message = "Error al insertar Evento";
                            res.error.Add(error);
                            res.resultado = false;
                        }
                        else { 
                            res.resultado = true;
                        }
                    }

                    if (req.Imagen != null) {

                        using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                        {
                            linq.SP_InsertarImagenEvento(req.Evento.IdEvento, req.Evento.Nombre, req.Evento.FechaInicio, req.Evento.FechaFin, req.Evento.Lugar,
                                req.Evento.Imagen, ref idBd, ref idError, ref errorDescripcion);

                            if (idBd < 0)
                            {

                                Error error = new Error();
                                error.ErrorCode = enumErrores.excepcionInsertarEvento;
                                error.Message = "Error al insertar Imagen";
                                res.error.Add(error);
                                res.resultado = false;
                            }
                            else
                            {
                                res.resultado = true;
                            }
                        }
                    }
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

        //Logica lista de eventos
        public ResListaEventos ListaEventos(ReqListaEventos req)
        {

            ResListaEventos res = new ResListaEventos();
            res.ListaEventos = new List<Evento>();
            res.error = new List<Error>();

            try
            {

                using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                {
                    LogFactorias Factorias = new LogFactorias();

                    List<SP_Lista_EventosResult> tc = linq.SP_Lista_Eventos().ToList();

                    res.ListaEventos = Factorias.ListaEventos(tc);

                    res.resultado = true;
                }
            }
            catch (Exception ex)
            {

                Error error = new Error();

                error.ErrorCode = enumErrores.excepcionListaUsuarios;
                error.Message = ex.ToString();
                res.error.Add(error);
            }

            return res;
        }

        //logica Buscar de eventos

        public ResBuscarEvento BuscarEvento(ReqBuscarEvento req)
        {
            ResBuscarEvento res = new ResBuscarEvento();
            res.ListaEventos = new List<Evento>();
            res.error = new List<Error>();
            try
            {
                using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                {
                    LogFactorias Factorias = new LogFactorias();

                    int? idBd = 0;
                    int? idError = 0;
                    string errorDescripcion = null;

                    List<SP_Buscar_EventoResult> tc = linq.SP_Buscar_Evento(req.Evento.Nombre, 
                        req.Evento.Lugar,ref idBd, ref idError, ref errorDescripcion).ToList();
                    res.ListaEventos = Factorias.BuscarEvento(tc);
                    res.resultado = true;
                }
            }
            catch (Exception ex)
            {
                Error error = new Error();
                error.ErrorCode = enumErrores.excepcionListaUsuarios;
                error.Message = ex.ToString();
                res.error.Add(error);
            }
            return res;
        }

        //Logica Actualizar Evento
        public ResActualizarEvento ActualizarEvento(ReqActualizarEvento req)
        {
            //Creacion de instancias generales del método
            ResActualizarEvento res = new ResActualizarEvento();
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
                    res.resultado = false;
                }

                if (!res.error.Any())
                {
                    int? idBd = 0;
                    int? idError = 0;
                    string errorDescripcion = null;

                    using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                    {

                        linq.SP_ActualizarEvento(req.Evento.IdEvento, req.Evento.Nombre, req.Evento.FechaInicio,
                            req.Evento.FechaFin, req.Evento.Lugar, req.Evento.Descripcion, req.Evento.Clima, req.Evento.Latitud,
                            req.Evento.Longitud, req.Evento.Provincia, req.Evento.Canton, req.Evento.Distrito, ref idBd, ref idError,
                            ref errorDescripcion);

                        res.resultado = true;
                    }

                    if (req.Imagen != null)
                    {
                        req.Evento.Imagen = Convert.ToBase64String(req.Imagen);

                        using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                        {
                            linq.SP_ActualizarImagenEvento(req.Evento.IdEvento, req.Evento.Imagen, ref idBd, ref idError, ref errorDescripcion);

                            if (idBd < 0)
                            {

                                Error error = new Error();
                                error.ErrorCode = enumErrores.excepcionInsertarEvento;
                                error.Message = "Error al insertar Imagen";
                                res.error.Add(error);
                                res.resultado = false;
                            }
                            else
                            {
                                res.resultado = true;
                            }
                        }
                    }
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

        //Logica Eliminar Evento
        public ResEliminarEvento EliminarEvento(ReqEliminarEvento req) {

            ResEliminarEvento res = new ResEliminarEvento();
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
                    res.resultado = false;
                }

                if (!res.error.Any())
                {
                    int? idBd = 0;
                    int? idError = 0;
                    string errorDescripcion = null;

                    using (ConexionLinqDataContext linq = new ConexionLinqDataContext())
                    {

                        linq.SP_Eliminar_Evento(req.Evento.IdEvento, ref idBd, ref idError,
                            ref errorDescripcion);

                        res.resultado = true;
                    }
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

            return res;
        }


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