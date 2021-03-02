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
                        chanel.QueueDeclare(queue: "Lista",
                                            durable: false,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);
                        var consumer = new EventingBasicConsumer(chanel);
                        consumer.Received += (model, ea) =>
                        {
                            var body = ea.Body.ToArray();
                            string json = Encoding.UTF8.GetString(body);
                            string[] nomes = JsonConvert.DeserializeObject<string[]>(json);
                            foreach(var nome in nomes)
                                Console.WriteLine(" [x] Recebeu  Nome: {0} ", nome);
                        };
                        chanel.BasicConsume(queue: "Lista",
                                            autoAck: true,
                                            consumer: consumer);
                    Console.WriteLine(" Aperte [enter] para sair ");
                    Console.ReadLine();
                }
            }
        }
    }
}
