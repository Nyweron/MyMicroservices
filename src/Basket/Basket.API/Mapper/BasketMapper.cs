using AutoMapper;
using Basket.API.Entities;
using EventBusRabbitMq.Events;

namespace Basket.API.Mapper
{
    public class BasketMapper : Profile
    {
        public BasketMapper()
        {
            CreateMap<BasketCheckout, BasketCheckoutEvent>();
        }
    }
}
