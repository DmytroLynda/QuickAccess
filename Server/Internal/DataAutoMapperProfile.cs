using AutoMapper;
using DomainEntities;
using ServerInterface.DTOs.RequestTypes;

namespace ServerInterface.Internal
{
    internal class DataAutoMapperProfile : Profile
    {
        public DataAutoMapperProfile()
        {
            CreateMap<DirectoryPathDTO, DirectoryPath>().ForMember(dst => dst.Value, options => options.MapFrom(src => src.Path));
        }
    }
}
