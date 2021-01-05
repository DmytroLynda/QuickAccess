using System;
using System.Collections.Generic;
using System.Windows.Controls;
using ThesisProject.Internal.ViewModels;

namespace ThesisProject.Internal.Interfaces
{
    internal interface IFilesContainer
    {
        event EventHandler<DirectoryPathViewModel> OpenDirectory;

        event EventHandler<FilePathViewModel> DownloadFile;

        event EventHandler<FilePathViewModel> FileInfo;

        DeviceViewModel CurentDevice { get; set; }

        DirectoryPathViewModel CurentDirectory { get; set; }

        void Show(IEnumerable<PathViewModel> pathes);

        void Initialize(UIElementCollection children);

    }
}
