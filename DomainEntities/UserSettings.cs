using System.Collections.Generic;

namespace DomainEntities
{
    public class UserSettings
    {
        public Device CurrentDevice { get; set; }
        public List<DirectoryPath> BlockedDirectories { get; set; }
    }
}
