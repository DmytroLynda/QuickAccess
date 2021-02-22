using DomainEntities;
using ServerLogic.ExternalInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic.Internal
{
    internal class FileService : IDownloadFileService, IFileInfoService, IOpenDirectoryService
    {
        private readonly IFileContext _fileContext;
        private readonly IUserSettingsProvider _userSettingsProvider;

        public FileService(IFileContext fileContext, IUserSettingsProvider userSettingsProvider)
        {
            _fileContext = fileContext;
            _userSettingsProvider = userSettingsProvider;
        }

        public async Task<FileChunk> DownloadFileAsync(FileRequest part, int chunkSizeInMegabytes)
        {
            return await _fileContext.DownloadFileAsync(part, chunkSizeInMegabytes);
        }

        public async Task<FileInfo> GetFileInfoAsync(FilePath path)
        {
            return await _fileContext.GetFileInfoAsync(path);
        }

        public async Task<List<Path>> OpenDirectoryAsync(DirectoryPath path)
        {
            var pathes = await _fileContext.OpenDirectoryAsync(path);

            var userSettings = await _userSettingsProvider.GetUserSettingsAsync();

            return pathes.Where(path => userSettings.BlockedDirectories.All(blockedDirectory => blockedDirectory.Value != path.Value)).ToList();
        }
    }
}
