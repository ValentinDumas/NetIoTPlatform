using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceEndpoint.Models
{
    public class Telemetry
    {
        [JsonProperty("metricDate")]
        public string MetricDate { get; set; } // DateTime format

        [JsonProperty("deviceType")]
        public DeviceType DeviceType { get; set; }

        [JsonProperty("metricValue")]
        public string MetricValue { get; set; }
    }
}
