using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServerInterface.DTOs.ResponseTypes;
using ServerInterface.DTOs.RequestTypes;
using ServerInterface.Enums;
using ServerInterface.Internal.Options;
using System.IO;
using System.Threading.Tasks;
using ServerLogic;
using AutoMapper;
using DomainEntities;

namespace ServerInterface.Internal.RequestHandlers
{
    internal class DownloadFileRequestHandler : RequestHandler
    {
        private readonly ILogger<DownloadFileRequestHandler> _logger;
        private readonly DownloadFileRequestHandlerOptions _options;
        private readonly IDownloadFileService _service;
        private readonly IMapper _mapper;

        public DownloadFileRequestHandler(
            ILogger<DownloadFileRequestHandler> logger,
            IOptions<DownloadFileRequestHandlerOptions> options, 
            IDownloadFileService service,
            IMapper mapper)
        {
            _logger = logger;
            _options = options.Value;
            _service = service;
            _mapper = mapper;
        }

        public override async Task<byte[]> HandleAsync(byte[] requestData)
        {
            var requestDto = GetRequest<FileRequestDTO>(requestData);
            var request = _mapper.Map<FileRequest>(requestDto);

            var response = await _service.DownloadFileAsync(request, _options.ChunkSizeInMegabytes);
            var responseDto = _mapper.Map<FileChunkDTO>(response);

            return FormResponse(responseDto, ResponseType.File);
        }
    }
}
