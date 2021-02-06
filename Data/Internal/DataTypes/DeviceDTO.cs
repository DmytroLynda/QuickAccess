using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Internal.DataTypes
{
    internal class DeviceDTO: IComparable<DeviceDTO>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        #region Overrides
        public int CompareTo(DeviceDTO other)
        {
            return Id.CompareTo(other.Id);
        }

        public override bool Equals(object obj)
        {
            return obj is DeviceDTO device &&
                   Id == device.Id &&
                   Name == device.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }

        public override string ToString()
        {
            return Name;
        }

        public static bool operator ==(DeviceDTO left, DeviceDTO right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DeviceDTO left, DeviceDTO right)
        {
            return !(left == right);
        }
        #endregion
    }
}
