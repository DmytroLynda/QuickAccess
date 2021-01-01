﻿using AutoMapper;
using DomainEntities;
using Server.DTOs.RequestTypes;
using Server.DTOs.ResponseTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                options.DoNotAllowNull();
            });
        }
    }
}