using AutoMapper;
using DomainEntities;
using Server.DTOs.RequestTypes;
using Server.DTOs.ResponseTypes;
using Server.Enums;
using System.Collections.Generic;

namespace Data.Internal.Preprocessors
{
    internal class OpenFolderOperationPreprocessor : OperationPreprocessor<DirectoryPath, List<Path>>
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
