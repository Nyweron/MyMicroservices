using RabbitMQ.Client;
using System;

namespace EventBusRabbitMq
{
    interface IRabbitMqConnection : IDisposable
    {
        bool TryConnect();
        bool IsConnected { get; }
        IModel CreateModel();
    }
}
