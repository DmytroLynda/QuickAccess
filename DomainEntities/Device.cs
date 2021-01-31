using System;

namespace DomainEntities
{
    public class Device : IEquatable<Device>
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Device);
        }

        public bool Equals(Device other)
        {
            return other != null &&
                   Id == other.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public override string ToString()
        {
            return Id + ":" + Name;
        }
    }
}
