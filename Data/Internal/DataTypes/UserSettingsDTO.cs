using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Internal.DataTypes
{
    internal class UserSettingsDTO
    {
        public DeviceDTO CurrentDevice { get; set; }
        public List<DirectoryPathDTO> BlockedDirectories { get; set; }
    }
}
