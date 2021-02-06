using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Server.DTOs.RequestTypes;
using Server.DTOs.ResponseTypes;
using Server.Enums;
using Server.Internal.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Server.Internal.RequestHandlers
{
    internal class DownloadFileRequestHandler : RequestHandler
    {
        private const int BytesInMegabyte = 1048576;

        private readonly ILogger<DownloadFileRequestHandler> _logger;
        private readonly DownloadFileRequestHandlerOptions _options;

        public DownloadFileRequestHandler(ILogger<DownloadFileRequestHandler> logger, IOptions<DownloadFileRequestHandlerOptions> options)
        {
            _logger = logger;
            _options = options.Value;
        }

        public override async Task<byte[]> HandleAsync(byte[] requestData)
        {
            var request = GetRequest<FileRequestDTO>(requestData);

            var fileInfo = new FileInfo(request.Path.Path);
            if (fileInfo.Exists)
            {
                var responseFile = new FileDTO
                {
                    Data = await GetFileChunkAsync(fileInfo, request.Chunk),
                    ShortFileName = Path.GetFileName(request.Path.Path),
                };

                var response = new FileChunkDTO
                {
                    File = responseFile,
                    AmountOfChunks = GetAmountOfChunks(fileInfo)
                };

                return FormResponse(response, ResponseType.File);
            }
            else
            {
                throw new FileNotFoundException(request.Path.Path);
            }
        }

        private int GetAmountOfChunks(FileInfo fileInfo)
        {
            var fileLength = fileInfo.Length;

            double chunks = (double)fileLength / (_options.ChunkSizeInMegabytes * BytesInMegabyte);

            return (int)Math.Ceiling(chunks);
        }

        private async Task<byte[]> GetFileChunkAsync(FileInfo fileInfo, int chunk)
        {
            using var fileStream = fileInfo.OpenRead();

            var offset = (chunk - 1) * _options.ChunkSizeInMegabytes * BytesInMegabyte;

            var available = fileStream.Length - offset;

            var chunkInBytes = _options.ChunkSizeInMegabytes * BytesInMegabyte;
            var count = Math.Min(available, chunkInBytes);

            var fileBytes = new byte[count];
            fileStream.Position = offset;
            await fileStream.ReadAsync(fileBytes, offset: 0, (int)count);

            return fileBytes;
        }
    }
}
