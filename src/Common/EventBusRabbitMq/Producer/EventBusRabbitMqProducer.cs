using EventBusRabbitMq.Events;
using Newtonsoft.Json;
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

        public void Publish(BasketCheckoutEvent basketCheckoutEvent)
        {
            //TODO channel confirmation etc...

            string json = JsonConvert.SerializeObject(basketCheckoutEvent);
            var body = Encoding.UTF8.GetBytes(json);

            var channel = _rabbitMqConnection.CreateModel();

            channel.QueueDeclare(queue: "test",
                      durable: false,
                      exclusive: false,
                      autoDelete: false,
                      arguments: null);

          
            channel.BasicPublish(exchange: "",
                                 routingKey: "test",
                                 basicProperties: null,
                                 body: body);
        }
    }
}
