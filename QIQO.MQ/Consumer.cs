using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.MessagePatterns;

namespace QIQO.MQ
{
    public interface IConsumer
    {
        void ReceiveMessages(string exchgName, string qName, string rtKey);
        void StopRecieving();
    }
    public class Consumer : IConsumer
    {
        private ConnectionFactory _factory;
        private IConnection _connection;
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public void ReceiveMessages(string exchgName, string qName, string rtKey)
        {
            //ProcessMessages();
        }

        public void StopRecieving()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }

        private void ProcessMessages(string hostName, string userName, string password, string exchangeName, string queueName, string routingKey)
        {
            var token = cancellationTokenSource.Token;
            var listener = Task.Factory.StartNew(() =>
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
                            // var message = deliveryArguments.Body.DeSerializeText();

                            // Console.WriteLine("Message Received '{0}'", message);
                            subscription.Ack(deliveryArguments);
                            if (token.IsCancellationRequested)
                                break;
                        }
                    }
                }
            }, token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }
    }
}
