using ClientLogic.ExternalInterfaces;
using Data.Internal;
using Data.Internal.Options;
using DomainEntities;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Data.Internal.Contexts
{
    internal class LocalDeviceContext : ILocalDeviceContext
    {
        private readonly LocalDeviceOptions _options;

        public LocalDeviceContext(IOptions<LocalDeviceOptions> options)
        {
            _options = options.Value;
        }

        public async Task SaveFileAsync(byte[] file, string name)
        {
            var fileInfo = new System.IO.FileInfo(_options.SaveDirectory + name);
            using var fileStream = fileInfo.Create();
            await fileStream.WriteAsync(file);
        }
    }
}
