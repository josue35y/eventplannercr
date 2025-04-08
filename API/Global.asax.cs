using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Timers;
using System.Web;
using EventPlannerCR_backend.Logica;

namespace API
{
    public class WebApiApplication : HttpApplication
    {
        //private static Timer _timer;

        //protected void Application_Start()
        //{
        //    AreaRegistration.RegisterAllAreas();
        //    GlobalConfiguration.Configure(WebApiConfig.Register);
        //    FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        //    RouteConfig.RegisterRoutes(RouteTable.Routes);
        //    BundleConfig.RegisterBundles(BundleTable.Bundles);

        //    // Configurar la tarea en segundo plano cada 
        //    _timer = new Timer(180000);
        //    _timer.Elapsed += EjecutarTarea;
        //    _timer.AutoReset = true;
        //    _timer.Enabled = true;

        //    _timer.Start();
        //}

        //private void EjecutarTarea(object sender, ElapsedEventArgs e)
        //{
        //    // Buscar eventos cercanos
        //    LogEvento evento = new LogEvento();
        //    evento.BuscarEventosCercanos();
        //    Console.WriteLine("Salió de la tarea");
        //}
    }
}