using AutoMapper;
using DomainEntities;
using ServerInterface.DTOs.RequestTypes;
using ServerInterface.DTOs.ResponseTypes;

namespace ServerInterface.Internal
{
    internal class DataAutoMapperProfile : Profile
    {
        public DataAutoMapperProfile()
        {
            CreateMap<DirectoryPathDTO, DirectoryPath>().ForMember(dst => dst.Value, options => options.MapFrom(src => src.Path));
            CreateMap<FilePathDTO, FilePath>().ForMember(dst => dst.Value, options => options.MapFrom(src => src.Path));
            CreateMap<FileRequestDTO, FileRequest>();

            CreateMap<Path, PathDTO>();
            CreateMap<FileChunk, FileChunkDTO>();
            CreateMap<File, FileDTO>();
            CreateMap<FileInfo, FileInfoDTO>();
        }
    }
}
