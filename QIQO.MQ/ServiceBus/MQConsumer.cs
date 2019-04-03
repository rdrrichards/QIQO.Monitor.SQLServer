using Microsoft.Extensions.Configuration;

namespace QIQO.MQ
{
    public interface IMQConsumer
    {
        void Pull(string exchangeName, string queueName, string routingKey);
    }
    public class MQConsumer : Consumer, IMQConsumer
    {
        public MQConsumer(IConfiguration configuration) : base(configuration) { }

        public void Pull(string exchangeName, string queueName, string routingKey)
        {
            ReceiveMessages(exchangeName, queueName, routingKey);
        }
    }
}
