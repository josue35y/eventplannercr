using System;
using System.Net.Http;
using EventPlannerCR_Gateway.Models.Request;
using EventPlannerCR_Gateway.Models.Response;
using Newtonsoft.Json;

namespace EventPlannerCR_Gateway.Controllers
{
    public class EventoController
    {
        public OpenWeatherForecastResponse ConsultarEventosCercanos(OpenWeatherForecastRequest req)
        {
            OpenWeatherForecastResponse res = new OpenWeatherForecastResponse();
            
            var url = $"http://api.openweathermap.org/data/2.5/forecast?lat={req.lat}&lon={req.lon}&appid={req.appid}&cnt={req.cnt}&lang={req.lang}&units={req.units}";

            using (var cliente = new HttpClient())
            {
                try
                {
                    HttpResponseMessage respuesta = cliente.GetAsync(url).Result;
                    respuesta.EnsureSuccessStatusCode();
                    string contenido = respuesta.Content.ReadAsStringAsync().Result;
                    
                    var forecast = JsonConvert.DeserializeObject<OpenWeatherForecastResponse>(contenido);

                    res = forecast;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error desconocido: {ex.Message}");
                }
            }

            return res;
        }
    }
}