﻿using System;

namespace ThesisProject.Internal.ViewModels
{
    internal class DeviceViewModel: IComparable<DeviceViewModel>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        #region Overrides
        public int CompareTo(DeviceViewModel other)
        {
            return Id.CompareTo(other.Id);
        }

        public override bool Equals(object obj)
        {
            return obj is DeviceViewModel device &&
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

        public static bool operator ==(DeviceViewModel left, DeviceViewModel right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DeviceViewModel left, DeviceViewModel right)
        {
            return !(left == right);
        }
        #endregion
    }
}
