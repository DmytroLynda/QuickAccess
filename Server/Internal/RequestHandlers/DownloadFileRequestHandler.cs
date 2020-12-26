using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Server.DTOs;
using Server.DTOs.RequestTypes;
using Server.DTOs.ResponseTypes;
using Server.Enums;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Server.Internal.RequestHandlers
{
    internal class DownloadFileRequestHandler : IRequestHandler
    {
        private readonly ILogger<DownloadFileRequestHandler> _logger;

        public DownloadFileRequestHandler(ILogger<DownloadFileRequestHandler> logger)
        {
            _logger = logger;
        }

        public async Task<byte[]> HandleAsync(byte[] requestData)
        {
            var serializedRequest = Encoding.UTF8.GetString(requestData);
            var request = JsonConvert.DeserializeObject<FilePathDTO>(serializedRequest);

            var fileInfo = new FileInfo(request.Path);
            if (fileInfo.Exists)
            {
                var response = new FileResponseDTO
                {
                    File = await GetFileAsync(fileInfo),
                    ShortFileName = Path.GetFileName(request.Path),
                };
                var serializedResponse = JsonConvert.SerializeObject(response);
                return Encoding.UTF8.GetBytes(serializedResponse);
            }
            else
            {
                return FileDoesNotExist(request.Path);
            }
        }

        private byte[] FileDoesNotExist(string file)
        {
            var error = new ErrorDTO
            {
                Message = $"File: {file} does not exist."
            };
            var serializedError = JsonConvert.SerializeObject(error);
            var errorBytes = Encoding.UTF8.GetBytes(serializedError);

            return FormResponse(ResponseType.Error, errorBytes);
        }

        private async Task<byte[]> GetFileAsync(FileInfo fileInfo)
        {
            var fileStream = fileInfo.OpenRead();

            var fileBytes = new byte[fileStream.Length];
            await fileStream.ReadAsync(fileBytes);

            return fileBytes;
        }

        private byte[] FormResponse(ResponseType type, byte[] response)
        {
            var responseDTO = new ResponseDTO
            {
                Type = type,
                Data = response
            };

            var serializedResponse = JsonConvert.SerializeObject(responseDTO);
            return Encoding.UTF8.GetBytes(serializedResponse);
        }
    }
}
