using AutoMapper;
using DomainEntities;
using ServerInterface.DTOs.RequestTypes;
using ServerInterface.DTOs.ResponseTypes;
using ServerInterface.Enums;
using System.Collections.Generic;

namespace Data.Internal.Preprocessors
{
    internal class OpenFolderOperationPreprocessor : JsonOperationPreprocessor<DirectoryPath, List<Path>>
    {
        public OpenFolderOperationPreprocessor(IMapper mapper)
            :base(mapper)
        { }

        public override byte[] Preprocess(DirectoryPath request)
        {
            return Preprocess<DirectoryPathDTO>(request, Query.OpenFolder);
        }

        public override List<Path> Preprocess(byte[] responseBytes)
        {
            return Preprocess<List<PathDTO>>(responseBytes, ResponseType.Folder);
        }
    }
}
