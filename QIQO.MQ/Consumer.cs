using System;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.MessagePatterns;

namespace QIQO.MQ
{
    public class Consumer
    {
        private ConnectionFactory _factory;
        private IConnection _connection;

        private readonly IConfiguration _configuration;

        public Consumer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ReceiveMessages(string exchangeName, string queueName, string routingKey, Action<string, string> action)
        {
            ProcessMessages(_configuration["QueueConfig:Server"], _configuration["QueueConfig:User"],
                _configuration["QueueConfig:Password"], exchangeName, queueName, routingKey, action);
        }

        private void ProcessMessages(string hostName, string userName, string password,
            string exchangeName, string queueName, string routingKey, Action<string, string> action)
        {
            _factory = new ConnectionFactory { HostName = hostName, UserName = userName, Password = password };
            using (_connection = _factory.CreateConnection())
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
                        // Console.WriteLine("Message Received '{0}'", message);
                        subscription.Ack(deliveryArguments);
                    }
                }
            }
        }
    }
}
