using ClientLogic.ExternalInterfaces;
using Data.Internal.Exceptions;
using Data.Internal.Interfaces;
using DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Internal.Contexts
{
    internal class TrackerContextMock : ITrackerService, ITrackerContext
    {
        private readonly Dictionary<User, List<Device>> _userDevices = new Dictionary<User, List<Device>>();
        private readonly Dictionary<Device, Uri> _uris = new Dictionary<Device, Uri>();
        private readonly List<User> _registeredUsers = new List<User>();

        public TrackerContextMock()
        {
            var defaultUser = new User
            {
                Login = "Dima",
                Password = "12345"
            };

            _registeredUsers.Add(defaultUser);

            var defaulUserDevice = new Device { Id = Guid.NewGuid(), Name = "Dima" };
            var defaultUserUri = new UriBuilder("http", "127.0.0.1", 65432).Uri;

            _userDevices.Add(defaultUser, new List<Device> { defaulUserDevice });
            _uris.Add(defaulUserDevice, defaultUserUri);
        }

        public async Task<Uri> GetDeviceUriAsync(Device device)
        {
            return await Task.FromResult(_uris[device]);
        }

        public async Task<List<Device>> GetDevicesAsync(User user, Device device)
        {
            if (LogIn(user, device))
            {
                var userDevices = _userDevices[user];
                return await Task.FromResult(userDevices);
            }

            throw new AuthenticationException("The user does not exist.");
        }

        public bool LogIn(User user, Device device)
        {
            if (_registeredUsers.Contains(user))
            {
                if (_userDevices[user].Contains(device))
                {
                    _userDevices[user].First(existingDevice => existingDevice.Equals(device)).Name = device.Name;
                }
                else
                {
                    _userDevices[user].Add(device);

                    var uri = new UriBuilder("http", "127.0.0.1", 65432).Uri;
                    _uris.Add(device, uri);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Register(User user, Device firstDevice)
        {
            if (LogIn(user, firstDevice))
            {
                return false;
            }
            else
            {
                _registeredUsers.Add(user);
                _userDevices.Add(user, new List<Device> { firstDevice });

                var uri = new UriBuilder("http", "127.0.0.1", 65432).Uri;
                _uris.Add(firstDevice, uri);

                return true;
            }
        }
    }
}
