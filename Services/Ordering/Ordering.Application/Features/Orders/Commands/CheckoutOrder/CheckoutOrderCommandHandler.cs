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

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;
        private readonly IEmailService emailService;
        private readonly ILogger logger;

        public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, IEmailService emailService, ILogger logger)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
            this.emailService = emailService;
            this.logger = logger;
        }

        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = mapper.Map<Order>(request);
            var newOrder = await orderRepository.AddAsynce(orderEntity);
            logger.LogInformation("Create New Order");
            // Send Email
            await SendEmail(newOrder);
            return newOrder.Id;
        }
        private async Task SendEmail(Order order)
        {
            try
            {
                await emailService.SendEmailAsync(new Models.Email
                {
                    To = "test@test.com",
                    Subject = "sdhjcdc",
                    Body = "sdjsdjfdshfd"
                });
            }
            catch (Exception)
            {

                logger.LogError("error");
            }
        }
    }
}
