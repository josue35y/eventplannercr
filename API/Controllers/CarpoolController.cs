using EventPlannerCR_backend.Entidades;
using EventPlannerCR_backend.Logica;
using System.Web.Http;
namespace API.Controllers
{
    public class CarpoolController : ApiController
    {
        [HttpPost]
        [Route("api/carpool/Insertar")]
        public ResInsertarCarpool insertar(ReqInsertarCarpool req)
        {
            return new LogCarpool().insertar(req);
        }

        [HttpPost]
        [Route("api/carpool/capoolporevento")]
        public ResObtenerCarpoolPorEvento listar(ReqObtenerCarpoolPorEvento req)
        {
            return new LogCarpool().listar(req);
        }

        [HttpPost]
        [Route("api/carpool/capoolporusuario")]
        public ResObtenerCarpoolPorUsuario listarUsuario(ReqObtenerCarpoolPorUsuario req)
        {
            return new LogCarpool().listarUsuario(req);
        }


        [HttpPost]
        [Route("api/carpool/editar")]
        public ResEditarCarpool editar(ReqEditarCarpool req)
        {
            return new LogCarpool().Editar(req);
        }
    }
}