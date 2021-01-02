using ClientLogic.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ThesisProject.Internal.Interfaces
{
    internal interface IDevicesContainer
    {
        void Initialize(UIElementCollection containerElements);
        void Show(IEnumerable<DeviceDTO> devices);
        bool IsSelectedDevice();
        DeviceDTO GetSelectedDevice();
    }
}
