using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeviceEndpoint.Services
{
    public class RabbitMQService
    {
        public RabbitMQService()
        {

        }

        public void Send(string hostName, string queueName, string message)
        {
            // We connect to a broker at the "localhost" address
            var factory = new ConnectionFactory() { HostName = hostName };
            using (var connection = factory.CreateConnection())
            {
                // Create a broker's channel to get things done :)
                using (var channel = connection.CreateModel())
                {
                    // Declare an indempotent queue
                    // durable: true, ensure queues are not lost when RabbitMQ restarts
                    channel.QueueDeclare(queue: queueName,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    var body = Encoding.UTF8.GetBytes(message);

                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true; // declare messages as persistent => they won't be lost even if RabbitMQ restarts.

                    channel.BasicPublish(exchange: "",
                        routingKey: queueName,
                        basicProperties: properties,
                        body: body);
                }
            }
        }

        public void Receive(string hostName, string queueName)
        {
            Console.WriteLine("Initialize RabbitMQ Receive Service (listening to the Gateway and Device data)");

            var factory = new ConnectionFactory() { HostName = hostName };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                Console.WriteLine(" [*] Waiting for messages.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);

                    int dots = message.Split('.').Length - 1;
                    Thread.Sleep(dots * 1000);

                    Console.WriteLine(" [x] Done");

                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                };
                channel.BasicConsume(queue: "task_queue",
                                                     autoAck: false,
                                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
