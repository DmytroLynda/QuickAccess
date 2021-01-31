using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using ThesisProject.Internal.Interfaces;
using ThesisProject.Internal.Options;
using ThesisProject.Internal.ViewModels;

namespace ThesisProject.Internal.Providers
{
    internal class CurrentDeviceProvider: ICurrentDeviceProvider
    {
        private readonly DeviceViewModel _currentDevice;

        public CurrentDeviceProvider(IOptions<CurrentDeviceOptions> options)
        {
            _currentDevice = GetCurrentDevice(options.Value);
        }

        public DeviceViewModel CurrentDevice 
        { 
            get 
            { 
                 return _currentDevice; 
            }
        }

        private DeviceViewModel GetCurrentDevice(CurrentDeviceOptions options)
        {
            var deviceConfigInfo = new FileInfo(options.DeviceConfigurationName);
            if (!deviceConfigInfo.Exists)
            {
                deviceConfigInfo.Create().Close();
            }

            using var configStream = deviceConfigInfo.Open(FileMode.Open);
            var configBytes = new byte[configStream.Length];
            configStream.Read(configBytes, offset: 0, (int)configStream.Length);

            var currentDevice = JsonConvert.DeserializeObject<DeviceViewModel>(Encoding.UTF8.GetString(configBytes));
            if (currentDevice is not null)
            {
                return currentDevice;
            }
            else
            {
                var newCurrentDevice = MakeNewDevice();

                var serializedDevice = JsonConvert.SerializeObject(newCurrentDevice);
                configBytes = Encoding.UTF8.GetBytes(serializedDevice);
                configStream.Write(configBytes);
                configStream.Flush();

                return newCurrentDevice;
            }
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
    }
}
