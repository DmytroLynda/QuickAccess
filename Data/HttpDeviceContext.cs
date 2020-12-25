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
using ClientLogic;
using Server.DTOs.ResponseTypes;
using Data.Interfaces;

namespace Data
{
    public class HttpDeviceContext : IDeviceContext
    {
        private readonly ILogger<IDeviceContext> _logger;
        private readonly Device _device;
        private readonly Uri _deviceAddress;
        private readonly IOperationPreprocessorFactory _preprocessorFactory;

        public HttpDeviceContext(ILogger<IDeviceContext> logger, Uri deviceAddress, Device device, IOperationPreprocessorFactory preprocessorFactory)
        {
            _logger = logger;
            _device = device;
            _deviceAddress = deviceAddress;
            _preprocessorFactory = preprocessorFactory;
        }

        public async Task<File> DownloadFileAsync(FilePath file)
        {
            var preprocessor = _preprocessorFactory.Create<FilePath, File>();

            var request = preprocessor.Preprocess(file);
            var requestContent = new ByteArrayContent(request);

            using var httpClient = new HttpClient();
            var httpResponse = await httpClient.PostAsync(_deviceAddress, requestContent);
            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsByteArrayAsync();

            return preprocessor.Preprocess(response);
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
