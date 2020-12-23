using ClientLogic.ExternalInterfaces;
using DomainEntities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Server.DTOs;
using Newtonsoft.Json;
using System.Text;
using Server.Enums;
using System.Net.Http;

namespace Data
{
    public class DeviceContext : IDeviceContext
    {
        private readonly ILogger<IDeviceContext> _logger;
        private readonly Device _device;
        private readonly Uri _deviceAddress;

        public DeviceContext(ILogger<IDeviceContext> logger, Uri deviceAddress, Device device)
        {
            _logger = logger;
            _device = device;
            _deviceAddress = deviceAddress;
        }

        public async Task<byte[]> DownloadFileAsync(FilePath file)
        {
            var filePathDTO = new FilePathDTO
            {
                Path = file.Value
            };

            var serializedFilePath = JsonConvert.SerializeObject(filePathDTO);
            var filePathBytes = Encoding.UTF8.GetBytes(serializedFilePath);

            var request = new RequestDTO
            {
                Query = Query.DownloadFile,
                Request = filePathBytes
            };
            var serializedRequest = JsonConvert.SerializeObject(request);
            var requestContent = new StringContent(serializedRequest);

            using var httpClient = new HttpClient();
            var httpResponse = await httpClient.PostAsync(_deviceAddress, requestContent);
            httpResponse.EnsureSuccessStatusCode();

            var serializedResponse = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponseDTO>(serializedResponse);

            if (response.Type == ResponseType.File)
            {
                return response.Response;
            }
            else if (response.Type == ResponseType.Error)
            {
                var serializedError = Encoding.UTF8.GetString(response.Response);
                var error = JsonConvert.DeserializeObject<ErrorDTO>(serializedError);
                throw new ErrorResponseMessageException(error.Message);
            }
            else
            {
                throw new InvalidOperationException($"Unknown response type: {response.Type}");
            }
        }

        public Task<FileInfo> GetFileInfoAsync(FilePath file)
        {
            throw new NotImplementedException();
        }

        public Task<List<Path>> OpenFolderAsync(DirectoryPath folder)
        {
            throw new NotImplementedException();
        }
    }
}
