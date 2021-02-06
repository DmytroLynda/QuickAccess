namespace ServerInterface.DTOs.ResponseTypes
{
    public class FileChunkDTO
    {
        public FileDTO File { get; set; }
        public int AmountOfChunks { get; set; }
    }
}
