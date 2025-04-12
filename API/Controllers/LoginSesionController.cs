using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using EventPlannerCR_backend.Entidades;
using EventPlannerCR_backend.Logica;

namespace API.Controllers
{
    public class LoginSesionController : ApiController
    {
        // GET: LoginSesion

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/utilitarios/login")]
        public ResLogin Login(ReqLogin req)
        {
            LogUtilitarios MiLogica = new LogUtilitarios();
            ResLogin res = new ResLogin();

            return res = MiLogica.Login(req);
        }
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/utilitarios/registro")]
        public ResRegistrarUsuario registro(ReqRegistrarUsuario req)
        {
            LogUtilitarios MiLogica = new LogUtilitarios();
            ResRegistrarUsuario res = new ResRegistrarUsuario();
            
            return res = MiLogica.RegistrarUsuario(req);
        }
    }
}