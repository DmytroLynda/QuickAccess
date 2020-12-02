using ClientLogic.ExternalInterfaces;
using DomainEntities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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

        public void DownloadFile(Device device, Path file)
        {
            throw new NotImplementedException();
        }

        public string GetFileInfo(Device device, Path file)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Path>> ShowDirectoryAsync(Device device, Path directory)
        {
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
