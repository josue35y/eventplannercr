using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using EventPlannerCR_backend.Logica;
using EventPlannerCR_backend.Entidades;

namespace EventPlannerCR_API.Controllers
{
    public class UsuarioController : ApiController 
    {
        //insertar



        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/usuario/insertar")]
        public ResInsertarUsuario insertar(ReqInsertarUsuario req) //INVESTIGAR: RECIBIR Y RETORNAR HTTP
        {
            //ResInsertarUsuario res = new ResInsertarUsuario();
            //LogUsuario miLogica = new LogUsuario();
            //res = miLogica.insertar(req);
            //return res
            return new LogUsuario().insertar(req);
        }


        // controlar los errores si el api no responde.



        //obtener
    }
}