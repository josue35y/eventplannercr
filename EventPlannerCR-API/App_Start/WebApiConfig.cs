﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace EventPlannerCR_API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{idUsuario}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
