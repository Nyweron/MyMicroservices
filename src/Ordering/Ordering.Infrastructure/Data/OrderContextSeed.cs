using Ordering.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.Infrastructure.Data
{
    public class OrderContextSeed
    {
        public static void Seed(OrderContext context)
        {
            context.Database.EnsureCreated();

            if (context.Orders.Any())
            {
                return;   // DB has been seeded
            }

            context.AddRangeAsync(GetPreconfiguredOrders());
            context.SaveChangesAsync();
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>()
            {
                new Order() { UserName = "swn", FirstName = "Adam", LastName = "Malina", EmailAddress = "ada@ozk.com", AddressLine = "Dworcowa", TotalPrice = 5239 },
                new Order() { UserName = "swn", FirstName = "Ola", LastName = "Banan", EmailAddress ="ban@ars.com", AddressLine = "Fabryczna", TotalPrice = 3486 }
            };
        }
    }
}
