using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Collections.Generic;

namespace QIQO.MQ
{
    public class Publisher
    {
        private readonly IConfiguration _configuration;

        public Publisher(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendMessage(object thing, string routingKey)
        {
            Publish(thing.Serialize(), string.Empty, routingKey, routingKey);
        }

        public void SendMessage(byte[] message, string routingKey)
        {
            Publish(message, string.Empty, routingKey, routingKey);
        }
        public void SendMessage(object thing, string exchangeName, string queueName, string routingKey)
        {
            Publish(thing.Serialize(), exchangeName, queueName, routingKey);
        }

        public void SendMessage(byte[] message, string exchangeName, string queueName, string routingKey)
        {
            Publish(message, exchangeName, queueName, routingKey);
        }
        private void Publish(byte[] message, string exchangeName, string queueName, string routingKey)
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration["QueueConfig:Server"],
                UserName = _configuration["QueueConfig:User"],
                Password = _configuration["QueueConfig:Password"]
            };
            var queueArgs = new Dictionary<string, object>
                {
                    {"x-dead-letter-exchange", _configuration["QueueConfig:Monitor:DeadLetterExchange"]},
                    // {"x-expires", 30000}
                };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchangeName, ExchangeType.Topic);
            channel.QueueDeclare(queueName, true, false, false, queueArgs);
            channel.BasicPublish(exchange: exchangeName,
                                 routingKey: routingKey,
                                 basicProperties: null,
                                 body: message);
        }
    }
}
