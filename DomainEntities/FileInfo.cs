using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities
{
    public class FileInfo
    {
        public string Name { get; set; }
        public long Weight { get; set; }
        public FilePath Path { get; set; }
    }
}
