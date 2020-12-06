using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities
{
    public class FilePath
    {
        public string Value { get; }

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
