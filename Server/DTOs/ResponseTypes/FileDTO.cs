using System;
using System.Net;

namespace Server.DTOs.ResponseTypes
{
    public class FileDTO
    {
        public string ShortFileName { get; set; }
        public byte[] File { get; set; }
    }
}
