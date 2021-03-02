using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQExample
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
                    string[] nomes =new string[10] { "GUSTAVO", "FABIO", "AMANDA", "CECILIA", "JENIFER", "MARIA", "PEDRO", "MOISES","AUGUSTO","JOSE" };                     
                        chanel.QueueDeclare(queue: "Lista",
                                            durable: false,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);
                        
                        var json = JsonConvert.SerializeObject(nomes);                        
                        var body = Encoding.UTF8.GetBytes(json);
                        chanel.BasicPublish(exchange: "",
                                            routingKey:"Lista",
                                            basicProperties: null,
                                            body: body);

                        Console.WriteLine(" [x] Enviado {0}", json);
                    Console.WriteLine(" [x] Aperte enter para sair ");
                    Console.ReadLine();
                }
            }
        }
    }
}
