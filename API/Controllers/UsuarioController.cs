using EventPlannerCR_backend.Entidades;
using EventPlannerCR_backend.Logica;
using System.Web.Http;


namespace API.Controllers
{
    public class UsuarioController : ApiController
    {
        //Insertar Usuario
        [HttpPost]
        [Route("api/usuario/InsertarUsuario")]
        public ResInsertarUsuario Insertar(ReqInsertarUsuario req)
        {
            return new LogUsuario().InsertarUsuario(req);
        }

        //Lista de Usuario
        [HttpGet]
        [Route("api/usuario/Lista_Usuarios")]
        public ResListaUsuarios Lista_Usuarios(ReqListaUsuarios req)
        {
            return new LogUsuario().ListaUsuarios(req);
        }

        //Buscar Usuario
        [HttpPost]
        [Route("api/usuario/BuscarUsuario")]
        public ResBuscarUsuario BuscarUsuario(ReqBuscarUsuario req)
        {
            return new LogUsuario().BuscarUsuario(req);
        }

        //Eliminar Usuario
        [HttpPost]
        [Route("api/usuario/Eliminar")]
        public ResEliminarUsuario Eliminar(ReqEliminarUsuario req)
        {
            return new LogUsuario().EliminarUsuario(req);
        }
        
        //Actualizar Usuario
        [HttpPost]
        [Route("api/usuario/Actualizar")]
        public ResActualizarUsuario ActualizarUsuario(ReqActualizarUsuario req)
        {
            return new LogUsuario().ActualizarUsuario(req);
        }



        // controlar los errores si el api no responde.

        //obtener
    }
}