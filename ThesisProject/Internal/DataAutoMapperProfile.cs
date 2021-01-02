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
        }
    }
}
