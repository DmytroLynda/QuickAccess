using DomainEntities;
using Microsoft.Extensions.Logging;
using ServerLogic.ExternalInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Internal.Contexts
{
    internal class FileContext : IFileContext
    {
        private const int BytesInMegabyte = 1048576;

        private readonly ILogger<FileContext> _logger;

        public FileContext(ILogger<FileContext> logger)
        {
            _logger = logger;
        }

        public async Task<FileChunk> DownloadFileAsync(FileRequest part, int chunkSizeInMegabytes)
        {
            var fileInfo = new System.IO.FileInfo(part.Path.Value);

            var file = new File
            {
                Data = await GetFileChunkAsync(fileInfo, part.Chunk, chunkSizeInMegabytes),
                ShortFileName = System.IO.Path.GetFileName(part.Path.Value)
            };

            return new FileChunk
            {
                File = file,
                AmountOfChunks = GetAmountOfChunks(fileInfo, chunkSizeInMegabytes)
            };
        }

        public async Task<FileInfo> GetFileInfoAsync(FilePath path)
        {
            return await Task.Run(() => GetFileInfo(path));
        }

        public async Task<List<Path>> OpenDirectoryAsync(DirectoryPath path)
        {
            bool isDrive = string.IsNullOrEmpty(path.Value);
            if (isDrive)
            {
                return await GetDrivesAsync();
            }
            else
            {
                return await GetFolderAsync(path.Value);
            }
        }

        private async Task<List<Path>> GetDrivesAsync()
        {
            return await Task.Run(() =>
            {
                return GetDrives();
            });
        }

        private async Task<List<Path>> GetFolderAsync(string path)
        {
            return await Task.Run(() =>
            {
                return GetFolder(path);
            });
        }

        private List<Path> GetDrives()
        {
            var drives = System.IO.DriveInfo.GetDrives().Where(drive => drive.IsReady);

            return drives.Select(drive => new DirectoryPath(drive.RootDirectory.FullName)).Cast<Path>().ToList();
        }

        private List<Path> GetFolder(string path)
        {
            var directoryInfo = new System.IO.DirectoryInfo(@path);

            try
            {
                return SelectInternalPathces(directoryInfo);
            }
            catch (Exception e)
            {
                _logger.LogWarning($"An exception was occured when try access to {directoryInfo.FullName}," +
                    $"\nException: {e.GetType()}, message: {e.Message}.");
                return new List<Path>(0);
            }
        }

        private static List<Path> SelectInternalPathces(System.IO.DirectoryInfo directoryInfo)
        {
            var directoryInfos = directoryInfo
                .GetDirectories()
                .Where(directory => directory.Exists && !directory.Attributes.HasFlag(System.IO.FileAttributes.Hidden))
                .Select(directory => new DirectoryPath(directory.FullName));

            var fileInfos = directoryInfo
                .GetFiles()
                .Where(file => file.Exists && !file.Attributes.HasFlag(System.IO.FileAttributes.Hidden))
                .Select(file => new FilePath(file.FullName));

            var folder = new List<Path>();
            folder.AddRange(directoryInfos);
            folder.AddRange(fileInfos);
            return folder;
        }

        private FileInfo GetFileInfo(FilePath path)
        {
            var fileInfo = new System.IO.FileInfo(path.Value);
            return new FileInfo
            {
                Name = fileInfo.Name,
                Directory = fileInfo.DirectoryName,
                Size = fileInfo.Length,
                Created = fileInfo.CreationTime,
                LastChanged = fileInfo.LastWriteTime
            };
        }

        private int GetAmountOfChunks(System.IO.FileInfo fileInfo, int chunkSizeInMegabytes)
        {
            var fileLength = fileInfo.Length;

            double chunks = (double)fileLength / (chunkSizeInMegabytes * BytesInMegabyte);

            return (int)Math.Ceiling(chunks);
        }

        private async Task<byte[]> GetFileChunkAsync(System.IO.FileInfo fileInfo, int chunk, int chunkSizeInMegabytes)
        {
            using var fileStream = fileInfo.OpenRead();

            var offset = (chunk - 1) * chunkSizeInMegabytes * BytesInMegabyte;

            var available = fileStream.Length - offset;

            var chunkInBytes = chunkSizeInMegabytes * BytesInMegabyte;
            var count = Math.Min(available, chunkInBytes);

            var fileBytes = new byte[count];
            fileStream.Position = offset;
            await fileStream.ReadAsync(fileBytes, offset: 0, (int)count);

            return fileBytes;
        }
    }
}
