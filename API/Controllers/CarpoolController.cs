using EventPlannerCR_backend.Entidades;
using EventPlannerCR_backend.Logica;
using System.Web.Http;
namespace API.Controllers
{
    public class CarpoolController : ApiController
    {
        [HttpPost]
        [Route("api/carpool/insertar")]

        public ResInsertarCarpool insertar(ReqInsertarCarpool req)
        {
            return new LogCarpool().insertar(req);
        }

    }
}