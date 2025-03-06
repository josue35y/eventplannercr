using EventPlannerCR_backend.Entidades;
using EventPlannerCR_backend.Logica;
using System.Web.Http;


namespace API.Controllers
{
    public class UsuarioController : ApiController
    {
        //insertar
        [HttpPost]
        [Route("api/usuario/insertar")]
        public ResInsertarUsuario insertar(ReqInsertarUsuario req)
        {
            return new LogUsuario().insertar(req);
        }

        // controlar los errores si el api no responde.

        //obtener
    }
}