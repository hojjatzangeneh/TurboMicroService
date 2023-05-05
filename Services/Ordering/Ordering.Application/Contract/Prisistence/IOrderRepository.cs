using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Contract.Prisistence
{
    public interface IOrderRepository:IAsynceRepository<Order>
    {
        Task<IEnumerable<Order>> GetAll();
        Task<IEnumerable<Order>> GetOrdersByUsername(string username);
    }
}
