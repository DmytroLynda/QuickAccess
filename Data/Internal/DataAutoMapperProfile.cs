using AutoMapper;
using DomainEntities;
using Server.DTOs.RequestTypes;
using Server.DTOs.ResponseTypes;

namespace Data.Internal
{
    internal class DataAutoMapperProfile: Profile
    {
        public DataAutoMapperProfile()
        {
            CreateMap<File, FileDTO>();
            CreateMap<FileDTO, File>();

            CreateMap<FilePath, FilePathDTO>().ForMember(value => value.Path, options => 
            {
                options.MapFrom(src => src.Value);
            });
        }
    }
}
