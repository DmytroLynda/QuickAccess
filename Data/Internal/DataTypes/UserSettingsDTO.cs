using System.Collections.Generic;

namespace Data.Internal.DataTypes
{
    internal class UserSettingsDTO
    {
        public DeviceDTO CurrentDevice { get; set; }
        public List<DirectoryPathDTO> BlockedDirectories { get; set; }
    }
}
