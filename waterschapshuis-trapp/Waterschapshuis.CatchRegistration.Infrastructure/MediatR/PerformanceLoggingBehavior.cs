using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.ApplicationServices.Logging;
using Waterschapshuis.CatchRegistration.Core.Helpers;

namespace Waterschapshuis.CatchRegistration.Infrastructure.MediatR
{
    public class PerformanceLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private const double LogLongExecutingHandlersSeconds = 5;
        private readonly ILogger<PerformanceLoggingBehavior<TRequest, TResponse>> _logger;

        public PerformanceLoggingBehavior(
            ILogger<PerformanceLoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            TResponse response;
            Exception? exception = default;

            var watch = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                response = await next();
            }
            catch (Exception e)
            {
                exception = e;
                throw;
            }
            finally
            {
                watch.Stop();

                if (watch.ElapsedMilliseconds > LogLongExecutingHandlersSeconds * 1000)
                {
                    _logger.LogDebug(GenerateLogMessage(request, exception, watch.ElapsedMilliseconds));
                }
            }

            return response;
        }

        private static string GenerateLogMessage(TRequest request, Exception? exception, long elapsedMilliseconds)
        {
            var message = $"Long running request \"{request.GetGenericTypeName(true)}\" execution time {elapsedMilliseconds} ms.";

            if (RequestLogging.ShouldRequestTypeBeLogged(typeof(TRequest)))
            {
                message += $" Request object:" +
                    Environment.NewLine +
                    JsonConvert.SerializeObject(request, Formatting.None,
                        new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }

            if (exception != null)
            {
                message +=
                    Environment.NewLine +
                    $"{exception.GetGenericTypeName()} occurred: {exception.Message}";
            }

            return message;
        }
    }
}
