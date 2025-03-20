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
        [System.Web.Http.Route("api/asistencia/buscarusuario")]
        public ResBuscarAsistenciaUsuario BuscarAsistenciaUsuario(ReqBuscarAsistenciaUsuario req)
        {
            return new LogAsistencia().BuscarAsistenciaUsuario (req);
        }
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/asistencia/buscarevento")]
        public ResBuscarAsistenciaEvento BuscarAsistenciaEvento(ReqBuscarAsistenciaEvento req)
        {
            return new LogAsistencia().BuscarAsistenciaEvento(req);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/asistencia/editar")]
        public ResEditarAsistencia Modificar(ReqEditarAsistencia req)
        {
            return new LogAsistencia().Editar(req);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/asistencia/borrar")]
        public ResBorrarAsistencia Borrar(ReqBorrarAsistencia req)
        {
            return new LogAsistencia().Borrar(req);
        }
    }
}