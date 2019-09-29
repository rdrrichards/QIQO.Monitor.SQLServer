using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using System;
using System.Collections.Generic;
using System.Text;

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
            using var _connection = _factory.CreateConnection();
            using var channel = _connection.CreateModel();
            channel.ExchangeDeclare(exchangeName, "topic");
            channel.QueueDeclare(queueName, true, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey);
            channel.BasicQos(0, 10, false);
            var consumer = new EventingBasicConsumer(channel); //, queueName, false);
            consumer.Received += (model, ea) =>
            {
                var message = ea.Body.DeSerializeText();
                action.Invoke(ea.RoutingKey, message);
            };
            channel.BasicConsume(queue: queueName,
                          autoAck: true,
                          consumer: consumer);
        }
        private void ProcessMessages(string exchangeName, string queueName, string routingKey, Func<string, string, bool> action)
        {
            var queueArgs = new Dictionary<string, object>
            {
                {"x-dead-letter-exchange", _configuration["QueueConfig:Monitor:DeadLetterExchange"]}
            };
            using var _connection = _factory.CreateConnection();
            using var channel = _connection.CreateModel();
            channel.ExchangeDeclare(exchangeName, "topic");
            channel.QueueDeclare(queueName, true, false, false, queueArgs);
            channel.QueueBind(queueName, exchangeName, routingKey);

            channel.BasicQos(0, 10, false);
            var consumer = new EventingBasicConsumer(channel); //, queueName, false);
            consumer.Received += (model, ea) =>
            {
                var message = ea.Body.DeSerializeText();
                var ret = action.Invoke(ea.RoutingKey, message);
                    //if (ret)
                    //    //consumer.Ack(ea);
                    //else
                    //    //consumer.Nack(ea, false, false);
                };
            channel.BasicConsume(queue: queueName,
                          autoAck: true,
                          consumer: consumer);
        }
    }
}
