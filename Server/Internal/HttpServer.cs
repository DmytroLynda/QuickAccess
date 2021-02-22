using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ServerInterface.DTOs;
using ServerInterface.DTOs.ResponseTypes;
using ServerInterface.Enums;
using ServerInterface.Internal.Exceptions;
using ServerInterface.Internal.Interfaces;
using ServerInterface.Internal.Options;
using ServerLogic;
using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerInterface.Internal
{
    internal class HttpServer : IServer
    {
        private readonly ILogger<HttpServer> _logger;
        private readonly IRequestHandlerFactory _requestHandlerFactory;
        private readonly IAuthenticationService _authenticationService;
        private readonly HttpServerOptions _options;
        private readonly HttpListener _httpListener;

        public HttpServer(
            ILogger<HttpServer> logger,
            IRequestHandlerFactory requestHandlerFactory,
            IAuthenticationService authenticationService,
            IOptions<HttpServerOptions> options,
            HttpListener httpListener)
        {
            _logger = logger;
            _requestHandlerFactory = requestHandlerFactory;
            _authenticationService = authenticationService;
            _options = options.Value;
            _httpListener = httpListener;
        }

        public void Start()
        {
            StartAsync().Wait();
        }

        public async Task StartAsync()
        {
            _options.Prefixes.ToList().ForEach(prefix => _httpListener.Prefixes.Add(prefix));
            _httpListener.Start();

            while (true)
            {
                var incomingContext = await _httpListener.GetContextAsync();

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

            byte[] response;
            try
            {
                var requesterEndPoint = incomingContext.Request.RemoteEndPoint;
                await _authenticationService.AuthenticateAsync(requesterEndPoint.Address.ToString(), requesterEndPoint.Port);

                var request = await GetRequest(httpRequest);

                var requestHandler = _requestHandlerFactory.Create(request.Query);
                response = await requestHandler.HandleAsync(request.Data);
            }
            catch (Exception ex)
            {
                response = FormErrorResponse(new ServerException(ex));
            }

            using var httpResponse = incomingContext.Response;
            await httpResponse.OutputStream.WriteAsync(response);
        }

        private byte[] FormErrorResponse(ServerException exception)
        {
            var error = new ErrorDTO
            {
                Exception = exception
            };
            var serializedError = JsonConvert.SerializeObject(error);
            var errorsBytes = Encoding.UTF8.GetBytes(serializedError);

            var response = new ResponseDTO
            {
                Type = ResponseType.Error,
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
