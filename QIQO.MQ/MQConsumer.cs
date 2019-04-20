using Microsoft.Extensions.Configuration;
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
        public MQConsumer(IConfiguration configuration) : base(configuration) { }
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
