using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace QIQO.MQ
{
    public interface IMQConsumer
    {
        void Dequeue(string exchangeName, string queueName, string routingKey, Action<string, string> action);
        void Dequeue(string exchangeName, string queueName, string routingKey, Func<string, string, bool> action);
    }
    public class MQConsumer : Consumer, IMQConsumer
    {
        public MQConsumer(IConfiguration configuration, ILogger<MQConsumer> logger) : base(configuration, logger) { }
        public void Dequeue(string exchangeName, string queueName, string routingKey, Action<string, string> action)
        {
            ReceiveMessages(exchangeName, queueName, routingKey, action);
        }
        public void Dequeue(string exchangeName, string queueName, string routingKey, Func<string, string, bool> action)
        {
            ReceiveMessages(exchangeName, queueName, routingKey, action);
        }
    }
}
