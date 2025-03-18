using System.Collections.Generic;

namespace EventPlannerCR_Gateway.Models.Response
{
    public class OpenWeatherForecastResponse
    {
        public string cod { get; set; }
        public int cnt { get; set; }
        public int message { get; set; }
        public List<WeatherData> list { get; set; }
        public City city { get; set; }
    }
}