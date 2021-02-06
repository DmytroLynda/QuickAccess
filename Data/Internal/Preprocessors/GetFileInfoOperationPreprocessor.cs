using AutoMapper;
using DomainEntities;
using Server.DTOs.RequestTypes;
using Server.Enums;
using System;

namespace Data.Internal.Preprocessors
{
    internal class GetFileInfoOperationPreprocessor : JsonOperationPreprocessor<FilePath, FileInfo>
    {
        public GetFileInfoOperationPreprocessor(IMapper mapper)
            :base(mapper)
        { }

        public override byte[] Preprocess(FilePath request)
        {
            return Preprocess<FilePathDTO>(request, Query.GetFileInfo);
        }

        public override FileInfo Preprocess(byte[] responseBytes)
        {
            return Preprocess<FileInfoDTO>(responseBytes, ResponseType.FileInfo);
        }
    }
}
