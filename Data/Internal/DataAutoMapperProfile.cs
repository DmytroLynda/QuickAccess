using AutoMapper;
using DomainEntities;
using Server.DTOs.RequestTypes;
using Server.DTOs.ResponseTypes;
using System;
using System.Linq;

namespace Data.Internal
{
    internal class DataAutoMapperProfile: Profile
    {
        public DataAutoMapperProfile()
        {
            CreateMap<FilePath, FilePathDTO>().ForMember(value => value.Path, options => options.MapFrom(src => src.Value));

            CreateMap<FileInfoDTO, FileInfo>();

            CreateMap<DirectoryPath, DirectoryPathDTO>().ForMember(value => value.Path, options => options.MapFrom(src => src.Value));

            CreateMap<PathDTO, Path>().ConvertUsing(pathDTO => MapToPath(pathDTO));
        }

        private Path MapToPath(PathDTO pathDTO)
        {
            if (System.IO.Path.HasExtension(pathDTO.Value))
            {
                return new FilePath(pathDTO.Value);
            }
            else
            {
                return new DirectoryPath(pathDTO.Value);
            }
        }
    }
}
