using System;
using System.Collections.Generic;

namespace ThesisProject.Internal.ViewModels
{
    internal class DirectoryPathViewModel : IEquatable<DirectoryPathViewModel>
    {
        public string Path { get; set; }

        public DirectoryPathViewModel()
        { }

        public DirectoryPathViewModel(string path)
        {
            Path = path;
        }

        public static DirectoryPathViewModel Root { get => new DirectoryPathViewModel { Path = string.Empty }; }

        public override bool Equals(object obj)
        {
            return Equals(obj as DirectoryPathViewModel);
        }

        public bool Equals(DirectoryPathViewModel other)
        {
            return other != null &&
                   Path == other.Path;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Path);
        }

        public static bool operator ==(DirectoryPathViewModel left, DirectoryPathViewModel right)
        {
            return EqualityComparer<DirectoryPathViewModel>.Default.Equals(left, right);
        }

        public static bool operator !=(DirectoryPathViewModel left, DirectoryPathViewModel right)
        {
            return !(left == right);
        }
    }
}
