using Server.DTOs.RequestTypes;
using Server.Enums;
using Server.Internal.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Internal.RequestHandlers
{
    internal class GetFileInfoRequestHandler : RequestHandler
    {
        public override async Task<byte[]> HandleAsync(byte[] data)
        {
            return await Task.Run(() => Handle(data));
        }

        private byte[] Handle(byte[] data)
        {
            var request = GetRequest<FilePathDTO>(data);

            FileInfoDTO fileInfo = GetFileInfo(request);

            return FormResponse(fileInfo, ResponseType.FileInfo);
        }

        private FileInfoDTO GetFileInfo(FilePathDTO request)
        {
            var fileInfo = new FileInfo(request.Path);
            if (fileInfo.Exists)
            {
                return new FileInfoDTO
                {
                    Name = fileInfo.Name,
                    Directory = fileInfo.DirectoryName,
                    Size = fileInfo.Length,
                    Created = fileInfo.CreationTime,
                    LastChanged = fileInfo.LastWriteTime
                };
            }
            else
            {
                throw new FileDoesNotExistException("Required file doest not exist.");
            }
        }
    }
}
