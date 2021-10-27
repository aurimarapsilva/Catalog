using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace catalog.infra.RabbitMQ
{
    public class RabbitMQPersistentConnection : IRabbitMQPersistentConnection
    {
        private string _hostName;
        private string _message; 

        public RabbitMQPersistentConnection(string hostName)
        {
            _hostName = hostName;
        }
        
        public string GetMessage()
        {
            return _message;
        }

        public void DisposeMessage()
        {
            _message = string.Empty;
        }

        private void Consumer_Received(
            object sender, BasicDeliverEventArgs e)
        {
            _message =  $"[Nova mensagem | {DateTime.Now:yyyy-MM-dd HH:mm:ss}] " +
                Encoding.UTF8.GetString(e.Body.ToArray());
        }

        public string Receivement(string queue, bool durable, bool exclusive, bool autoDelete)
        {
            var message = "";
            var factory = new ConnectionFactory() { HostName = _hostName };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queue,
                                         durable: durable,
                                         exclusive: exclusive,
                                         autoDelete: autoDelete,
                                         arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += Consumer_Received;

                    channel.BasicConsume(queue: queue,
                                 autoAck: true,
                                 consumer: consumer);
                    return message;
                }
            }
        }

        public string Send(string queue, bool durable, bool exclusive, bool autoDelete, string message)
        {
            var factory = new ConnectionFactory() { HostName = _hostName };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queue,
                                         durable: durable,
                                         exclusive: exclusive,
                                         autoDelete: autoDelete,
                                         arguments: null);

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: queue,
                                         basicProperties: null,
                                         body: body);
                    return $"{message}";
                }
            }
        }
    }
}
