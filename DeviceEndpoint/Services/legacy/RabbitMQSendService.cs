using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using RabbitMQ.Client;

namespace DeviceEndpoint.Services
{
    public class RabbitMQSendService
    {

        byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public string GetMockMessage()
        {
            StringBuilder sb = new StringBuilder(4000000);

            int message_id = 1;
            int a = DateTime.Now.Millisecond;
            while (message_id <= 50000)
            {
                sb.Append(@"{
                            'REST':
                            {
                                'metricDate': '2019-05-25T16:43:07Z',
                                'deviceType': 'gpsSensor',
                                'metricValue': 'N;10;8;9.14;E;18;19;9.19'
                            },
                            'MQTT':
                            {
                                'name':'humiditySensor_wealthy-snails',
                                'macAddress':'44:81:C0:0D:6C:E3',
                                'metricDate':'2019-05-25T16:40:13Z',
                                'deviceType':'humiditySensor',
                                'metricValue':'0'
                            }
                         }");

                message_id += 1;
            }

            Console.WriteLine("Elapsed to build string: {0} ms", (DateTime.Now.Millisecond - a).ToString());

            return sb.ToString();
        }

        private ConnectionFactory connectionFactory = null;

        public RabbitMQSendService(string hostname = "localhost" )
        {
            connectionFactory = new ConnectionFactory() { HostName = hostname }; // should be in controller.
        }

        public void Send(string message)
        {
            // We connect to a broker at the "localhost" address
            using (var connection = connectionFactory.CreateConnection())
            {
                // Create a broker's channel to get things done :)
                using (var channel = connection.CreateModel())
                {
                    // Declare an indempotent queue
                    // durable: true, ensure queues are not lost when RabbitMQ restarts
                    channel.QueueDeclare(queue: "task_queue",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    int start_time = DateTime.Now.Millisecond;

                    var body = Encoding.UTF8.GetBytes(message);

                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true; // declare messages as persistent => they won't be lost even if RabbitMQ restarts.

                    channel.BasicPublish(exchange: "",
                        routingKey: "task_queue",
                        basicProperties: properties,
                        body: body);

                    int elapsed_time = DateTime.Now.Millisecond - start_time;
                    Console.WriteLine("Elapsed time for 'N=50000' ({0} ms)", elapsed_time.ToString());

                    //Console.WriteLine("Press enter to exit...");
                    //Console.ReadLine();
                }
            }
        }

        public void ReceiveStart()
        {

        }

        private static string GetMessage(string[] args)
        {
            return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
        }
    }
}

