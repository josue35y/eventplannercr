using EventPlannerCR_backend.Entidades;
using EventPlannerCR_backend.Logica;
using System.Web.Http;


namespace API.Controllers
{
    public class UsuarioController : ApiController
    {
        //Insertar Usuario
        [HttpPost]
        [Route("api/Usuario/InsertarUsuario")]
        public ResInsertarUsuario Insertar(ReqInsertarUsuario req)
        {
            return new LogUsuario().InsertarUsuario(req);
        }

        //Lista de Usuario
        [HttpGet]
        [Route("api/Usuario/Lista_Usuarios")]
        public ResListaUsuarios Lista_Usuarios(ReqListaUsuarios req)
        {
            return new LogUsuario().ListaUsuarios(req);
        }

        //Buscar Usuario
        [HttpPost]
        [Route("api/Usuario/BuscarUsuario")]
        public ResBuscarUsuario BuscarUsuario(ReqBuscarUsuario req)
        {
            return new LogUsuario().BuscarUsuario(req);
        }

        //Eliminar Usuario
        [HttpPost]
        [Route("api/Usuario/Eliminar")]
        public ResEliminarUsuario Eliminar(ReqEliminarUsuario req)
        {
            return new LogUsuario().EliminarUsuario(req);
        }
        
        //Actualizar Usuario
        [HttpPost]
        [Route("api/Usuario/Actualizar")]
        public ResActualizarUsuario ActualizarUsuario(ReqActualizarUsuario req)
        {
            return new LogUsuario().ActualizarUsuario(req);
        }



        // controlar los errores si el api no responde.

        //obtener
    }
}