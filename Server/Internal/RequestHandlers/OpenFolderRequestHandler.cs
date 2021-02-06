using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServerInterface.DTOs;
using ServerInterface.DTOs.RequestTypes;
using ServerInterface.DTOs.ResponseTypes;
using ServerInterface.Enums;
using ServerInterface.ExternalInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerInterface.Internal.RequestHandlers
{
    internal class OpenFolderRequestHandler : RequestHandler
    {
        private readonly ILogger<OpenFolderRequestHandler> _logger;
        private readonly IUserSettingsProvider _settingsProvider;

        public OpenFolderRequestHandler(ILogger<OpenFolderRequestHandler> logger, IUserSettingsProvider settingsProvider)
        {
            _logger = logger;
            _settingsProvider = settingsProvider;
        }

        public override async Task<byte[]> HandleAsync(byte[] requestData)
        {
            var request = GetRequest<DirectoryPathDTO>(requestData);

            bool isDrive = string.IsNullOrEmpty(request.Path);
            if (isDrive)
            {
                return await GetDrivesAsync();
            }
            else
            {
                return await GetFolderAsync(request.Path);
            }
        }

        private async Task<byte[]> GetDrivesAsync()
        {
            var drives = DriveInfo.GetDrives().Where(drive => drive.IsReady);

            var responseDrives = drives.Select(drive => new PathDTO
            {
                Value = drive.RootDirectory.FullName
            });

            responseDrives = await RemoveBlockedAsync(responseDrives);

            return FormResponse(responseDrives, ResponseType.Folder);
        }

        private async Task<List<PathDTO>> RemoveBlockedAsync(IEnumerable<PathDTO> pathes)
        {
            var userSettings = await _settingsProvider.GetUserSettingsAsync();

            return pathes.Where(path => userSettings.BlockedDirectories.All(blockedDirectory => blockedDirectory.Value != path.Value)).ToList();
        }

        private async Task<byte[]> GetFolderAsync(string path)
        {
            var directoryInfo = new DirectoryInfo(@path);
            if (directoryInfo.Exists)
            {
                List<PathDTO> responseFolder = GetFolder(directoryInfo);
                responseFolder = await RemoveBlockedAsync(responseFolder);

                var serializedResponse = JsonConvert.SerializeObject(responseFolder);
                return FormResponse(ResponseType.Folder, Encoding.UTF8.GetBytes(serializedResponse));
            }
            else
            {
                throw new DirectoryNotFoundException(path);
            }
        }

        private List<PathDTO> GetFolder(DirectoryInfo directoryInfo)
        {
            try
            {
                var directoryInfos = directoryInfo
                    .GetDirectories()
                    .Where(directory => directory.Exists && !directory.Attributes.HasFlag(FileAttributes.Hidden))
                    .Select(directory => new PathDTO { Value = directory.FullName });

                var fileInfos = directoryInfo
                    .GetFiles()
                    .Where(file => file.Exists && !file.Attributes.HasFlag(FileAttributes.Hidden))
                    .Select(file => new PathDTO { Value = file.FullName });

                var folder = new List<PathDTO>();
                folder.AddRange(directoryInfos);
                folder.AddRange(fileInfos);
                return folder;
            }
            catch (Exception e)
            {
                _logger.LogWarning($"An exception was occured when try access to {directoryInfo.FullName}," +
                    $"\nException: {e.GetType()}, message: {e.Message}.");
                return new List<PathDTO>(0);
            }
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
