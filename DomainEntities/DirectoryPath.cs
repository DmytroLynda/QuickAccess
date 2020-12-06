using System;
using System.IO;

namespace DomainEntities
{
    public class DirectoryPath
    {
        public string Value { get; init; }

        public DirectoryPath(string path)
        {
            #region Check arguments
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException($"'{nameof(path)}' cannot be null or empty", nameof(path));
            }

            if (Path.HasExtension(path))
            {
                throw new ArgumentException("Passed path has extention, then, it is no a directory path: {0}", path);
            }
            #endregion

            Value = path;
        }


        public override string ToString()
        {
            return Value;
        }
    }
}
