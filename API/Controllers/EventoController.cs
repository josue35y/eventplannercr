using EventPlannerCR_backend.Entidades;
using EventPlannerCR_backend.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class EventoController : ApiController
    {
        // Http post Insertar evento Api controller

        [HttpPost]
        [Route("api/evento/Insertar")]
        public ResInsertarEvento InsertarEvento(ReqInsertarEvento req)
        {
            return new LogEvento().InsertarEvento(req);
        }


        //Lista de eventos
        [HttpGet]
        [Route("api/evento/Lista_Eventos")]
        public ResListaEventos Lista_Eventos(ReqListaEventos req)
        {
            return new LogEvento().ListaEventos(req);
        }

        //Buscar evento
        [HttpPost]
        [Route("api/evento/BuscarEvento")]
        public ResBuscarEvento BuscarEvento(ReqBuscarEvento req)
        {
            return new LogEvento().BuscarEvento(req);
        }

        //Eliminar Evento
        [HttpPost]
        [Route("api/evento/Eliminar")]
        public ResEliminarEvento EliminarEvento(ReqEliminarEvento req)
        {
            return new LogEvento().EliminarEvento(req);
        }

        //Actualizar Evento
        [HttpPost]
        [Route("api/evento/Actualizar")]
        public ResActualizarEvento ActualizarEvento(ReqActualizarEvento req)
        {
            return new LogEvento().ActualizarEvento(req);
        }

    }
}