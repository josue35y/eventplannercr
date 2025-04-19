using System.Web.Http;
using EventPlannerCR_backend.Entidades;
using EventPlannerCR_backend.Logica;

namespace API.Controllers
{
    public class DeudasController : ApiController
    {
        [HttpPost]
        [Route("api/deuda/Insertar")]
        public ResInsertarDeuda InsertarDeuda(ReqInsertarDeuda req)
        {
            return new LogDeudas().Insertar(req);
        }
        
        [HttpPost]
        [Route("api/deuda/Borrar")]
        public ResBorrarDeuda BorrarDeuda(ReqBorrarDeuda req)
        {
            return new LogDeudas().Borrar(req);
        }
        
        [HttpPost]
        [Route("api/deuda/Buscar")]
        public ResBuscarDeuda BuscarDeuda(ReqBuscarDeuda req)
        {
            return new LogDeudas().Buscar(req);
        }
        
        [HttpPost]
        [Route("api/deuda/BuscarDueno")]
        public ResBuscarDeudaDueno BuscarDeudaDueno(ReqBuscarDeudaDueno req)
        {
            return new LogDeudas().BuscarDueno(req);
        }
        
        [HttpPost]
        [Route("api/deuda/BuscarDeudaUsuario")]
        public ResBuscarDeudaUsuario BuscarDeudaUsuario(ReqBuscarDeudaUsuario req)
        {
            return new LogDeudas().BuscarDeudaUsuario(req);
        }
        
    }
}