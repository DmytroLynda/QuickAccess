using ClientLogic.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ThesisProject.Internal.Interfaces;

namespace ThesisProject.Internal.Containers
{
    internal class DevicesContainer : IDevicesContainer
    {
        private List<(Button button, DeviceDTO device)> DevicesButtons { get; set;}

        private UIElementCollection ContainerElements { get; set; }

        private Button SelectedDevice { get; set; }

        public DevicesContainer()
        {
            DevicesButtons = new List<(Button button, DeviceDTO device)>();
        }

        public void Initialize(UIElementCollection containerElements)
        {
            ContainerElements = containerElements ?? throw new ArgumentNullException(nameof(containerElements));
        }

        public void Show(IEnumerable<DeviceDTO> devices)
        {
            DevicesButtons.RemoveAll(deviceButton => !devices.Contains(deviceButton.device));
            
            var newDevices = devices.Except(DevicesButtons.Select(deviceButton => deviceButton.device));

            DevicesButtons.AddRange(newDevices.Select(newDevice => (MakeDeviceButton(newDevice), newDevice)));
            DevicesButtons = DevicesButtons.OrderBy(deviceButton => deviceButton.device.Name).ToList();

            ContainerElements.Clear();
            DevicesButtons.ToList().ForEach(deviceButton => ContainerElements.Add(deviceButton.button));
        }

        public DeviceDTO GetSelectedDevice()
        {
            if (!IsSelectedDevice())
            {
                throw new InvalidOperationException("No selected device!");
            }

            return DevicesButtons.First(devicesButton => devicesButton.button == SelectedDevice).device;
        }

        public bool IsSelectedDevice()
        {
            return SelectedDevice is not null;
        }

        private Button MakeDeviceButton(DeviceDTO device)
        {
            var devicesButton = new Button
            {
                Content = device.Name,
                FontSize = 16,
                Margin = new Thickness(0, 0, 0, 5),
            };

            devicesButton.Click +=  DeviceSelected;

            return devicesButton;
        }

        private void DeviceSelected(object sender, RoutedEventArgs e)
        {
            //Unselect previous device and select selected.
            if (SelectedDevice is not null)
            { 
                SelectedDevice.IsEnabled = true;
            }

            SelectedDevice = sender as Button;
            SelectedDevice.IsEnabled = false;
        }

    }
}
