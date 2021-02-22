using AutoMapper;
using DomainEntities;
using Microsoft.Extensions.Logging;
using ServerInterface.DTOs.RequestTypes;
using ServerInterface.Enums;
using ServerInterface.Internal.Exceptions;
using ServerLogic;
using System.IO;
using System.Threading.Tasks;

namespace ServerInterface.Internal.RequestHandlers
{
    internal class GetFileInfoRequestHandler : RequestHandler
    {
        private readonly ILogger<GetFileInfoRequestHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IFileInfoService _service;

        public GetFileInfoRequestHandler(ILogger<GetFileInfoRequestHandler> logger, IMapper mapper, IFileInfoService service)
        {
            _logger = logger;
            _mapper = mapper;
            _service = service;
        }

        public override async Task<byte[]> HandleAsync(byte[] data)
        {
            var requestDto = GetRequest<FilePathDTO>(data);
            var request = _mapper.Map<FilePath>(requestDto);

            var fileInfo = await _service.GetFileInfoAsync(request);
            var fileInfoDto = _mapper.Map<FileInfoDTO>(fileInfo);

            return FormResponse(fileInfoDto, ResponseType.FileInfo);
        }
    }
}
