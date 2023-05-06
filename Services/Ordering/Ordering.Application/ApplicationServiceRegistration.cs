using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using Ordering.Application.Behavaiours;

namespace Ordering.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddTransient(typeof(IPipelineBehavior<,>),typeof(UnhandleExceptionBehavaiour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavaiour<,>));

            return services;
        }
    }
}
