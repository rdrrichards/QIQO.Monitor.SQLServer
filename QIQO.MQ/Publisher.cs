using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace QIQO.MQ
{
    public class Publisher
    {
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _model;

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
            CreateConnection(exchangeName, queueName, routingKey);
            _model.BasicPublish(exchangeName, routingKey, null, message);
            Close();
        }

        private void CreateConnection(string exchangeName, string queueName, string routingKey)
        {
            _factory = new ConnectionFactory
            {
                HostName = _configuration["QueueConfig:Server"],
                UserName = _configuration["QueueConfig:User"],
                Password = _configuration["QueueConfig:Password"]
            };

            _connection = _factory.CreateConnection();
            _model = _connection.CreateModel();
            _model.ExchangeDeclare(exchangeName, "topic");

            _model.QueueDeclare(queueName, true, false, false, null);

            _model.QueueBind(queueName, exchangeName, routingKey);
        }
        private void Close()
        {
            _connection.Close();
        }
    }
}
