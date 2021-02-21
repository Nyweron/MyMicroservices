using AutoMapper;
using MediatR;
using Ordering.Application.Queries;
using Ordering.Application.Responses;
using Ordering.Core.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Handlers.QueryHandlers
{
    public class GetOrderByUserNameHandler : IRequestHandler<GetOrderByUserNameQuery, IEnumerable<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrderByUserNameHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderResponse>> Handle(GetOrderByUserNameQuery query, CancellationToken cancellationTokenn)
        {
            var orderList = await _orderRepository.GetOrdersByUserName(query.UserName);

            var orderResponseList = _mapper.Map<IEnumerable<OrderResponse>>(orderList);

            return orderResponseList;
        }
    }
}
