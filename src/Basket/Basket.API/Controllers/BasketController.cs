using AutoMapper;
using Basket.API.Entities;
using Basket.API.Repositories.interfaces;
using EventBusRabbitMq.Events;
using EventBusRabbitMq.Producer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepositry;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly EventBusRabbitMqProducer _eventBusRabbitMqProducer;

        public BasketController(
            IBasketRepository basketRepositry,
            ILogger logger,
            IMapper mapper,
            EventBusRabbitMqProducer eventBusRabbitMqProducer)
        {
            _basketRepositry = basketRepositry;
            _logger = logger;
            _mapper = mapper;
            _eventBusRabbitMqProducer = eventBusRabbitMqProducer;
        }

        [HttpGet]
        [ProducesResponseType(typeof(BasketCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BasketCart>> GetBasket(string userName)
        {
            return Ok(await _basketRepositry.GetBasket(userName));
        }

        [HttpDelete]
        [ProducesResponseType(typeof(BasketCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BasketCart>> DeleteByUsername(string userName)
        {
            return Ok(await _basketRepositry.DeleteBasket(userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(BasketCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BasketCart>> Update([FromBody] BasketCart basketCart)
        {

            return Ok(await _basketRepositry.UpdateBasket(basketCart));
        }



        [HttpGet("SendToQueue")]
        public async Task<ActionResult> SendToQueue()
        {
            var basketCheckout = new BasketCheckoutEvent();

            _eventBusRabbitMqProducer.Publish(basketCheckout);

            return Ok();
        }

        public async Task<ActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            // get total price of the basket
            // remove the basket 
            // send checkout event to rabbitMq 


            return Ok();
        }
    }
}
