using EventBusRabbitMq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.RabbitMq
{
    public class EventBusRabbitMqConsumer
    {
        private readonly IRabbitMqConnection _rabbitMqConnection;
        public EventBusRabbitMqConsumer(IRabbitMqConnection rabbitMqConnection)
        {
            _rabbitMqConnection = rabbitMqConnection;
        }

        public void Consume()
        {
            var channel = _rabbitMqConnection.CreateModel();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                // copy or deserialise the payload
                // and process the message
                // ...
                channel.BasicAck(ea.DeliveryTag, false);
            };
            // this consumer tag identifies the subscription
            // when it has to be cancelled
            String consumerTag = channel.BasicConsume("test", false, consumer);
        }
    }
}
