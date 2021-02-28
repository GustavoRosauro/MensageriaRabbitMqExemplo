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
                    chanel.QueueDeclare(queue: "Mensagem",
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);
                    string mensagem = "Mensagem foi enviada para o Rabbit";
                    var body = Encoding.UTF8.GetBytes(mensagem);
                    chanel.BasicPublish(exchange: "",
                                        routingKey:"Mensagem",
                                        basicProperties: null,
                                        body: body);

                    Console.WriteLine(" [x] Enviado {0}", mensagem);
                    Console.WriteLine(" [x] Aperte enter para sair ");
                    Console.ReadLine();
                }
            }
        }
    }
}
