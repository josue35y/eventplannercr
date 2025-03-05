using EventPlannerCR_backend.Entidades;
using EventPlannerCR_backend.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace API.Controllers
{
    public class UsuarioController : ApiController
    {
        //insertar
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/usuario/insertar")]
        public ResInsertarUsuario insertar(ReqInsertarUsuario req)
        {
            return new LogUsuario().insertar(req);
        }

        // controlar los errores si el api no responde.

        //obtener
    }
}