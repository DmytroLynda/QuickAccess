﻿namespace Server.DTOs.ResponseTypes
{
    public class FileDTO
    {
        public string ShortFileName { get; set; }
        public byte[] Body { get; set; }
    }
}