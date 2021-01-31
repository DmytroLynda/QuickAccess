using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DTOs.ResponseTypes
{
    public class FileChunkDTO
    {
        public FileDTO File { get; set; }
        public int AmountOfChunks { get; set; }
    }
}
