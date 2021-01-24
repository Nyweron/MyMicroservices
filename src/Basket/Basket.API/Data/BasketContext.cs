﻿using Basket.API.Data.interfaces;
using StackExchange.Redis;

namespace Basket.API.Data
{
    public class BasketContext : IBasketContext
    {
        private readonly ConnectionMultiplexer _redisConnection;
        public BasketContext(ConnectionMultiplexer redisConnection)
        {
            _redisConnection = redisConnection;
            Redis = redisConnection.GetDatabase();
        }
        public IDatabase Redis { get; }
    }
}
