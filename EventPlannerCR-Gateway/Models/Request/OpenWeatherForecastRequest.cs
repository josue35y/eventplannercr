namespace EventPlannerCR_Gateway.Models.Request
{
    public class OpenWeatherForecastRequest
    {
        public readonly string appid = "e2b2e9dfc96ed1e9592227777215ef8b";
        public readonly string lang = "es";
        public readonly string units = "metric";
        public double? lat { get; set; }
        public double? lon { get; set; }
        public int? cnt { get; set; }
    }
}