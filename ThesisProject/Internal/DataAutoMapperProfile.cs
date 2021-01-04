using AutoMapper;
using DomainEntities;
using ThesisProject.Internal.ViewModels;

namespace ThesisProject.Internal
{
    internal class DataAutoMapperProfile : Profile
    {
        public DataAutoMapperProfile()
        {
            CreateMap<Device, DeviceViewModel>();
            CreateMap<DeviceViewModel, Device>().ForMember(member => member.Id, options => options.AllowNull());

            CreateMap<DirectoryPath, DirectoryPathViewModel>().ForMember(member => member.Path, options => options.MapFrom(src => src.Value));
            CreateMap<DirectoryPathViewModel, DirectoryPath>().ForMember(memver => memver.Value, options => options.MapFrom(src => src.Path));

            CreateMap<Path, PathViewModel>();

            CreateMap<FilePathViewModel, FilePath>().ForMember(member => member.Value, options => options.MapFrom(src => src.Path));
        }
    }
}
