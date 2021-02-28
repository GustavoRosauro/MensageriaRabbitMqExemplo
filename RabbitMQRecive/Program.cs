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
                    chanel.QueueDeclare(queue:"Mensagem",
                                        durable:false,
                                        exclusive:false,
                                        autoDelete:false,
                                        arguments:null);
                    var consumer = new EventingBasicConsumer(chanel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var mensagem = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] Recebeu  {0}", mensagem);
                    };
                    chanel.BasicConsume(queue:"Mensagem",
                                        autoAck:true,
                                        consumer:consumer);

                    Console.WriteLine(" Aperte [enter] para sair ");
                    Console.ReadLine();
                }
            }
        }
    }
}
