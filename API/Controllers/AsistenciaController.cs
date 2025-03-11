using EventPlannerCR_backend.Entidades;
using EventPlannerCR_backend.Logica;
using System.Web.Http;
using System.Web.Mvc;

namespace API.Controllers
{
    public class AsistenciaController : ApiController
    {

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/asistencia/Insertar")]
        public ResInsertarAsistencia insertar(ReqInsertarAsistencia req)
        {
            return new LogAsistencia().Insertar(req);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/asistencia/Buscar")]
        public ResBuscarAsistencia buscarAsistencia(ReqBuscarAsistencia req)
        {
            return new LogAsistencia().Buscar(req);
        }
    }
}