using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrderList
{
    public class GetOrderListQuery : IRequest<List<OrderDTO>>
    {
        public string Username { get; set; }
        public GetOrderListQuery(string username)
        {
            this.Username = username;
        }
    }
}
