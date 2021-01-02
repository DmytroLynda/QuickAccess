using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ThesisProject.Internal.Interfaces;
using ThesisProject.Internal.ViewModels;

namespace ThesisProject.Internal.Containers
{
    internal class DevicesContainer : IDevicesContainer
    {
        private List<(Button button, DeviceViewModel device)> DevicesButtons { get; set;}

        private UIElementCollection UIElements { get; set; }

        private Button SelectedDevice { get; set; }

        public event EventHandler<DeviceViewModel> DeviceWasSelected;

        public DevicesContainer()
        {
            DevicesButtons = new List<(Button button, DeviceViewModel device)>();
        }


        public void Initialize(UIElementCollection uiElements)
        {
            UIElements = uiElements ?? throw new ArgumentNullException(nameof(uiElements));
        }

        public void Show(IEnumerable<DeviceViewModel> devices)
        {
            DevicesButtons.RemoveAll(deviceButton => !devices.Contains(deviceButton.device));
            
            var newDevices = devices.Except(DevicesButtons.Select(deviceButton => deviceButton.device));

            DevicesButtons.AddRange(newDevices.Select(newDevice => (MakeDeviceButton(newDevice), newDevice)));
            DevicesButtons = DevicesButtons.OrderBy(deviceButton => deviceButton.device.Name).ToList();

            UIElements.Clear();
            DevicesButtons.ToList().ForEach(deviceButton => UIElements.Add(deviceButton.button));
        }

        public DeviceViewModel GetSelectedDevice()
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

        private Button MakeDeviceButton(DeviceViewModel device)
        {
            var devicesButton = new Button
            {
                Content = device.Name,
                FontSize = 16,
                Margin = new Thickness(0, 0, 0, 5),
            };

            devicesButton.Click +=  OnDeviceSelect;

            return devicesButton;
        }

        private void OnDeviceSelect(object sender, RoutedEventArgs e)
        {
            //Unselect previous device and select selected.
            if (SelectedDevice is not null)
            { 
                SelectedDevice.IsEnabled = true;
            }

            SelectedDevice = sender as Button;
            SelectedDevice.IsEnabled = false;

            DeviceWasSelected(this, DevicesButtons.First(deviceButton => deviceButton.button == SelectedDevice).device);
        }
    }
}
