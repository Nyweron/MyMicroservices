using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.API.RabbitMq;
using Ordering.Application.Queries;
using Ordering.Application.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ordering.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly EventBusRabbitMqConsumer _eventBusRabbitMqConsumer;
        private readonly IMediator _mediator;

        public OrderController(EventBusRabbitMqConsumer eventBusRabbitMqConsumer,
                               IMediator mediator)
        {
            _eventBusRabbitMqConsumer = eventBusRabbitMqConsumer;
            _mediator = mediator;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    return Ok(await _orderRepository.GetOrdersByUserName());
        //}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrdersByUserName(string userName)
        {
            var query = new GetOrderByUserNameQuery(userName);
            var orders = await _mediator.Send(query);
            return Ok(orders);
        }

        [HttpGet("ReceiveFromQueue")]
        public async Task<IActionResult> GetMsgFromQueue()
        {
            _eventBusRabbitMqConsumer.Consume();
            return Ok();
        }
    }
}
