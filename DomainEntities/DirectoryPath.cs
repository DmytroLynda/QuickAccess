using System;

namespace DomainEntities
{
    public class DirectoryPath : Path
    {
        public DirectoryPath(string path)
        {
            #region Check arguments
            if (path is null)
            {
                throw new ArgumentException($"'{nameof(path)}' cannot be null.", nameof(path));
            }

            if (System.IO.Path.HasExtension(path))
            {
                throw new ArgumentException("Passed path has extention, then, it is no a directory path: {0}.", path);
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
