using System;
using System.Collections.Generic;

namespace ThesisProject.Internal.ViewModels
{
    internal class DeviceViewModel: IComparable<DeviceViewModel>
    {
        public string Name { get; set; }


        #region Overrides
        public int CompareTo(DeviceViewModel other)
        {
            return Name.CompareTo(other.Name);
        }

        public override bool Equals(object obj)
        {
            return obj is DeviceViewModel dTO &&
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

        public static bool operator ==(DeviceViewModel left, DeviceViewModel right)
        {
            return EqualityComparer<DeviceViewModel>.Default.Equals(left, right);
        }

        public static bool operator !=(DeviceViewModel left, DeviceViewModel right)
        {
            return !(left == right);
        }
        #endregion
    }
}
