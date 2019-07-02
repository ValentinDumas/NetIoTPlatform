using Newtonsoft.Json;

namespace DeviceEndpoint.Models
{
    public class Metric
    {
        [JsonProperty("id")]
        public int Id { get; set; } // DateTime format

        [JsonProperty("type")]
        public string Type{ get; set; } // DateTime format

        [JsonProperty("value")]
        public float Value { get; set; }
    }
}
