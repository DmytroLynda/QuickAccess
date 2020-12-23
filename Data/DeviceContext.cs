using ClientLogic.ExternalInterfaces;
using DomainEntities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServerLogic;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Data
{
    public class DeviceContext : IDeviceContext
    {
        private readonly ILogger<IDeviceContext> _logger;
        private readonly HttpClient _httpClient;
        private readonly Uri _deviceAddress;

        public DeviceContext(ILogger<IDeviceContext> logger, HttpClient httpClient, Uri deviceAddress)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _deviceAddress = deviceAddress ?? throw new ArgumentNullException(nameof(deviceAddress));
        }

        public Task<File> DownloadFileAsync(FilePath file)
        {
            throw new NotImplementedException();
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
