using System;
using System.Collections.Generic;
using System.Windows.Controls;
using ThesisProject.Internal.ViewModels;

namespace ThesisProject.Internal.Interfaces
{
    internal interface IDevicesContainer
    {
        void Initialize(UIElementCollection containerElements);

        void Show(IEnumerable<DeviceViewModel> devices);

        bool IsSelectedDevice();

        DeviceViewModel GetSelectedDevice();

        event EventHandler<DeviceViewModel> DeviceWasSelected;
    }
}
