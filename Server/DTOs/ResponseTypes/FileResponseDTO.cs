using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DTOs.ResponseTypes
{
    public class FileResponseDTO
    {
        public string ShortFileName { get; set; }
        public byte[] File { get; set; }
    }
}
