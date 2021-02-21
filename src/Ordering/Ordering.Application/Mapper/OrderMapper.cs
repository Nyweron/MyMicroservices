using AutoMapper;
using Ordering.Application.Responses;
using Ordering.Core.Entities;


namespace Ordering.Application.Mapper
{
    public class OrderMapper : Profile
    {
        public OrderMapper()
        {
            CreateMap<Order, OrderResponse>();
        }
    }
}
