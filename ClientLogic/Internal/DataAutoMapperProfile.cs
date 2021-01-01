using AutoMapper;
using ClientLogic.DataTypes;
using DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLogic.Internal
{
    internal class DataAutoMapperProfile: Profile
    {
        public DataAutoMapperProfile()
        {
            CreateMap<Device, DeviceDTO>();
        }
    }
}
