using AutoMapper;
using DomainEntities;
using Server.DTOs.RequestTypes;
using Server.DTOs.ResponseTypes;
using Server.Enums;

namespace Data.Internal.Preprocessors
{
    internal class DownloadFileOperationPreprocessor : JsonOperationPreprocessor<FilePath, FileDTO>
    {
        public DownloadFileOperationPreprocessor(IMapper mapper)
            :base(mapper)
        { }

        public override byte[] Preprocess(FilePath request)
        {
            return Preprocess<FilePathDTO>(request, Query.DownloadFile);
        }

        public override FileDTO Preprocess(byte[] responseBytes)
        {
           return Preprocess<FileDTO>(responseBytes, ResponseType.File);
        }
    }
}
