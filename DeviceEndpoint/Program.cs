using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace DeviceEndpoint
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //RabbitMQReceiveService rabbitMQReceiveService = new RabbitMQReceiveService();
            //RabbitMQSendService rabbitMQSendService = new RabbitMQSendService();
            //rabbitMQSendService.Send("alloooooo");

            //using (var context = new SchoolContext())
            //{
            //    var std = new Student()
            //    {
            //        Name = "Bill"
            //    };
            //    var query = context.Students
            //           .Where(s => s.Name == "Bill")
            //           .FirstOrDefault<Student>();
            //    context.Students.Add(std);
            //    context.SaveChanges();
            //}

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://0.0.0.0:2890")
                .UseStartup<Startup>()
                .Build();
    }
}
