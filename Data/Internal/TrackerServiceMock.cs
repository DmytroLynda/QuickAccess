﻿using Data.Internal.Interfaces;
using DomainEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    internal class TrackerServiceMock : ITrackerService
    {
        private readonly Dictionary<Device, Uri> _devices = new Dictionary<Device, Uri>();

        public TrackerServiceMock()
        {
            var dimaUri = new UriBuilder("http", "127.0.0.1", 65432);
            var juliaUri = new UriBuilder("http", "192.168.0.220", 8080);

            _devices.Add(new Device { Id = 1, Name = "Dima" }, dimaUri.Uri);
            _devices.Add(new Device { Id = 2, Name = "Julia" }, juliaUri.Uri);
        }

        public async Task<Uri> GetDeviceUriAsync(Device device)
        {
            return await Task.FromResult(_devices[device]);
        }
    }
}