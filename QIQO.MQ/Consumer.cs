using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;

namespace QIQO.MQ
{
    public class Consumer
    {
        private readonly ConnectionFactory _factory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<Consumer> _logger;

        public Consumer(IConfiguration configuration, ILogger<Consumer> logger)
        {
            _configuration = configuration;
            _logger = logger;
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
            var connection = _factory.CreateConnection();
            var channel = connection.CreateModel();
            _logger.LogInformation($"ProcessMessages (Action) ExchangeDeclare Name {exchangeName}");
            channel.ExchangeDeclare(exchangeName, ExchangeType.Topic);
            channel.QueueDeclare(queueName, true, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey);
            channel.BasicQos(0, 10, false);
            var consumer = new EventingBasicConsumer(channel); //, queueName, false);
                consumer.Received += (model, ea) =>
                {
                    _logger.LogInformation($"ProcessMessages (Action) Received Routing Key: {ea.RoutingKey}");
                    var message = ea.Body.DeSerializeText();
                    // _logger.LogInformation($"ProcessMessages (Action) Received Message {message}");
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
            var connection = _factory.CreateConnection();
            var channel = connection.CreateModel();

            _logger.LogInformation($"ProcessMessages (Func) ExchangeDeclare Name {exchangeName}");
            channel.ExchangeDeclare(exchangeName, ExchangeType.Topic);
            channel.QueueDeclare(queueName, true, false, false, queueArgs);
            channel.QueueBind(queueName, exchangeName, routingKey);
            channel.BasicQos(0, 10, false);
            var consumer = new EventingBasicConsumer(channel); //, queueName, false);
            consumer.Received += (model, ea) =>
            {
                _logger.LogInformation($"ProcessMessages (Func) Received Routing Key: {ea.RoutingKey}");
                var message = ea.Body.DeSerializeText();
                // _logger.LogInformation($"ProcessMessages (Func) Received Message {message}");
                var ret = action.Invoke(ea.RoutingKey, message);
                if (ret)
                    channel.BasicAck(ea.DeliveryTag, false);
                else
                    channel.BasicReject(ea.DeliveryTag, false);
            };
            channel.BasicConsume(queue: queueName,
                            autoAck: false,
                            consumer: consumer);

        }
    }
}
