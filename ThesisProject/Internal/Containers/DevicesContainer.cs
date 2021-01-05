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
            return SelectedViewModel;
        }

        public bool IsSelectedDevice()
        {
            return SelectedViewModel is not null;
        }
    }
}
