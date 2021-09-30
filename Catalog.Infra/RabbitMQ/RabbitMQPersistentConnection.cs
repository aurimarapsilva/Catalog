using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace catalog.infra.RabbitMQ
{
    public class RabbitMQPersistentConnection : IRabbitMQPersistentConnection
    {
        private string _hostName;

        public RabbitMQPersistentConnection(string hostName)
        {
            _hostName = hostName;
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
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                    };

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
                    return $" [x] Sent {message}";
                }
            }
        }
    }
}
