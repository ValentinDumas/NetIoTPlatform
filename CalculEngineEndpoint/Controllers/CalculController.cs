using DeviceEndpoint.Models;
using DeviceEndpoint.Repositories;
using DeviceEndpoint.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeviceEndpoint
{
    [Route("calcul")]
    public class CalculController : Controller
    {
        private RequestService _requestService;

        private const string JAVA_CRUD_API_URL = "http://192.168.43.154:8080/atlantisProject/webresources/server/metric";


        public CalculController()
        {
            _requestService = new RequestService();
        }

        [HttpGet("lastMetrics")]
        public async Task<List<Metric>> GetFakedLastMetrics(/*[FromBody]Metrics metrics*/)
        {
            var m = await _requestService.GetData(JAVA_CRUD_API_URL);

            var fakejson = @"[{'id':3,'type':'co2Sensor','value':156.516},{'id':3,'type':'co2Sensor','value':156.516},{'id':3,'type':'co2Sensor','value':156.516},{'id':3,'type':'co2Sensor','value':156.516},{'id':3,'type':'presenceSensor','value':156.516},{'id':3,'type':'presenceSensor','value':156.516},{'id':3,'type':'presenceSensor','value':156.516},{'id':3,'type':'presenceSensor','value':156.516},{'id':3,'type':'presenceSensor','value':156.516},{'id':3,'type':'presenceSensor','value':156.516},{'id':3,'type':'presenceSensor','value':156.516},{'id':3,'type':'presenceSensor','value':156.516}]";

            //List<Metric> result = (List<Metric>)JsonConvert.DeserializeObject(fakejson, typeof(List<Metric>));
            List<Metric> result = (List<Metric>)JsonConvert.DeserializeObject(m, typeof(List<Metric>));

            return result;
        }

        //[HttpGet]
        //[Consumes("application/json")]
        //[Produces("application/json")]
        //public Task<string> GetLastMetrics()
        //{
        //    // TODO: check for emptiness && recall
        //    var last_metrics = _requestService.GetData("http://192.168.43.154:8080/atlantisProject/webresources/server/metric");
        //    var r = last_metrics.Result;
        //    return r;
        //}

        [HttpPost("saveCalculatedMetrics")]
        public void saveCalculatedMetrics([FromBody] Calcul calcul)
        {
            Console.WriteLine(calcul);

            using (var calculContext = new CalculContext())
            {

                Calcul calculEnt = calculContext.Calcul.FirstOrDefault(x => x.domain_id == calcul.domain_id && x.date >= DateTime.UtcNow.Date && x.type==calcul.type);
                if (calculEnt != null)
                {
                    //Data update
                    calculEnt.domain_id = calcul.domain_id;
                    calculEnt.type = calcul.type;
                    calculEnt.average = calcul.average;
                    calculEnt.sum = calcul.sum;
                    calculEnt.max = calcul.max;
                    calculEnt.min = calcul.min;
                    calculEnt.date = calcul.date;
                    calculContext.Calcul.Update(calculEnt);
                }
                else
                {
                    calculContext.Calcul.Add(new Calcul
                    {
                        domain_id = calcul.domain_id,
                        type = calcul.type,
                        average = calcul.average,
                        sum = calcul.sum,
                        max = calcul.max,
                        min = calcul.min,
                        date = calcul.date
                    });
                }
                calculContext.SaveChanges();
            }
        }

        [HttpGet("inDomain/{domainId}/{jour}")]
        public List<Calcul> GetCalcul(int domainId, int jour)
        {
            using (var calculContext = new CalculContext())
            {
                //var calculatedMetricWithSpecificDomainIdAndDeviceType = calculContext.Calcul.Where(c => c.domain_id == domainId).Where(c => c.type == deviceType).OrderByDescending(e => e.date).ToList();

                DateTime dt = DateTime.UtcNow.Date.AddDays((double)-jour);
                var calculatedMetricWithSpecificDomainIdAndDeviceType = calculContext.Calcul.Where(c => c.domain_id == domainId && c.date > dt).OrderByDescending(e => e.date).ToList();

                return calculatedMetricWithSpecificDomainIdAndDeviceType;
            }
        }
    }
}
