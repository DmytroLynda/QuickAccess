using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities
{
    public class File
    {
        public string ShortFileName { get; set; }
        public byte[] Data { get; set; }
    }
}
