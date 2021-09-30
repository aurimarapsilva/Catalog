namespace catalog.infra.RabbitMQ
{
    public interface IRabbitMQPersistentConnection
    {
        string Receivement(string queue, bool durable, bool exclusive, bool autoDelete);
        string Send(string queue, bool durable, bool exclusive, bool autoDelete, string message);
    }
}
