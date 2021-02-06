using System.Collections.Generic;

namespace ThesisProject.Internal.ViewModels
{
    internal class UserSettingsViewModel
    {
        public DeviceViewModel CurrentDevice { get; set; }
        public List<DirectoryPathViewModel> BlockedDirectories { get; set; }
    }
}
