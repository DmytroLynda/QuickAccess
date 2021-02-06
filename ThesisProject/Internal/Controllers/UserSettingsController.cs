using AutoMapper;
using ClientLogic;
using DomainEntities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisProject.Internal.Interfaces;
using ThesisProject.Internal.ViewModels;

namespace ThesisProject.Internal.Controllers
{
    internal class UserSettingsController : IUserSettingsController
    {
        private readonly ILogger<UserSettingsController> _logger;
        private readonly IMapper _mapper;
        private readonly IUserSettingsService _userSettingsService;
        public UserSettingsController(ILogger<UserSettingsController> logger, IMapper mapper, IUserSettingsService userSettingsService)
        {
            _logger = logger;
            _mapper = mapper;
            _userSettingsService = userSettingsService;
        }

        public async Task<UserSettingsViewModel> GetUserSettingsAsync()
        {
            var userSettings = await _userSettingsService.GetUserSettingsAsync();

            return _mapper.Map<UserSettingsViewModel>(userSettings);
        }

        public async Task SetUserSettingsAsync(UserSettingsViewModel userSettingsViewModel)
        {
            if (userSettingsViewModel is null)
            {
                throw new ArgumentNullException(nameof(userSettingsViewModel));
            }

            var userSettings = _mapper.Map<UserSettings>(userSettingsViewModel);
            await _userSettingsService.SetUserSettingsAsync(userSettings);
        }
    }
}
