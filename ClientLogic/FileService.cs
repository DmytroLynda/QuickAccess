using ClientLogic.ExternalInterfaces;
using DomainEntities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using File = DomainEntities.File;
using DirectoryPath = DomainEntities.DirectoryPath;

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

        public string GetFileInfo(Device device, FilePath file)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DirectoryPath>> ShowDirectoryAsync(Device device, DirectoryPath directory)
        {
            IDeviceContext context = _deviceFactory.GetDeviceContext(device);

            List<DirectoryPath> folder;
            try
            { 
                folder = await context.OpenFolderAsync(directory);
            }
            catch(DirectoryNotFoundException e)
            {
                _logger.LogWarning("The device: {0} does not share the directory: {1}.", device, directory, e);
                folder = new List<DirectoryPath>(0);
            }

            return folder;
        }
    }
}
