using Microsoft.Extensions.Logging;
using Server.DTOs.RequestTypes;
using Server.DTOs.ResponseTypes;
using Server.Enums;
using System.IO;
using System.Threading.Tasks;

namespace Server.Internal.RequestHandlers
{
    internal class DownloadFileRequestHandler : RequestHandler
    {
        private readonly ILogger<DownloadFileRequestHandler> _logger;

        public DownloadFileRequestHandler(ILogger<DownloadFileRequestHandler> logger)
        {
            _logger = logger;
        }

        public override async Task<byte[]> HandleAsync(byte[] requestData)
        {
            var request = GetRequest<FilePathDTO>(requestData);

            var fileInfo = new FileInfo(request.Path);
            if (fileInfo.Exists)
            {
                var response = new FileDTO
                {
                    File = await GetFileAsync(fileInfo),
                    ShortFileName = Path.GetFileName(request.Path),
                };

                return FormResponse(response, ResponseType.File);
            }
            else
            {
                throw new FileNotFoundException(request.Path);
            }
        }

        private async Task<byte[]> GetFileAsync(FileInfo fileInfo)
        {
            using var fileStream = fileInfo.OpenRead();

            var fileBytes = new byte[fileStream.Length];
            await fileStream.ReadAsync(fileBytes, offset: 0, count: (int)fileStream.Length);

            return fileBytes;
        }
    }
}
