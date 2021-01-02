using AutoMapper;
using ClientLogic;
using DomainEntities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThesisProject.Internal.Interfaces;
using ThesisProject.Internal.ViewModels;

namespace ThesisProject.Internal.Controllers
{
    internal class MainWindowController : IMainWindowController
    {
        private readonly ILogger<MainWindowController> _logger;
        private readonly IMapper _mapper;

        private readonly IFileService _fileService;
        private readonly IDeviceService _deviceService;

        public MainWindowController(ILogger<MainWindowController> logger, IFileService fileService, IDeviceService deviceService, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;

            _fileService = fileService;
            _deviceService = deviceService;
        }

        public async Task<List<DeviceViewModel>> GetDevicesAsync()
        {
            var devices = await _deviceService.GetDevicesAsync();
            return _mapper.Map<List<DeviceViewModel>>(devices);
        }

        public async Task<List<PathViewModel>> GetDirectoryAsync(DirectoryPathViewModel directory, DeviceViewModel device)
        {
            var pathes = await _fileService.ShowDirectoryAsync(_mapper.Map<Device>(device), _mapper.Map<DirectoryPath>(directory));
            return _mapper.Map<List<PathViewModel>>(pathes);
        }
    }
}
