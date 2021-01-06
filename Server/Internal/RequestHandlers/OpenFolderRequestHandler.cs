using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Server.DTOs;
using Server.DTOs.RequestTypes;
using Server.DTOs.ResponseTypes;
using Server.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Internal.RequestHandlers
{
    internal class OpenFolderRequestHandler : RequestHandler
    {
        private readonly ILogger<OpenFolderRequestHandler> _logger;

        public OpenFolderRequestHandler(ILogger<OpenFolderRequestHandler> logger)
        {
            _logger = logger;
        }

        public override async Task<byte[]> HandleAsync(byte[] requestData)
        {
            return await Task.Run(() => Handle(requestData));
        }

        private byte[] Handle(byte[] requestData)
        {
            var request = GetRequest<DirectoryPathDTO>(requestData);
            
            bool isDrive = string.IsNullOrEmpty(request.Path);
            if (isDrive)
            {
                return GetDrives();
            }
            else
            {
                return GetFolder(request.Path);
            }
        }

        private byte[] GetDrives()
        {
            var drives = DriveInfo.GetDrives().Where(drive => drive.IsReady);

            var responseDrives = drives.Select(drive => new PathDTO 
            { 
                Value = drive.RootDirectory.FullName 
            });

            return FormResponse(responseDrives, ResponseType.Folder);
        }

        private byte[] GetFolder(string path)
        {
            var directoryInfo = new DirectoryInfo(@path);
            if (directoryInfo.Exists)
            {
                List<PathDTO> responseFolder = GetFolder(directoryInfo);
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
                    .Where(directory => IsExist(directory) && !directory.Attributes.HasFlag(FileAttributes.Hidden))
                    .Select(directory => new PathDTO { Value = directory.FullName });

                var fileInfos = directoryInfo
                    .GetFiles()
                    .Where(file => IsExist(file) && !file.Attributes.HasFlag(FileAttributes.Hidden))
                    .Select(file => new PathDTO { Value = file.FullName });

                var folder = new List<PathDTO>();
                folder.AddRange(directoryInfos);
                folder.AddRange(fileInfos);
                return folder;
            }
            catch(Exception e)
            {
                _logger.LogWarning($"An exception was occured when try access to {directoryInfo.FullName}," +
                    $"\nException: {e.GetType()}, message: {e.Message}.");
                return new List<PathDTO>(0);
            }
        }

        private bool IsExist(FileInfo file)
        {
            try
            {
                file.OpenRead().Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool IsExist(DirectoryInfo directory)
        {
            try
            {
                directory.GetFileSystemInfos();
                return true;
            }
            catch
            {
                return false;
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
