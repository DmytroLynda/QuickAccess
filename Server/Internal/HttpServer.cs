using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Server.DTOs;
using Server.DTOs.ResponseTypes;
using Server.Internal.Exceptions;
using Server.Internal.Interfaces;
using System;
using System.Net;
using System.Text;
using System.Threading;
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
            Thread.CurrentThread.Name = "Server";
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
            
            byte[] response;
            try
            { 
                var requestHandler = _requestHandlerFactory.Create(request.Query);
                response = await requestHandler.HandleAsync(request.Request);
            }
            catch (Exception ex)
            {
                var serverException = new ServerException(ex);
                response = FormErrorResponse(serverException);
            }

            using var httpResponse = incomingContext.Response;
            await httpResponse.OutputStream.WriteAsync(response);
        }

        private byte[] FormErrorResponse(Exception exception)
        {
            var error = new ErrorDTO
            {
                Exception = exception
            };
            var serializedError = JsonConvert.SerializeObject(error);
            var errorsBytes = Encoding.UTF8.GetBytes(serializedError);

            var response = new ResponseDTO
            {
                Type = Enums.ResponseType.Error,
                Data = errorsBytes,
            };

            var serializedResponse = JsonConvert.SerializeObject(response);
            return Encoding.UTF8.GetBytes(serializedResponse);
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
