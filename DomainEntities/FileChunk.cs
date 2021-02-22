using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities
{
    public class FileChunk
    {
        public File File { get; set; }
        public int AmountOfChunks { get; set; }
    }
}
