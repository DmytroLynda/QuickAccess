using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ThesisProject.Internal.Interfaces;
using ThesisProject.Internal.ViewModels;

namespace ThesisProject.Internal.Containers
{
    internal class FilesContainer : IFilesContainer
    {
        private UIElementCollection UIElements { get; set; }
        private List<(Button button, PathViewModel path)> ButtonPatches { get; set; }
        private Button SelectedPathButton { get; set;}

        public DeviceViewModel CurentDevice { get; set; }
        public DirectoryPathViewModel CurentDirectory { get; set; }

        public event EventHandler<DirectoryPathViewModel> OpenDirectory;
        public event EventHandler<FilePathViewModel> DownloadFile;
        public event EventHandler<FilePathViewModel> FileInfo;

        public FilesContainer()
        {
            ButtonPatches = new List<(Button button, PathViewModel path)>();
        }

        public void Initialize(UIElementCollection uiElements)
        {
            UIElements = uiElements ?? throw new ArgumentNullException(nameof(uiElements));
        }

        public void Show(List<PathViewModel> pathes)
        {
            ButtonPatches.Clear();
            foreach (var path in pathes)
            {
                if (Path.HasExtension(path.Value))
                {
                    ButtonPatches.Add((MakeFileButton(path), path));
                }
                else
                {
                    ButtonPatches.Add((MakeFolderButton(path), path));
                }
            }

            UIElements.Clear();
            ButtonPatches.ForEach(buttonPath => UIElements.Add(buttonPath.button));
        }

        private Button MakeFolderButton(PathViewModel path)
        {
            var folderButton = new Button
            {
                Width = 50,
                Height = 50,
                Margin = new Thickness(0, 5, 5, 5),
                Content = Path.GetDirectoryName(path.Value),
            };

            folderButton.MouseDoubleClick += OnFolderOpen;
            return folderButton;
        }

        private void OnFileInfo(object sender, RoutedEventArgs e)
        {
            var path = ButtonPatches.First(buttonPath => buttonPath.button == SelectedPathButton).path;
            FileInfo(this, new FilePathViewModel { Path = path.Value });
        }

        private void OnFileDownload(object sender, RoutedEventArgs e)
        {
            var path = ButtonPatches.First(buttonPath => buttonPath.button == SelectedPathButton).path;
            DownloadFile(this, new FilePathViewModel { Path = path.Value });
        }

        private void OnFolderOpen(object sender, MouseButtonEventArgs e)
        {
            var path = ButtonPatches.First(buttonPath => buttonPath.button == sender as Button).path;
            OpenDirectory(this, new DirectoryPathViewModel { Path = path.Value });
        }

        private Button MakeFileButton(PathViewModel path)
        {
            var contextMenu = new ContextMenu();

            var downloadMenuItem = new MenuItem
            {
                Header = "Download",
            };
            downloadMenuItem.Click += OnFileDownload;

            var fileInfoMenuItem = new MenuItem
            {
                Header = "Show info"
            };
            fileInfoMenuItem.Click += OnFileInfo;

            contextMenu.Items.Add(downloadMenuItem);
            contextMenu.Items.Add(fileInfoMenuItem);

            var folderButton = new Button
            {
                Width = 50,
                Height = 50,
                Margin = new Thickness(0, 5, 5, 5),
                Content = Path.GetFileName(path.Value),
                ContextMenu = contextMenu
            };

            folderButton.Click += ElementSelected;
            return folderButton;
        }

        private void ElementSelected(object sender, RoutedEventArgs e)
        {
            if (SelectedPathButton is not null)
            {
                SelectedPathButton.IsEnabled = true;
            }


            SelectedPathButton = sender as Button;
            SelectedPathButton.IsEnabled = false;
        }
    }
}
