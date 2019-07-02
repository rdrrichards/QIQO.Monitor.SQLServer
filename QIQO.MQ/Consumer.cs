using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.MessagePatterns;
using System;
using System.Collections.Generic;

namespace QIQO.MQ
{
    public class Consumer
    {
        private readonly ConnectionFactory _factory;
        private readonly IConfiguration _configuration;

        public Consumer(IConfiguration configuration)
        {
            _configuration = configuration;
            _factory = new ConnectionFactory
            {
                HostName = _configuration["QueueConfig:Server"],
                UserName = _configuration["QueueConfig:User"],
                Password = _configuration["QueueConfig:Password"]
            };
        }
        public void ReceiveMessages(string exchangeName, string queueName, string routingKey, Action<string, string> action)
        {
            ProcessMessages(exchangeName, queueName, routingKey, action);
        }
        public void ReceiveMessages(string exchangeName, string queueName, string routingKey, Func<string, string, bool> action)
        {
            ProcessMessages(exchangeName, queueName, routingKey, action);
        }
        private void ProcessMessages(string exchangeName, string queueName, string routingKey, Action<string, string> action)
        {
            using (var _connection = _factory.CreateConnection())
            {
                using (var channel = _connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchangeName, "topic");
                    channel.QueueDeclare(queueName, true, false, false, null);
                    channel.QueueBind(queueName, exchangeName, routingKey);
                    channel.BasicQos(0, 10, false);
                    var subscription = new Subscription(channel, queueName, false);

                    while (true)
                    {
                        var deliveryArguments = subscription.Next();
                        var message = deliveryArguments.Body.DeSerializeText();
                        action.Invoke(deliveryArguments.RoutingKey, message);
                        subscription.Ack(deliveryArguments);
                    }
                }
            }
        }
        private void ProcessMessages(string exchangeName, string queueName, string routingKey, Func<string, string, bool> action)
        {
            var queueArgs = new Dictionary<string, object>
            {
                {"x-dead-letter-exchange", _configuration["QueueConfig:Monitor:DeadLetterExchange"]}
            };
            using (var _connection = _factory.CreateConnection())
            {
                using (var channel = _connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchangeName, "topic");
                    channel.QueueDeclare(queueName, true, false, false, queueArgs);
                    channel.QueueBind(queueName, exchangeName, routingKey);

                    channel.BasicQos(0, 10, false);
                    var subscription = new Subscription(channel, queueName, false);

                    while (true)
                    {
                        var deliveryArguments = subscription.Next();
                        var message = deliveryArguments.Body.DeSerializeText();
                        var ret = action.Invoke(deliveryArguments.RoutingKey, message);
                        if (ret)
                            subscription.Ack(deliveryArguments);
                        else
                            subscription.Nack(deliveryArguments, false, false);
                    }
                }
            }
        }
    }
}
