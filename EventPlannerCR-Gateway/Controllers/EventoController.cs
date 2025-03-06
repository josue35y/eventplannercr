using System.Web.Http;
using EventPlannerCR_backend.Entidades;
using EventPlannerCR_backend.Logica;

namespace EventPlannerCR_Gateway.Controllers
{
    public class EventoController
    {
        [HttpPost]
        [Route("gateway/evento/consultar")]
        public ResEventosCercanos BuscarEventosCercanos(ReqEventosCercanos req)
        {
            return new LogEvento().BuscarEventosCercanos(req);
        }
    }
}