using MediatR;
using Microsoft.Extensions.Logging;

namespace Ordering.Application.Behavaiours
{
    public class UnhandleExceptionBehavaiour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> logger;

        public UnhandleExceptionBehavaiour(ILogger<TRequest> logger)
        {
            this.logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception e)
            {
                var requestName = typeof(TRequest).Name;
                logger.LogError(e, $"Applicaation request: Unhandle Exception for request {requestName} {request}");
                throw;
            }
        }
    }
}
