using ClientLogic.ExternalInterfaces;
using DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class LocalDeviceContext : ILocalDeviceContext
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
