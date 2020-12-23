using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class HttpServer : IServer
    {
        private readonly ILogger<HttpServer> _logger;

        public HttpServer(ILogger<HttpServer> logger)
        {
            _logger = logger;
        }

        public Task StartAsync()
        {
            throw new NotImplementedException();
        }
    }
}
