using AutoMapper;
using DomainEntities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServerInterface.DTOs;
using ServerInterface.DTOs.RequestTypes;
using ServerInterface.DTOs.ResponseTypes;
using ServerInterface.Enums;
using ServerLogic;
using ServerLogic.ExternalInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerInterface.Internal.RequestHandlers
{
    internal class OpenFolderRequestHandler : RequestHandler
    {
        private readonly ILogger<OpenFolderRequestHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IOpenDirectoryService _service;

        public OpenFolderRequestHandler(ILogger<OpenFolderRequestHandler> logger, IMapper mapper, IOpenDirectoryService service)
        {
            _logger = logger;
            _mapper = mapper;
            _service = service;
        }

        public override async Task<byte[]> HandleAsync(byte[] requestData)
        {
            var requestDto = GetRequest<DirectoryPathDTO>(requestData);
            var request = _mapper.Map<DirectoryPath>(requestDto);

            var directory = await _service.OpenDirectoryAsync(request);
            var directoryDto = _mapper.Map<List<PathDTO>>(directory);

            return FormResponse(directoryDto, ResponseType.Folder);
        }
    }
}
