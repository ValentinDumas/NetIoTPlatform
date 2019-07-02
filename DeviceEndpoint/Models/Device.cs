using Newtonsoft.Json;

namespace DeviceEndpoint.Models
{
    // @TODO: Faire des description swagger ? 

    // GUID : used where the uniqueness of an object (here, a device) is required/mandatory.

    public class Device
    {
        [JsonProperty("id")]
        public string Id{ get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("deviceType")]
        public string DeviceType { get; set; }

        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("macAddress")]
        public string MacAddress { get; set; }

        [JsonProperty("macDomain")]
        public string MacDomain { get; set; }
    }
}
