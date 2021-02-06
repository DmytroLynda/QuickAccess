using AutoMapper;
using DomainEntities;
using Server.DTOs.RequestTypes;

namespace Data.Internal
{
    internal class DataAutoMapperProfile: Profile
    {
        public DataAutoMapperProfile()
        {
            CreateMap<DirectoryPathDTO, DirectoryPath>().ForMember(dst => dst.Value, options => options.MapFrom(src => src.Path));
        }
    }
}
