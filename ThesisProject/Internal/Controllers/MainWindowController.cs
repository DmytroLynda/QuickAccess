using AutoMapper;
using ClientLogic;
using DomainEntities;
using Microsoft.Extensions.Logging;
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
        private readonly IUserSettingsController _userSettingsController;

        private readonly IFileService _fileService;
        private readonly IDeviceService _deviceService;

        public MainWindowController(ILogger<MainWindowController> logger, IFileService fileService, IDeviceService deviceService, IMapper mapper, IUserSettingsController userSettingsController)
        {
            _logger = logger;
            _mapper = mapper;
            _userSettingsController = userSettingsController;

            _fileService = fileService;
            _deviceService = deviceService;
        }

        public async Task DownloadFileAsync(FilePathViewModel filePathViewModel, DeviceViewModel deviceViewModel)
        {
            var device = _mapper.Map<Device>(deviceViewModel);
            var filePath = _mapper.Map<FilePath>(filePathViewModel);

            await _fileService.DownloadFileAsync(device, filePath);
        }

        public async Task<List<DeviceViewModel>> GetDevicesAsync(UserViewModel userViewModel, DeviceViewModel deviceViewModel)
        {
            var user = _mapper.Map<User>(userViewModel);
            var device = _mapper.Map<Device>(deviceViewModel);

            var devices = await _deviceService.GetDevicesAsync(user, device);
            return _mapper.Map<List<DeviceViewModel>>(devices);
        }

        public async Task<List<PathViewModel>> GetDirectoryAsync(DirectoryPathViewModel directoryViewModel, DeviceViewModel deviceViewModel)
        {
            var directory = _mapper.Map<DirectoryPath>(directoryViewModel);
            var device = _mapper.Map<Device>(deviceViewModel);

            var pathes = await _fileService.ShowDirectoryAsync(device, directory);
            return _mapper.Map<List<PathViewModel>>(pathes);
        }

        public async Task<FileInfoViewModel> GetFileInfoAsync(FilePathViewModel filePathViewModel, DeviceViewModel deviceViewModel)
        {
            var filePath = _mapper.Map<FilePath>(filePathViewModel);
            var device = _mapper.Map<Device>(deviceViewModel);

            var fileInfo = await _fileService.GetFileInfoAsync(device, filePath);

            return _mapper.Map<FileInfoViewModel>(fileInfo);
        }

        public async Task<UserSettingsViewModel> GetUserSettingsAsync()
        {
            return await _userSettingsController.GetUserSettingsAsync();
        }
    }
}
