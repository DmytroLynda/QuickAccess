using System;
using System.IO;

namespace ThesisProject.Internal.ViewModels
{
    internal class PathViewModel: IComparable<PathViewModel>
    {
        public string Value { get; set; }

        public int CompareTo(PathViewModel other)
        {
            return Value.CompareTo(other.Value);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
