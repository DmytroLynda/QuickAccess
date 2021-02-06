using AutoMapper;
using ClientLogic.ExternalInterfaces;
using Data.Internal.DataTypes;
using Data.Internal.Options;
using DomainEntities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServerInterface.ExternalInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Data.Internal.Contexts
{
    internal class UserSettingsContext : IUserSettingsContext, IUserSettingsProvider
    {
        private const string SettingsFileName = "usersettings.json";
        private UserSettingsDTO _userSettings;

        private readonly ILogger<UserSettingsContext> _logger;
        private readonly IMapper _mapper;

        public UserSettingsContext(ILogger<UserSettingsContext> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        #region Public methods
        public async Task<UserSettings> GetUserSettingsAsync()
        {
            if (_userSettings is null)
            {
                _userSettings = await ReadSettingsAsync();
            }

            return _mapper.Map<UserSettings>(_userSettings);
        }

        public async Task SetUserSettingsAsync(UserSettings userSettings)
        {
            if (userSettings is null)
            {
                throw new ArgumentNullException(nameof(userSettings));
            }
            _userSettings = _mapper.Map<UserSettingsDTO>(userSettings);

            await WriteSettingsAsync(_mapper.Map<UserSettingsDTO>(_userSettings));
        }

        #endregion

        #region Private methods
        private async Task<UserSettingsDTO> ReadSettingsAsync()
        {
            var settingsFileInfo = new System.IO.FileInfo(SettingsFileName);
            if (!settingsFileInfo.Exists)
            {
                settingsFileInfo.Create().Close();
            }

            using var settingsStream = settingsFileInfo.Open(FileMode.Open);
            var settings = await ReadSettingsAsync(settingsStream);

            if (settings is null)
            {
                var newSettings = MakeNewSettings();
                await WriteSettingsAsync(settingsStream, newSettings);

                settings = newSettings;
            }

            return settings;
        }

        private async Task WriteSettingsAsync(UserSettingsDTO userSettings)
        {
            var settingsInfo = new System.IO.FileInfo(SettingsFileName);
            using var settingsStream = settingsInfo.OpenWrite();

            await WriteSettingsAsync(settingsStream, userSettings);
        }

        private UserSettingsDTO MakeNewSettings()
        {
            return new UserSettingsDTO
            {
                CurrentDevice = MakeNewDevice(),
                BlockedDirectories = new List<DirectoryPathDTO>()
            };
        }

        private DeviceDTO MakeNewDevice()
        {
            var newId = Guid.NewGuid();
            var newIdString = newId.ToString().Substring(newId.ToString().Length - 5);

            return new DeviceDTO
            {
                Id = newId,
                Name = "Device:" + newIdString
            };
        }
        #endregion

        #region Static methods
        private static async Task<UserSettingsDTO> ReadSettingsAsync(FileStream settingsStream)
        {
            try
            {
                var settingsBytes = new byte[settingsStream.Length];
                await settingsStream.ReadAsync(settingsBytes, offset: 0, (int)settingsStream.Length);
                var serializedSettings = Encoding.UTF8.GetString(settingsBytes);
                var settings = JsonConvert.DeserializeObject<UserSettingsOption>(serializedSettings);
                settingsStream.Position = 0;

                return settings?.UserSettings;
            }
            catch
            {
                return null;
            }
        }

        private static async Task WriteSettingsAsync(FileStream settingsStream, UserSettingsDTO newSettings)
        {
            //Clear the setting file for rewrite settings.
            settingsStream.SetLength(0);

            var userSettings = new UserSettingsOption(newSettings);
            var serializedSettings = JsonConvert.SerializeObject(userSettings);
            var settingsBytes = Encoding.UTF8.GetBytes(serializedSettings);
            await settingsStream.WriteAsync(settingsBytes);
            await settingsStream.FlushAsync();
            settingsStream.Position = 0;
        }

        #endregion
    }
}
