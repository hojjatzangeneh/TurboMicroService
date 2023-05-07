using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Contract.Infrastructure;
using Ordering.Application.Contract.Prisistence;
using Ordering.Application.Models;
using Ordering.Infrastructure.Mail;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Ordering.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderContext>(optionsAction: options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString"));
            });
            services.AddScoped(typeof(IAsynceRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IEmailService, EmailService>();
            return services;
        }
    }
}
