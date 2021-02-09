using Ordering.Core.Entities;
using Ordering.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
