using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities
{
    public class FileRequest
    {
        public FilePath Path { get; set; }
        public int Chunk { get; set; }
    }
}
