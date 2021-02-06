using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities
{
    public class UserSettings
    {
        public Device CurrentDevice { get; set; }
        public List<DirectoryPath> BlockedDirectories { get; set; }
    }
}
