using AutoMapper;
using Data.Internal.DataTypes;
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

            CreateMap<DirectoryPath, Server.DTOs.RequestTypes.DirectoryPathDTO>().ForMember(value => value.Path, options => options.MapFrom(src => src.Value));

            CreateMap<PathDTO, Path>().ConvertUsing(pathDTO => MapToPath(pathDTO));

            CreateMap<FileRequest, FileRequestDTO>();

            CreateMap<FileChunkDTO, FileChunk>();

            CreateMap<UserSettings, UserSettingsDTO>();
            CreateMap<UserSettingsDTO, UserSettings>();

            CreateMap<Device, DeviceDTO>();
            CreateMap<DeviceDTO, Device>();

            CreateMap<DirectoryPath, DataTypes.DirectoryPathDTO>().ForMember(value => value.Path, options => options.MapFrom(src => src.Value));
            CreateMap<DataTypes.DirectoryPathDTO, DirectoryPath>().ForMember(value => value.Value, options => options.MapFrom(src => src.Path));
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
