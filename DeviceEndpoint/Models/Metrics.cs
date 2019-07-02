using Newtonsoft.Json;
using System.Collections.Generic;

namespace DeviceEndpoint.Models
{
    public class Metrics
    {
        //public int ID { get; set; }

        [JsonProperty("data")]
        public List<Metric> Data;
    }
}
