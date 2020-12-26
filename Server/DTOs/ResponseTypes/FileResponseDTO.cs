namespace Server.DTOs.ResponseTypes
{
    public class FileResponseDTO
    {
        public string ShortFileName { get; set; }
        public byte[] File { get; set; }
    }
}
