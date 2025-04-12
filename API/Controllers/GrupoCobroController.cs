using System.Web.Http;
using EventPlannerCR_backend.Entidades;
using EventPlannerCR_backend.Logica;

namespace API.Controllers
{
    public class GrupoCobroController : ApiController
    {
        [HttpPost]
        [Route("api/GrupoCobro/Insertar")]
        public ResInsertarGrupoCobro Insertar(ReqInsertarGrupoCobro req)
        {
            return new LogGrupoCobro().Insertar(req);
        }

        [HttpPost]
        [Route("api/GrupoCobro/Eliminar")]
        public ResBorrarGrupoCobro Eliminar(ReqBorrarGrupoCobro req)
        {
            return new LogGrupoCobro().Borrar(req);
        }
    }
}