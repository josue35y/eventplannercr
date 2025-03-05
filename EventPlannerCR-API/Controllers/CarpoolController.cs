using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using EventPlannerCR_backend.Entidades;
using EventPlannerCR_backend.Logica;

namespace EventPlannerCR_API.Controllers
{
    public class CarpoolController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/carpool/insertar")]

        public ResInsertarCarpool insertar (ReqInsertarCarpool req)
        {
            return new LogCarpool().insertar(req);
        }

    }
}