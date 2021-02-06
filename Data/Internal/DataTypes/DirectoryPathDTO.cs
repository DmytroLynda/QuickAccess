using System;
using System.Collections.Generic;

namespace Data.Internal.DataTypes
{
    internal class DirectoryPathDTO: IEquatable<DirectoryPathDTO>
    {
        public string Path { get; set; }

        public DirectoryPathDTO()
        { }

        public DirectoryPathDTO(string path)
        {
            Path = path;
        }

        public static DirectoryPathDTO Root { get => new DirectoryPathDTO { Path = string.Empty }; }

        public override bool Equals(object obj)
        {
            return Equals(obj as DirectoryPathDTO);
        }

        public bool Equals(DirectoryPathDTO other)
        {
            return other != null &&
                   Path == other.Path;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Path);
        }

        public static bool operator ==(DirectoryPathDTO left, DirectoryPathDTO right)
        {
            return EqualityComparer<DirectoryPathDTO>.Default.Equals(left, right);
        }

        public static bool operator !=(DirectoryPathDTO left, DirectoryPathDTO right)
        {
            return !(left == right);
        }
    }
}
