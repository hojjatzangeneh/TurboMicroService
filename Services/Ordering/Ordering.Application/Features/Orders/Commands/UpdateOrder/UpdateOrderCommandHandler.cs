using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contract.Infrastructure;
using Ordering.Application.Contract.Prisistence;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;
        private readonly ILogger<UpdateOrderCommandHandler> logger;
        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<UpdateOrderCommandHandler> logger)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
            this.logger = logger;
        }
         async Task IRequestHandler<UpdateOrderCommand>.Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderForUpdate =await orderRepository.GetByIdAsync(request.Id);
            if (orderForUpdate == null) { logger.LogError("Order is not exists"); }
            mapper.Map(request, orderForUpdate, typeof(UpdateOrderCommand), typeof(Order));
            await orderRepository.Update(orderForUpdate);
            logger.LogInformation($"order {orderForUpdate.Id} is successfully updated");
        }
    }
}
