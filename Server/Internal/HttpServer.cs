using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Server.DTOs;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server.Internal
{
    internal class HttpServer : IServer
    {
        private readonly ILogger<HttpServer> _logger;
        private readonly IRequestHandlerFactory _requestHandlerFactory;

        public HttpServer(ILogger<HttpServer> logger, IRequestHandlerFactory requestHandlerFactory)
        {
            _logger = logger;
            _requestHandlerFactory = requestHandlerFactory;
        }

        public async Task StartAsync(string serverUri)
        {
            var httpListener = new HttpListener();
            httpListener.Prefixes.Add(serverUri);
            httpListener.Start();

            while (true)
            {
                var incomingContext = await httpListener.GetContextAsync();

                try
                {
                    await HandleRequestAsync(incomingContext);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(ex.Message, ex);
                }
            }
        }

        private async Task HandleRequestAsync(HttpListenerContext incomingContext)
        {
            var httpRequest = incomingContext.Request;

            var request = await GetRequest(httpRequest);

            var requestHandler = _requestHandlerFactory.Create(request.Query);
            var response = await requestHandler.HandleAsync(request.Request);

            var httpResponseStream = incomingContext.Response.OutputStream;
            await httpResponseStream.WriteAsync(response);
            await httpResponseStream.FlushAsync();
        }

        private async Task<RequestDTO> GetRequest(HttpListenerRequest httpRequest)
        {
            var requestBytes = new byte[httpRequest.ContentLength64];

            using var httpRequestStream = httpRequest.InputStream;
            await httpRequestStream.ReadAsync(requestBytes);

            var serializedRequest = Encoding.UTF8.GetString(requestBytes);

            return JsonConvert.DeserializeObject<RequestDTO>(serializedRequest);
        }
    }
}
