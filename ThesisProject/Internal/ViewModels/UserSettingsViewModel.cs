using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisProject.Internal.ViewModels
{
    internal class UserSettingsViewModel
    {
        public DeviceViewModel CurrentDevice { get; set; }
        public DirectoryPathViewModel[] BlockedDirectories { get; set; }
    }
}
