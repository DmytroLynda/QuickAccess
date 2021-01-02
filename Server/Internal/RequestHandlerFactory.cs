using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Server.Enums;
using Server.Internal.Interfaces;
using System.Linq;
using System;
using Server.Internal.Exceptions;

namespace Server.Internal
{
    internal class RequestHandlerFactory : IRequestHandlerFactory
    {
        private readonly ILogger<RequestHandlerFactory> _logger;
        private readonly IServiceProvider _serviceProvider;

        private const string requestHandlerEnd = "RequestHandler";

        public RequestHandlerFactory(ILogger<RequestHandlerFactory> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public IRequestHandler Create(Query query)
        {
            var handlers = _serviceProvider.GetServices<IRequestHandler>();
            var requiredHandler = handlers.FirstOrDefault(handler => handler.GetType().Name == query.ToString() + requestHandlerEnd);

            if (requiredHandler is null)
            {
                throw new RequiredRequestHandlerDoesNotExistException(query.ToString() + requestHandlerEnd);
            }

            return requiredHandler;
        }
    }
}
