using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using EventPlannerCR_Gateway.Models;
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

                    if (forecast != null && forecast.list.Any())
                    {
                        var ultimoDato = forecast.list.Last();

                        if (ultimoDato != null)
                        {
                            res = new OpenWeatherForecastResponse
                            {
                                cod = forecast.cod,
                                message = forecast.message,
                                cnt = 1,
                                list = new List<WeatherData> { ultimoDato },
                                city = forecast.city
                            };
                        }
                    }
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