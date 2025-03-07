using System;
using System.Net.Http;
using System.Threading.Tasks;
using EventPlannerCR_Gateway.Models.Request;
using EventPlannerCR_Gateway.Models.Response;
using Newtonsoft.Json;

namespace EventPlannerCR_Gateway.Controllers
{
    public class EventoController
    {
        public async Task ConsultarEventosCercanos(OpenWeatherForecastRequest req)
        {
            ResConsultarEventosCercanos res = new ResConsultarEventosCercanos();
            
            var url = $"http://api.openweathermap.org/data/2.5/forecast?lat={req.lat}&lon={req.lon}&appid={req.appid}&cnt={req.cnt}&lang={req.lang}&units={req.units}";

            using (var cliente = new HttpClient())
            {
                try
                {
                    HttpResponseMessage respuesta = await cliente.GetAsync(url);
                    respuesta.EnsureSuccessStatusCode();
                    string contenido = await respuesta.Content.ReadAsStringAsync();
                    
                    var forecast = JsonConvert.DeserializeObject<OpenWeatherForecastResponse>(contenido);
                    Console.WriteLine(forecast);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error desconocido: {ex.Message}");
                }
            }
        }
    }
}