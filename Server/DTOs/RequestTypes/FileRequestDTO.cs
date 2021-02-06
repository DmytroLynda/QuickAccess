namespace ServerInterface.DTOs.RequestTypes
{
    public class FileRequestDTO
    {
        public FilePathDTO Path { get; set; }
        public int Chunk { get; set; }
    }
}
