using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQRecive
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var chanel = connection.CreateModel())
                {
                    for (int i = 0; i < 10; i++)
                    {
                        chanel.QueueDeclare(queue: i.ToString(),
                                            durable: false,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);
                        var consumer = new EventingBasicConsumer(chanel);
                        consumer.Received += (model, ea) =>
                        {
                            var body = ea.Body.ToArray();
                            string json = Encoding.UTF8.GetString(body);
                            dynamic obj = JsonConvert.DeserializeObject<dynamic>(json);
                            Console.WriteLine(" [x] Recebeu  Nome: {0} Idade: {1}", obj["Nome"],obj["Idade"]);
                        };
                        chanel.BasicConsume(queue: i.ToString(),
                                            autoAck: true,
                                            consumer: consumer);
                    }
                    Console.WriteLine(" Aperte [enter] para sair ");
                    Console.ReadLine();
                }
            }
        }
    }
}
