﻿using System;

namespace ThesisProject.Internal.ViewModels
{
    internal class PathViewModel: IComparable<PathViewModel>
    {
        public string Value { get; set; }

        public PathViewModel()
        { }

        public PathViewModel(string path)
        {
            Value = path;
        }

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
