using EventPlannerCR_backend.Entidades;
using EventPlannerCR_backend.Logica;
using System.Web.Http;


namespace API.Controllers
{
    public class UsuarioController : ApiController
    {
        //Insertar
        [HttpPost]
        [Route("api/usuario/Insertar")]
        public ResInsertarUsuarioD insertar(ReqInsertarUsuarioD req)
        {
            return new LogUsuarioD().Insertar(req);
        }

        [HttpGet]
        [Route("api/usuario/ListaUsuarios")]
        public ResListaUsuarios listaUsuarios(ReqListaUsuarios req)
        {
            return new LogUsuarioD().ListaUsuarios(req);
        }

        // controlar los errores si el api no responde.

        //obtener
    }
}