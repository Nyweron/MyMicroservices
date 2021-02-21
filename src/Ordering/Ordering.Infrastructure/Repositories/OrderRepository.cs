using Microsoft.EntityFrameworkCore;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;
using Ordering.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _context;

        public OrderRepository(OrderContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
            return await _context.Orders
                .Where(x => x.UserName == userName)
                .ToListAsync();
        }
    }
}
