using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WikiSurfModel;

namespace WikiSurfCore.RabbitMQ
{
    public static class RabbitHelper
    {
        public static void Send(WordBankQueue queueItem)
        {
            bool processed = false;
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "wiki_surf_dev",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
                var consumer = new EventingBasicConsumer(channel);
                var props = channel.CreateBasicProperties();
                var correlationId = Guid.NewGuid().ToString();
                var replyQueue = channel.QueueDeclare().QueueName;
                props.CorrelationId = correlationId;
                props.ReplyTo = replyQueue;
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var response = Encoding.UTF8.GetString(body);
                    if (ea.BasicProperties.CorrelationId == correlationId)
                    {
                        Console.WriteLine(response);
                        processed = true;
                    }
                };
                var json = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new
                {
                    queueItem.WordBankId,
                    queueItem.WordBankQueueId,
                    queueItem.CreatedDate,
                    queueItem.IsProcessed,
                    queueItem.ProcessedDate,
                    queueItem.WordBank.Word
                }));

                channel.BasicPublish(exchange: "",
                    routingKey: "wiki_surf_dev",
                    basicProperties: props,
                    body: json);
                Console.WriteLine(" [x] Sent WordBankQueueId: {0} Word: {1}", queueItem.WordBankQueueId, queueItem.WordBank.Word);
                while (!processed)
                {
                    Thread.Sleep(1000);
                    
                    var reply = channel.BasicConsume(replyQueue, true, consumer);
                }
            }

        }
    }
}
