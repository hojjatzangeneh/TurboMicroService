using AutoMapper;
using MediatR;
using Ordering.Application.Contract.Prisistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrderList
{
    public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, List<OrderDTO>>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;
        public GetOrderListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }

        public async Task<List<OrderDTO>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
        {
            var orderList = await orderRepository.GetOrdersByUsername(request.Username);
            return mapper.Map<List<OrderDTO>>(orderList);
        }
    }
}
