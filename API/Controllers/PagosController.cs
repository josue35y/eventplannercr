using System.Web.Http;
using EventPlannerCR_backend.Entidades;
using EventPlannerCR_backend.Logica;

namespace API.Controllers
{
    public class PagosController : ApiController
    {
        [HttpPost]
        [Route("api/pagos/actualizarDeudor")]
        public ResActualizarPagoDeudor PagoDeudor(ReqActualizarPagoDeudor req)
        {
            return new LogPagos().ActualizarPagoDeudor(req);
        }

        [HttpPost]
        [Route("api/pagos/actualizarDueno")]
        public ResActualizarPagoDueno PagoDueno(ReqActualizarPagoDueno req)
        {
            return new LogPagos().ActualizarPagoDueno(req);
        }

        [HttpGet]
        [Route("api/pagos/buscarPorUsuario")]
        public ResBuscarPagosPendientes PagosPendientes(ReqBuscarPagosPendientes req)
        {
            return new LogPagos().PagosPendientes(req);
        }
    }
}