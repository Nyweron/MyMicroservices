using Ordering.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Data
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext context)
        {
            context.Database.EnsureCreated();

            if (context.Orders.Any())
            {
                return;   // DB has been seeded
            }

            context.AddRange(GetPreconfiguredOrders());
            await context.SaveChangesAsync();
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>()
            {
                new Order() {Id=1, UserName = "swn", FirstName = "Adam", LastName = "Malina", EmailAddress = "ada@ozk.com", AddressLine = "Dworcowa", TotalPrice = 5239 },
                new Order() {Id=2, UserName = "swn", FirstName = "Ola", LastName = "Banan", EmailAddress ="ban@ars.com", AddressLine = "Fabryczna", TotalPrice = 3486 }
            };
        }
    }
}
