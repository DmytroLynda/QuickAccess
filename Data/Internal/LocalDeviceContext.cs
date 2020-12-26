using ClientLogic.ExternalInterfaces;
using DomainEntities;
using System;
using System.Threading.Tasks;

namespace Data
{
    internal class LocalDeviceContext : ILocalDeviceContext
    {
        private readonly DirectoryPath _saveDirectory;

        public LocalDeviceContext(DirectoryPath saveDirectory)
        {
            if (saveDirectory is null)
            {
                throw new ArgumentNullException(nameof(saveDirectory));
            }

            _saveDirectory = saveDirectory;
        }

        public async Task SaveFileAsync(byte[] file, string name)
        {
            var fileInfo = new System.IO.FileInfo(name);
            var fileStream = fileInfo.Create();
            await fileStream.WriteAsync(file);
        }
    }
}
