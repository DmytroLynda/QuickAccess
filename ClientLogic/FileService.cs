﻿using ClientLogic.ExternalInterfaces;
using DomainEntities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using File = DomainEntities.File;
using DirectoryPath = DomainEntities.DirectoryPath;
using FileInfo = DomainEntities.FileInfo;
using Path = DomainEntities.Path;

namespace ClientLogic
{
    public class FileService : IFileService
    {
        private readonly ILogger<IFileService> _logger;
        private readonly IDeviceContextFactory _deviceFactory;

        public FileService(ILogger<IFileService> logger, IDeviceContextFactory deviceFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _deviceFactory = deviceFactory ?? throw new ArgumentNullException(nameof(deviceFactory));
        }

        public async Task DownloadFileAsync(Device device, FilePath filePath)
        {
            #region check arguments
            if (device is null)
            {
                throw new ArgumentNullException(nameof(device));
            }

            if (filePath is null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }
            #endregion

            var remoteDeviceContext = _deviceFactory.GetDeviceContext(device);
            var file = await remoteDeviceContext.DownloadFileAsync(filePath);

            var thisDeviceContext = _deviceFactory.GetLocalDevice();
            thisDeviceContext.SaveFile(file);
        }

        public async Task<FileInfo> GetFileInfoAsync(Device device, FilePath file)
        {
            #region Check arguments
            if (device is null)
            {
                throw new ArgumentNullException(nameof(device));
            }

            if (file is null)
            {
                throw new ArgumentNullException(nameof(file));
            }
            #endregion

            var deviceContext = _deviceFactory.GetDeviceContext(device);

            var fileInfo = await deviceContext.GetFileInfoAsync(file);

            return fileInfo;
        }

        public async Task<List<Path>> ShowDirectoryAsync(Device device, DirectoryPath directory)
        {
            #region Check arguments
            if (device is null)
            {
                throw new ArgumentNullException(nameof(device));
            }

            if (directory is null)
            {
                throw new ArgumentNullException(nameof(directory));
            }
            #endregion

            IDeviceContext context = _deviceFactory.GetDeviceContext(device);

            List<Path> folder;
            try
            { 
                folder = await context.OpenFolderAsync(directory);
            }
            catch(DirectoryNotFoundException e)
            {
                _logger.LogWarning("The device: {0} does not share the directory: {1}.", device, directory, e);
                folder = new List<Path>(0);
            }

            return folder;
        }
    }
}
