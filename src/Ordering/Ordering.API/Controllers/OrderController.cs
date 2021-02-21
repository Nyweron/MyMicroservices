using Microsoft.AspNetCore.Mvc;
using Ordering.API.RabbitMq;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ordering.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly EventBusRabbitMqConsumer _eventBusRabbitMqConsumer;

        public OrderController(IOrderRepository orderRepository, EventBusRabbitMqConsumer eventBusRabbitMqConsumer)
        {
            _orderRepository = orderRepository;
            _eventBusRabbitMqConsumer = eventBusRabbitMqConsumer;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _orderRepository.GetOrdersByUserName());
        }

        [HttpGet("ReceiveFromQueue")]
        public async Task<IActionResult> GetMsgFromQueue()
        {
            _eventBusRabbitMqConsumer.Consume();
            return Ok();
        }
    }
}
