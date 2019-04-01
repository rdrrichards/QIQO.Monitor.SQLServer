
using Microsoft.Extensions.Configuration;

namespace QIQO.MQ
{
    public interface IMQPublisher
    {
        void Send(object thing, string routingKey);
        void Send(object thing, string exchangeName, string queueName, string routingKey);
    }
    public class MQPublisher : Publisher, IMQPublisher
    {
        public MQPublisher(IConfiguration configuration) : base(configuration) { }
        public void Send(object thing, string routingKey)
        {
            SendMessage(thing, routingKey);
        }
        public void Send(object thing, string exchangeName, string queueName, string routingKey)
        {
            SendMessage(thing, exchangeName, queueName, routingKey);
        }
    }

    //public interface IMQConsumer
    //{
    //    void Pull(string routingKey);
    //}
    //public class MQConsumer : Consumer, IMQConsumer
    //{
    //    public void Pull(string routingKey)
    //    {
    //        ReceiveMessage();
    //    }
    //}
}
