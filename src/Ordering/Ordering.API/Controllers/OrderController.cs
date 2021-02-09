using Microsoft.AspNetCore.Mvc;
using Ordering.Core.Entities;
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


        public OrderController()
        {

        }

        [HttpGet("[action]/{productName}")]
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Order>> GetOrdersByUserName(string username)
        {
            return Ok(await _orderRepository.GetOrdersByUserName(username));
        }
    }
}
