using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contract.Prisistence;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        private readonly OrderContext orderContext;
        public OrderRepository(OrderContext orderContext) : base(orderContext)
        {
            this.orderContext = orderContext;
        }
        public Task<IEnumerable<Order>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetOrdersByUsername(string username)
        {
            return await orderContext.Orders.Where(w=>w.Username==username).ToListAsync();
        }
    }
}
