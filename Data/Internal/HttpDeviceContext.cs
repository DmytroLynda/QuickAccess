using ClientLogic.ExternalInterfaces;
using DomainEntities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Data.Internal.Interfaces;

namespace Data.Internal
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

        public async Task<List<Path>> OpenFolderAsync(DirectoryPath folder)
        {
            #warning Mock implementation
            return await Task.FromResult(new List<Path>
            {
                new DirectoryPath(@"C:\Users\Dmytro\Desktop\Dyplom\"),
                new DirectoryPath(@"C:\Users\Dmytro\Desktop\Dyplom\Blabla"),
                new DirectoryPath(@"C:\Users\Dmytro\Desktop\Dyplom\1234"),
                new DirectoryPath(@"C:\Users\Dmytro\Desktop\Dyplom\sdsa"),
                new DirectoryPath(@"C:\Users\Dmytro\Desktop\Dyplom\Blablawe"),
                new DirectoryPath(@"C:\Users\Dmytro\Desktop\Dyplom\1234zxc"),
                new DirectoryPath(@"C:\Users\Dmytro\Desktop\Dyplom\asd"),
                new DirectoryPath(@"C:\Users\Dmytro\Desktop\Dyplom\Blablabnv"),
                new DirectoryPath(@"C:\Users\Dmytro\Desktop\Dyplom\1234jhgk"),
            });
        }
    }
}
