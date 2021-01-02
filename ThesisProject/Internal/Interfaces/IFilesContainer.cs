using System.Collections.Generic;
using System.Windows.Controls;
using ThesisProject.Internal.ViewModels;

namespace ThesisProject.Internal.Interfaces
{
    internal interface IFilesContainer
    {
        DeviceViewModel Device { get; set; }
        DirectoryPathViewModel Directory { get; set; }
        void Show(List<PathViewModel> pathes);
        void Initialize(UIElementCollection children);
    }
}
