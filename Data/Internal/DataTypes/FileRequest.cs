using DomainEntities;

namespace Data.Internal.DataTypes
{
    internal class FileRequest
    {
        public FilePath Path { get; set; }
        public int Chunk { get; set; }
    }
}
