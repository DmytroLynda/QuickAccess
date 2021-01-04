using ClientLogic.ExternalInterfaces;
using DomainEntities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Data.Internal.Interfaces;

namespace Data.Internal.Contexts
{
    internal class HttpDeviceContext : IDeviceContext
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
            return await HttpPostAsync<FilePath, File>(file);
        }

        public async Task<FileInfo> GetFileInfoAsync(FilePath file)
        {
            return await HttpPostAsync<FilePath, FileInfo>(file);
        }

        public async Task<List<Path>> OpenFolderAsync(DirectoryPath folder)
        {
            return await HttpPostAsync<DirectoryPath, List<Path>>(folder);
        }

        private async Task<TResponse> HttpPostAsync<TRequest, TResponse>(TRequest request)
        {
            var preprocessor = _preprocessorFactory.Create<TRequest, TResponse>();

            var preprocessedRequest = preprocessor.Preprocess(request);
            var requestContent = new ByteArrayContent(preprocessedRequest);

            using var httpClient = new HttpClient();
            var httpResponse = await httpClient.PostAsync(_deviceAddress, requestContent);
            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsByteArrayAsync();

            return preprocessor.Preprocess(response);
        }
    }
}
