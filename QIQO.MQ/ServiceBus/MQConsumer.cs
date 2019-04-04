using Microsoft.Extensions.Configuration;
using System;

namespace QIQO.MQ
{
    public interface IMQConsumer
    {
        void Pull(string exchangeName, string queueName, string routingKey, Action<string, string> action);
    }
    public class MQConsumer : Consumer, IMQConsumer
    {
        public MQConsumer(IConfiguration configuration) : base(configuration) { }

        public void Pull(string exchangeName, string queueName, string routingKey, Action<string, string> action)
        {
            ReceiveMessages(exchangeName, queueName, routingKey, action);
        }
    }
}
