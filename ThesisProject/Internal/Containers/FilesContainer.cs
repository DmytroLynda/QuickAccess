using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ThesisProject.Internal.Interfaces;
using ThesisProject.Internal.ViewModels;

namespace ThesisProject.Internal.Containers
{
    internal class FilesContainer : IFilesContainer
    {
        public DeviceViewModel Device { get; set; }
        public DirectoryPathViewModel Directory { get; set; }

        public void Initialize(UIElementCollection children)
        {
            throw new NotImplementedException();
        }

        public void Show(List<PathViewModel> pathes)
        {
            throw new NotImplementedException();
        }
    }
}
