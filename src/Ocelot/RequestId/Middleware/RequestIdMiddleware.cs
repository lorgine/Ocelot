using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Ocelot.Infrastructure.RequestData;
using Ocelot.Logging;
using Ocelot.Middleware;

namespace Ocelot.RequestId.Middleware
{
    public class RequestIdMiddleware : OcelotMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IOcelotLogger _logger;
        private readonly IRequestScopedDataRepository _requestScopedDataRepository;

        public RequestIdMiddleware(RequestDelegate next,
            IOcelotLoggerFactory loggerFactory,
            IRequestScopedDataRepository requestScopedDataRepository)
            :base(requestScopedDataRepository)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestIdMiddleware>();
            _requestScopedDataRepository = requestScopedDataRepository;
        }

        public async Task Invoke(HttpContext context)
        {         
            _logger.LogDebug("started calling request id middleware");

            SetOcelotRequestId(context);

            _logger.LogDebug("set request id");

            _logger.LogDebug("calling next middleware");

            await _next.Invoke(context);
            
            _logger.LogDebug("succesfully called next middleware");
        }

        private void SetOcelotRequestId(HttpContext context)
        {
            var key = DefaultRequestIdKey.Value;

            if (DownstreamRoute.ReRoute.RequestIdKey != null)
            {
                key = DownstreamRoute.ReRoute.RequestIdKey;
            }

            StringValues requestId;

            if (context.Request.Headers.TryGetValue(key, out requestId))
            {
                _requestScopedDataRepository.Add("RequestId", requestId.First());

                context.TraceIdentifier = requestId;
            }
        }
    }
}