using EventPlannerCR_backend.Entidades;
using EventPlannerCR_backend.Logica;
using System.Web.Http;
using System.Web.Mvc;

namespace API.Controllers
{
    public class AsistenciaController : ApiController
    {

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/asistencia/insertar")]

        public ResInsertarAsistencia insertar(ReqInsertarAsistencia req)
        {
            return new LogAsistencia().insertar(req);
        }

    }
}