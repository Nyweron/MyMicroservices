using StackExchange.Redis;

namespace Basket.API.Data.interfaces
{
    public interface IBasketContext
    {
        IDatabase Redis { get; }
    }
}
