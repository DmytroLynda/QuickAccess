using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Internal.DataTypes
{
    internal class FileChunk
    {
        public File File { get; set; }
        public int AmountOfChunks { get; set; }
    }
}
