using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DeviceEndpoint.Services
{
    public class RequestService
    {
        public async Task<string> GetData(string url)
        {
            //The 'using' will help to prevent memory leaks.
            //Create a new instance of HttpClient
            using (HttpClient client = new HttpClient())

            //Setting up the response...         
            using (HttpResponseMessage res = await client.GetAsync(url))
            using (HttpContent content = res.Content)
            {
                string data = await content.ReadAsStringAsync();
                if (data != null)
                {
                    Console.WriteLine(data);
                }

                return data;
            }
        }

        public async Task<string> PostData(string url, System.Net.Http.HttpContent jsonContent)
        {
            //The 'using' will help to prevent memory leaks.
            //Create a new instance of HttpClient
            using (HttpClient client = new HttpClient())

            //Setting up the response...         
            using (HttpResponseMessage res = await client.PostAsync(url, jsonContent))
            {
                using (HttpContent content = res.Content)
                {
                    string data = await content.ReadAsStringAsync();
                    if (data != null)
                    {
                        Console.WriteLine(data);
                    }

                    return data;
                }
             }
        }
    }
}
