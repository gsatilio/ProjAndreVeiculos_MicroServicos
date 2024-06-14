// See https://aka.ms/new-console-template for more information
using APIBank.Services;
using Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

const string QUEUE_NAME = "bank";

var factory = new ConnectionFactory() { HostName = "localhost" };

using (var connection = factory.CreateConnection())
{
    using (var channel = connection.CreateModel())
    {
        channel.QueueDeclare(queue: QUEUE_NAME,
                      durable: false,
                      exclusive: false,
                      autoDelete: false,
                      arguments: null);

        while (true)
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var returnMessage = Encoding.UTF8.GetString(body);
                var message = JsonConvert.DeserializeObject<Bank>(returnMessage);
                Bank msgMongo = new BankRabbitService().PostBankToMongo(message).Result;
                Bank msgSQL = new BankRabbitService().PostBankToSQL(message).Result;
                Console.WriteLine("Banco: " + msgMongo.BankName);
                Console.WriteLine("Banco: " + msgSQL.BankName);
            };

            channel.BasicConsume(queue: QUEUE_NAME,
                                 autoAck: true,
                                 consumer: consumer);

            Thread.Sleep(2000);
        }
    }
}
