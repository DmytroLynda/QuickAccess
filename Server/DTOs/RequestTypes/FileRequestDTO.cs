using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DTOs.RequestTypes
{
    public class FileRequestDTO
    {
        public FilePathDTO Path { get; set; }
        public int Chunk { get; set; }
    }
}
