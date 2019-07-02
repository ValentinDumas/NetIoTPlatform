using DeviceEndpoint.Models;
using DeviceEndpoint.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DeviceManagementAPI.Controllers
{
    [Route("device")] // Note: This is correcting the Device from DeviceController to device => matches with the swagger file.
    public class DeviceController : Controller
    {
        private const string IOT_QUEUE_NAME = "command_queue";
        private const string IOT_URL = "localhost";

        private const string JAVA_CRUD_API_URL = "http://192.168.43.154:8080/atlantisProject/webresources/server";
        

        private RequestService _requestService;
        private RabbitMQService _rabbitMQService;

        public DeviceController()
        {
            _requestService = new RequestService();
            _rabbitMQService = new RabbitMQService();
            _rabbitMQService.Send("localhost", "command_queue", "ALLO");
        }

        [HttpGet]
        public string GETTest()
        {
            return "GET request succeeded !";
        }

        
        [HttpPost]
        public string createDevice([FromBody] string device)
        {
            var newDevice = new Device();

            dynamic json_device = JsonConvert.DeserializeObject(device);

            newDevice.Id = json_device.id;
            newDevice.Name = json_device.name;
            newDevice.DeviceType = json_device.deviceType;
            newDevice.Ip = json_device.ip;
            newDevice.MacAddress = json_device.macAddress;
            newDevice.MacDomain = json_device.macDomain;

            // Call url to Java CRUD API
            string url = string.Empty;
            if(newDevice.MacAddress == newDevice.MacDomain)
            {
                url = (JAVA_CRUD_API_URL + "/domain");
            }
            else // device
            {
                url = (JAVA_CRUD_API_URL + "/device");
                newDevice.Ip = string.Empty;
            }

            // Get a generated ID from Java CRUD API
            var id = _requestService.PostData(url, new JsonContent(newDevice));

            newDevice.Id = id.Result;

            return newDevice.Id;
        }

        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="deviceId"></param> 
        [HttpPost("{deviceId}/Telemetry")]
        public string sendTelemetry([FromBody]string body, string deviceId) // TODO: @Check this "path"
        {
            var telemetryAsJson = "ALLO";
            return telemetryAsJson;
        }

        // NOTE: Post or GET ?!
        /// <summary>
        /// Send deviceId and Command to Gateway through RabbitMQ.
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="command"></param>
        //[HttpPost("command")]
        [HttpGet("{deviceId}/command/{commandName}")]
        //public bool sendCommand([FromBody] string command)
        public bool sendCommand(int deviceId, string commandName)
        {
            bool is_command_success = true;

            // Note: no need to format the message, if the command 
            //string message = "{'deviceId': " + deviceId + ", 'commandName': " + commandName + "}";
            string message = deviceId + ";" + commandName;

            /* bool is_command_success = */ _rabbitMQService.Send(IOT_URL, IOT_QUEUE_NAME, message);

            return is_command_success;
        }


    }
}