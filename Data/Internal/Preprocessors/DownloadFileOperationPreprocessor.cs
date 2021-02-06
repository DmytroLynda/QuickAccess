using AutoMapper;
using Data.Internal.DataTypes;
using DomainEntities;
using Server.DTOs.RequestTypes;
using Server.DTOs.ResponseTypes;
using Server.Enums;

namespace Data.Internal.Preprocessors
{
    internal class DownloadFileOperationPreprocessor : JsonOperationPreprocessor<FileRequest, FileChunk>
    {
        public DownloadFileOperationPreprocessor(IMapper mapper)
            :base(mapper)
        { }

        public override byte[] Preprocess(FileRequest request)
        {
            return Preprocess<FileRequestDTO>(request, Query.DownloadFile);
        }

        public override FileChunk Preprocess(byte[] responseBytes)
        {
           return Preprocess<FileChunk>(responseBytes, ResponseType.File);
        }
    }
}
