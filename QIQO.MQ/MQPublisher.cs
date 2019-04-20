using Microsoft.Extensions.Configuration;

namespace QIQO.MQ
{
    public interface IMQPublisher
    {
        void Enqueue(object thing, string routingKey);
        void Enqueue(object thing, string exchangeName, string queueName, string routingKey);
    }
    public class MQPublisher : Publisher, IMQPublisher
    {
        public MQPublisher(IConfiguration configuration) : base(configuration) { }
        public void Enqueue(object thing, string routingKey) => SendMessage(thing, routingKey);
        public void Enqueue(object thing, string exchangeName, string queueName, string routingKey) => SendMessage(thing, exchangeName, queueName, routingKey);
    }
}
