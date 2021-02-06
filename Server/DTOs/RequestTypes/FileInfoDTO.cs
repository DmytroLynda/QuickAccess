using System;

namespace Server.DTOs.RequestTypes
{
    public class FileInfoDTO
    {
        public string Name { get; set; }
        public string Directory { get; set; }
        public long Size { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastChanged { get; set; }
    }
}
