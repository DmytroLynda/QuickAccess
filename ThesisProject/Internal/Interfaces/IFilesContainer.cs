using System;
using System.Collections.Generic;
using System.Windows.Controls;
using ThesisProject.Internal.ViewModels;

namespace ThesisProject.Internal.Interfaces
{
    internal interface IFilesContainer
    {
        event EventHandler<DirectoryPathViewModel> OpenDirectory;
        DeviceViewModel CurentDevice { get; set; }
        DirectoryPathViewModel CurentDirectory { get; set; }
        void Show(List<PathViewModel> pathes);
        void Initialize(UIElementCollection children);
    }
}
