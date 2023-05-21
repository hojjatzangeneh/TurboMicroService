using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contract.Prisistence;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;
        private readonly ILogger<DeleteOrderCommandHandler> logger;
        public DeleteOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<DeleteOrderCommandHandler> logger)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
            this.logger = logger;
        }
        public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderForDelete = await orderRepository.GetByIdAsync(request.Id);
            if (orderForDelete == null) { logger.LogError("Order is not exists"); }
            else
            {
                mapper.Map(request, orderForDelete, typeof(UpdateOrderCommand), typeof(Order));
                await orderRepository.Delete(orderForDelete);
                logger.LogInformation($"order {orderForDelete.Id} is successfully deleted");
            }
        }
    }
}
