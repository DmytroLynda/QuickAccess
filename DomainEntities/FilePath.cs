﻿using System;

namespace DomainEntities
{
    public class FilePath : Path
    {
        public FilePath(string path)
        {
            #region Check argument
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException($"'{nameof(path)}' cannot be null or empty", nameof(path));
            }

            if (!System.IO.Path.HasExtension(path))
            {
                throw new ArgumentException("Passed file path is not a file path: {0}", path);
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
