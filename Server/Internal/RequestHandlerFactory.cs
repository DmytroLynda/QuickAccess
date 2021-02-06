using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;
using System;
using ServerInterface.Enums;
using ServerInterface.Internal.Interfaces;
using ServerInterface.Internal.Exceptions;

namespace ServerInterface.Internal
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
                throw new RequestHandlerDoesNotExistException(query.ToString() + requestHandlerEnd);
            }

            return requiredHandler;
        }
    }
}
