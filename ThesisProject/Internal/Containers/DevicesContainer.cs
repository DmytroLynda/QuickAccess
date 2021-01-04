using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ThesisProject.Internal.Interfaces;
using ThesisProject.Internal.ViewModels;

namespace ThesisProject.Internal.Containers
{
    internal class DevicesContainer : BaseContainer<DeviceViewModel>, IDevicesContainer
    {
        public DeviceViewModel LastSelectedDevice { get; set; }

        public DevicesContainer()
        {
            LeftClick += OnLeftClick;
        }

        private void OnLeftClick(object sender, DeviceViewModel deviceViewModel)
        {
            SelectDevice(this, deviceViewModel);
        }

        public event EventHandler<DeviceViewModel> SelectDevice;

        public DeviceViewModel GetSelectedDevice()
        {
            if (!IsSelectedDevice())
            {
                throw new InvalidOperationException("No selected device!");
            }

            return LastSelectedDevice;
        }

        public bool IsSelectedDevice()
        {
            return LastSelectedDevice is not null;
        }
    }
}
