using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLogic.DataTypes
{
    public class DeviceDTO
    {
        public string Name { get; set; }

        #region Overrides
        public override bool Equals(object obj)
        {
            return obj is DeviceDTO dTO &&
                   Name == dTO.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }

        public override string ToString()
        {
            return Name;
        }

        public static bool operator ==(DeviceDTO left, DeviceDTO right)
        {
            return EqualityComparer<DeviceDTO>.Default.Equals(left, right);
        }

        public static bool operator !=(DeviceDTO left, DeviceDTO right)
        {
            return !(left == right);
        }
        #endregion
    }
}
