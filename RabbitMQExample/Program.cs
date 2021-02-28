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
                    for (int i = 0; i < 10; i++)
                    { 
                        chanel.QueueDeclare(queue: i.ToString(),
                                            durable: false,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);
                        var pessoa = new
                        {
                            Nome = nomes[0],
                            Idade = 10 + i
                        };
                        var json = JsonConvert.SerializeObject(pessoa);                        
                        var body = Encoding.UTF8.GetBytes(json);
                        chanel.BasicPublish(exchange: "",
                                            routingKey:i.ToString(),
                                            basicProperties: null,
                                            body: body);

                        Console.WriteLine(" [x] Enviado {0}", json);
                    }
                    Console.WriteLine(" [x] Aperte enter para sair ");
                    Console.ReadLine();
                }
            }
        }
    }
}
