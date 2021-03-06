﻿using System.Threading.Tasks;
using System.Windows;
using ThesisProject.Internal.ViewModels;

namespace ThesisProject.Internal.Interfaces
{
    internal interface IWindowManager
    {
        Task ShowMainWindowAsync(Window caller);
        void ShowLoginWindow(Window caller);
        void ShowFileInfoWindow(FileInfoViewModel fileInfo);
        void ShowConfigurationWindow();
    }
}
