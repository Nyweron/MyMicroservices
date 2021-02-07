using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusRabbitMq.Producer
{
    public class EventBusRabbitMqProducer
    {
        private readonly IRabbitMqConnection _rabbitMqConnection;
        public EventBusRabbitMqProducer(IRabbitMqConnection rabbitMqConnection)
        {
            _rabbitMqConnection = rabbitMqConnection;
        }

        public async Task Publish()
        {
            var channel = _rabbitMqConnection.CreateModel();

            channel.QueueDeclare(queue: "test",
                      durable: false,
                      exclusive: false,
                      autoDelete: false,
                      arguments: null);


            var message = "Test";
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: "",
                                 routingKey: "test",
                                 basicProperties: null,
                                 body: body);
            Console.WriteLine(" [x] Sent {0}", message);


        }
    }
}
