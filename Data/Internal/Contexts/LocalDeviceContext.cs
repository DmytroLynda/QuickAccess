using Data.Internal;
using Data.Internal.DataTypes;
using Data.Internal.Interfaces;
using Data.Internal.Options;
using DomainEntities;
using Microsoft.Extensions.Options;
using Server.DTOs.ResponseTypes;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Data.Internal.Contexts
{
    internal class LocalDeviceContext : ILocalDeviceContext
    {
        private readonly string _saveDirectory;

        public LocalDeviceContext(IOptions<LocalDeviceOptions> options)
        {
            _saveDirectory = options.Value.SaveDirectory;
        }

        public async Task SaveNewFileChunk(File file)
        {
            var fileInfo = new System.IO.FileInfo(_saveDirectory + file.ShortFileName);
            using var fileStream = fileInfo.Create();
            await fileStream.WriteAsync(file.Data);

            await fileStream.FlushAsync();
        }

        public async Task SaveNextFileChunk(File file)
        {
            var fileInfo = new System.IO.FileInfo(_saveDirectory + file.ShortFileName);
            using var fileStream = fileInfo.OpenWrite();

            fileStream.Position = fileStream.Length;
            await fileStream.WriteAsync(file.Data);

            await fileStream.FlushAsync();
        }
    }
}
