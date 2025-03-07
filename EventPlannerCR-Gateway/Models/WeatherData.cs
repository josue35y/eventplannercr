using System.Collections.Generic;

namespace EventPlannerCR_Gateway.Models
{
    public class WeatherData
    {
        public long dt { get; set; }
        public MainData main { get; set; }
        public List<Weather> weather { get; set; }
        public Wind wind { get; set; }
        public Rain rain { get; set; }
        public string dt_txt { get; set; }
    }
}