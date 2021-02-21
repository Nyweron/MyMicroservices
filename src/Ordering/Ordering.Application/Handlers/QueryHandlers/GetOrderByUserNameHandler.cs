using Ordering.Application.Queries;
using Ordering.Application.Responses;
using Ordering.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Handlers.QueryHandlers
{
    public class GetOrderByUserNameHandler
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByUserNameHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderResponse>> Handle(GetOrderByUserNameQuery query)
        {
            var orderList = await _orderRepository.GetOrdersByUserName(query.UserName);

            //mapper

            return new List<OrderResponse>();
        }
    }
}
