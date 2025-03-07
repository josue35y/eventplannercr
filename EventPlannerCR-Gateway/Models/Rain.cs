using Newtonsoft.Json;

namespace EventPlannerCR_Gateway.Models
{
    public class Rain
    {
        [JsonProperty("3h")] public float ThreeHours { get; set; }
    }
}