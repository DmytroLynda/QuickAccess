using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisProject.Internal.Interfaces;
using ThesisProject.Internal.ViewModels;

namespace ThesisProject.Internal.Providers
{
    internal class UserSettingsProvider: IUserSettingsProvider
    {
        private const string SettingsFileName = "usersettings.json";
        private UserSettingsViewModel _userSettings;


        #region Private properties
        #endregion

        #region Public methods
        public async Task<UserSettingsViewModel> GetUserSettingsAsync()
        {
            if (_userSettings is null)
            {
                _userSettings = await ReadSettingsAsync();
            }

            return _userSettings;
        }

        public async Task SetUserSettingsAsync(UserSettingsViewModel userSettings)
        {
            if (userSettings is null)
            {
                throw new ArgumentNullException(nameof(userSettings));
            }

            await WriteSettingsAsync(userSettings);
        }
       
        #endregion

        #region Private methods
        private async Task<UserSettingsViewModel> ReadSettingsAsync()
        {
            var settingsFileInfo = new FileInfo(SettingsFileName);
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

        private async Task WriteSettingsAsync(UserSettingsViewModel userSettings)
        {
            var settingsInfo = new FileInfo(SettingsFileName);
            using var settingsStream = settingsInfo.OpenWrite();

            await WriteSettingsAsync(settingsStream, userSettings);
        }

        private UserSettingsViewModel MakeNewSettings()
        {
            return new UserSettingsViewModel
            {
                CurrentDevice = MakeNewDevice(),
                BlockedDirectories = new DirectoryPathViewModel[0]
            };
        }

        private DeviceViewModel MakeNewDevice()
        {
            var newId = Guid.NewGuid();
            var newIdString = newId.ToString().Substring(newId.ToString().Length - 5);

            return new DeviceViewModel
            {
                Id = newId,
                Name = "Device:" + newIdString
            };
        }
        #endregion

        #region Static methods
        private static async Task<UserSettingsViewModel> ReadSettingsAsync(FileStream settingsStream)
        {
            try
            { 
                var settingsBytes = new byte[settingsStream.Length];
                await settingsStream.ReadAsync(settingsBytes, offset: 0, (int)settingsStream.Length);
                var serializedSettings = Encoding.UTF8.GetString(settingsBytes);
                var settings = JsonConvert.DeserializeObject<UserSettingsViewModel>(serializedSettings);
                return settings;
            }
            catch
            {
                return null;
            }
        }

        private static async Task WriteSettingsAsync(FileStream settingsStream, UserSettingsViewModel newSettings)
        {
            //Clear the setting file for rewrite settings.
            settingsStream.SetLength(0);

            var serializedNewSettings = JsonConvert.SerializeObject(newSettings);
            var newSettingsBytes = Encoding.UTF8.GetBytes(serializedNewSettings);
            await settingsStream.WriteAsync(newSettingsBytes);
            await settingsStream.FlushAsync();
        }

        #endregion
    }
}
