using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!await orderContext.Orders.AnyAsync())
            {
                await orderContext.Orders.AddRangeAsync();
                await orderContext.SaveChangesAsync();
                logger.LogInformation("data seed section configured");
            }
        }
        public static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>()
            {
                new Order()
                {
                    Firstname="dgdf",
                    Lastname="gdfgdfg",
                    Username="dfgdfg",
                    EmailAddress="dfgdfgd",
                    City="dfgdfg",
                    Country="dfgdfgdf",
                    TotalPrice=10000
                }
            };
        }
    }
}
