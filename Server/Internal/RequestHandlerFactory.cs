using Microsoft.Extensions.Logging;
using Server.Enums;
using Server.Internal.Interfaces;
using Server.Internal.RequestHandlers;
using System;

namespace Server.Internal
{
    internal class RequestHandlerFactory : IRequestHandlerFactory
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<RequestHandlerFactory> _logger;

        public RequestHandlerFactory(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger<RequestHandlerFactory>();
        }

        public IRequestHandler Create(Query query)
        {
            switch (query)
            {
                case Query.DownloadFile:
                    var logger = _loggerFactory.CreateLogger<DownloadFileRequestHandler>();
                    return new DownloadFileRequestHandler(logger);

                default:
                    throw new ArgumentException($"Unknown query: {query}.");
            }
        }
    }
}
