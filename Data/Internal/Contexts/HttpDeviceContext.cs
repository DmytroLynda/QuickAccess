﻿using ClientLogic.ExternalInterfaces;
using DomainEntities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Data.Internal.Interfaces;
using Data.Internal.DataTypes;

namespace Data.Internal.Contexts
{
    internal class HttpDeviceContext : IDeviceContext
    {
        private readonly ILogger<IDeviceContext> _logger;
        private readonly IOperationPreprocessorFactory _preprocessorFactory;
        private readonly ILocalDeviceContext _localDeviceContext;
        private Uri _deviceAddress;
        internal Uri DeviceAddress
        {
            get
            {
                if (_deviceAddress is null)
                {
                    throw new InvalidOperationException($"The {nameof(HttpDeviceContext)} should have defined the device address before any requests.");
                }

                return _deviceAddress;
            }
            set
            {
                if (value is null)
                {
                    throw new ArgumentException("Value can't be null.");
                }

                _deviceAddress = value;
            }
        }

        public HttpDeviceContext(
            ILogger<IDeviceContext> logger,
            IOperationPreprocessorFactory preprocessorFactory,
            ILocalDeviceContext localDeviceContext)
        {
            _logger = logger;
            _preprocessorFactory = preprocessorFactory;
            _localDeviceContext = localDeviceContext;
        }

        public async Task DownloadFileAsync(FilePath path)
        {
            var firstRequest = new FileRequest
            {
                Path = path,
                Chunk = 1
            };
            var firstChunk = await HttpPostAsync<FileRequest, FileChunk>(firstRequest);
            await _localDeviceContext.SaveNewFileChunk(firstChunk.File);

            for (int chunk = 2; chunk <= firstChunk.AmountOfChunks; chunk++)
            {
                var nextRequest = new FileRequest
                {
                    Path = path,
                    Chunk = chunk
                };

                var nextChunk = await HttpPostAsync<FileRequest, FileChunk>(nextRequest);
                await _localDeviceContext.SaveNextFileChunk(nextChunk.File);
            }
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
            var httpResponse = await httpClient.PostAsync(DeviceAddress, requestContent);
            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsByteArrayAsync();

            return preprocessor.Preprocess(response);
        }
    }
}
