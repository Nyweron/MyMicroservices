using RabbitMQ.Client;
using System;

namespace EventBusRabbitMq
{
    public interface IRabbitMqConnection : IDisposable
    {
        bool TryConnect();
        bool IsConnected { get; }
        IModel CreateModel();
    }
}
