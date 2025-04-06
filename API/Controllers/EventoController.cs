using EventPlannerCR_backend.Entidades;
using EventPlannerCR_backend.Entidades.Request;
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

    }
}