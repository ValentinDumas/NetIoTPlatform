using DeviceEndpoint.Models;
using DeviceEndpoint.Repositories;
using DeviceEndpoint.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CalculationEngine
{
    public class Program
    {
        static string CALCUL_ENGINE_API_ENDPOINT = "http://localhost:2888/calcul/";

        public static async Task<string> GetMetrics()
        {
            RequestService rs = new RequestService();
            var metrics = await rs.GetData(CALCUL_ENGINE_API_ENDPOINT + "lastMetrics");
            return metrics;
        }

        static object _ActiveWorkersLock = new object();
        static int _CountOfActiveWorkers;

        static void Main(string[] args)
        {
            ThreadPool.SetMaxThreads(8, 8);
            var Running = true;

            while (Running)
            {


                var metrics = GetMetrics();

                Console.WriteLine(metrics.Result);

                List<Metric> result = (List<Metric>)JsonConvert.DeserializeObject(metrics.Result, typeof(List<Metric>));

                lock (_ActiveWorkersLock)
                    ++_CountOfActiveWorkers;
                ThreadPool.QueueUserWorkItem(ThreadProc, result); // Queue the task. && count the workers

                // NOTE: Not sure about this line !..
                lock (_ActiveWorkersLock)
                {
                    Console.WriteLine("Number of workers in ThreadPool: {0}", _CountOfActiveWorkers);
                    while (_CountOfActiveWorkers >2)
                    {
                        Console.WriteLine("Number of workers in ThreadPool: {0}", _CountOfActiveWorkers);
                        Monitor.Wait(_ActiveWorkersLock);
                    }
                }

                Console.WriteLine("Main thread does some work, then sleeps for 5 seconds");
                Thread.Sleep(10000);
                Console.WriteLine("Moving to next execution");
            }
        }

        // This thread procedure performs the task.
        static void ThreadProc(Object state)
        {
            try
            {
                Console.WriteLine("Hello");

                RequestService rs = new RequestService();

                var metrics = state as List<Metric>;

                var groupbytype = metrics.GroupBy(x => x.Type);

                foreach (var keyValue in groupbytype)
                {
                    var type = keyValue.Key;

                    if (type == "ledDevice")
                    {
                        var domain = keyValue.FirstOrDefault().Id;

                        var average = keyValue.Select(x => x.Value).Average();
                        var sum = keyValue.Select(x => x.Value).Sum();
                        var max = keyValue.Select(x => x.Value).Max();
                        var min = keyValue.Select(x => x.Value).Min();

                        // Save in database
                        string json = JsonConvert.SerializeObject(new Calcul { domain_id = domain, type = type, average = average, sum = sum, max = max, min = min, date = DateTime.Now });
                        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                        var insertStatus = rs.PostData(CALCUL_ENGINE_API_ENDPOINT + "saveCalculatedMetrics", httpContent);
                    }
                    if (type == "beeperDevice")
                    {
                        var domain = keyValue.FirstOrDefault().Id;

                        var average = keyValue.Select(x => x.Value).Average();
                        var sum = keyValue.Select(x => x.Value).Sum();
                        var max = keyValue.Select(x => x.Value).Max();
                        var min = keyValue.Select(x => x.Value).Min();

                        // Save in database
                        string json = JsonConvert.SerializeObject(new Calcul { domain_id = domain, type = type, average = average, sum = sum, max = max, min = min, date = DateTime.Now });
                        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                        var insertStatus = rs.PostData(CALCUL_ENGINE_API_ENDPOINT + "saveCalculatedMetrics", httpContent);
                    }
                    if (type == "presenceSensor")
                    {
                        var domain = keyValue.FirstOrDefault().Id;

                        var average = keyValue.Select(x => x.Value).Average();
                        var sum = keyValue.Select(x => x.Value).Sum();
                        var max = keyValue.Select(x => x.Value).Max();
                        var min = keyValue.Select(x => x.Value).Min();

                        // Save in database
                        string json = JsonConvert.SerializeObject(new Calcul { domain_id = domain, type = type, average = average, sum = sum, max = max, min = min, date = DateTime.Now });
                        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                        var insertStatus = rs.PostData(CALCUL_ENGINE_API_ENDPOINT + "saveCalculatedMetrics", httpContent);
                    }
                    if (type == "temperatureSensor")
                    {
                        var domain = keyValue.FirstOrDefault().Id;

                        var average = keyValue.Select(x => x.Value).Average();
                        var sum = keyValue.Select(x => x.Value).Sum();
                        var max = keyValue.Select(x => x.Value).Max();
                        var min = keyValue.Select(x => x.Value).Min();

                        // Save in database
                        string json = JsonConvert.SerializeObject(new Calcul { domain_id = domain, type = type, average = average, sum = sum, max = max, min = min, date = DateTime.Now });
                        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                        var insertStatus = rs.PostData(CALCUL_ENGINE_API_ENDPOINT + "saveCalculatedMetrics", httpContent);
                    }
                    if (type == "brightnessSensor")
                    {
                        var domain = keyValue.FirstOrDefault().Id;

                        var average = keyValue.Select(x => x.Value).Average();
                        var sum = keyValue.Select(x => x.Value).Sum();
                        var max = keyValue.Select(x => x.Value).Max();
                        var min = keyValue.Select(x => x.Value).Min();

                        // Save in database
                        string json = JsonConvert.SerializeObject(new Calcul { domain_id = domain, type = type, average = average, sum = sum, max = max, min = min, date = DateTime.Now });
                        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                        var insertStatus = rs.PostData(CALCUL_ENGINE_API_ENDPOINT + "saveCalculatedMetrics", httpContent);
                    }
                    if (type == "atmosphericPressureSensor")
                    {
                        var domain = keyValue.FirstOrDefault().Id;

                        var average = keyValue.Select(x => x.Value).Average();
                        var sum = keyValue.Select(x => x.Value).Sum();
                        var max = keyValue.Select(x => x.Value).Max();
                        var min = keyValue.Select(x => x.Value).Min();

                        // Save in database
                        string json = JsonConvert.SerializeObject(new Calcul { domain_id = domain, type = type, average = average, sum = sum, max = max, min = min, date = DateTime.Now });
                        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                        var insertStatus = rs.PostData(CALCUL_ENGINE_API_ENDPOINT + "saveCalculatedMetrics", httpContent);
                    }
                    if (type == "humiditySensor")
                    {
                        var domain = keyValue.FirstOrDefault().Id;

                        var average = keyValue.Select(x => x.Value).Average();
                        var sum = keyValue.Select(x => x.Value).Sum();
                        var max = keyValue.Select(x => x.Value).Max();
                        var min = keyValue.Select(x => x.Value).Min();

                        // Save in database
                        string json = JsonConvert.SerializeObject(new Calcul { domain_id = domain, type = type, average = average, sum = sum, max = max, min = min, date = DateTime.Now });
                        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                        var insertStatus = rs.PostData(CALCUL_ENGINE_API_ENDPOINT + "saveCalculatedMetrics", httpContent);
                    }
                    if (type == "soundLevelSensor")
                    {
                        var domain = keyValue.FirstOrDefault().Id;

                        var average = keyValue.Select(x => x.Value).Average();
                        var sum = keyValue.Select(x => x.Value).Sum();
                        var max = keyValue.Select(x => x.Value).Max();
                        var min = keyValue.Select(x => x.Value).Min();

                        // Save in database
                        string json = JsonConvert.SerializeObject(new Calcul { domain_id = domain, type = type, average = average, sum = sum, max = max, min = min, date = DateTime.Now });
                        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                        var insertStatus = rs.PostData(CALCUL_ENGINE_API_ENDPOINT + "saveCalculatedMetrics", httpContent);
                    }
                    if (type == "gpsSensor")
                    {
                        var domain = keyValue.FirstOrDefault().Id;

                        var average = keyValue.Select(x => x.Value).Average();
                        var sum = keyValue.Select(x => x.Value).Sum();
                        var max = keyValue.Select(x => x.Value).Max();
                        var min = keyValue.Select(x => x.Value).Min();

                        // Save in database
                        string json = JsonConvert.SerializeObject(new Calcul { domain_id = domain, type = type, average = average, sum = sum, max = max, min = min, date = DateTime.Now });
                        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                        var insertStatus = rs.PostData(CALCUL_ENGINE_API_ENDPOINT + "saveCalculatedMetrics", httpContent);
                    }
                    if (type == "co2Sensor")
                    {
                        var domain = keyValue.FirstOrDefault().Id;

                        var average = keyValue.Select(x => x.Value).Average();
                        var sum = keyValue.Select(x => x.Value).Sum();
                        var max = keyValue.Select(x => x.Value).Max();
                        var min = keyValue.Select(x => x.Value).Min();

                        // Save in database
                        string json = JsonConvert.SerializeObject(new Calcul { domain_id = domain, type = type, average = average, sum = sum, max = max, min = min, date = DateTime.Now });
                        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                        var insertStatus = rs.PostData(CALCUL_ENGINE_API_ENDPOINT + "saveCalculatedMetrics", httpContent);
                    }

                }
                
                Console.WriteLine("Hello from the thread pool.");
            }
            finally
            {
                lock (_ActiveWorkersLock)
                {
                    --_CountOfActiveWorkers;
                    Monitor.PulseAll(_ActiveWorkersLock);
                }
            }
        }



    }
}
