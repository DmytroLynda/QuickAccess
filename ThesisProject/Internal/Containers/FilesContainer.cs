using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThesisProject.Internal.Interfaces;
using ThesisProject.Internal.ViewModels;

namespace ThesisProject.Internal.Containers
{
    internal class FilesContainer : IFilesContainer
    {
        private UIElementCollection UIElements { get; set; }
        private Button SelectedPathButton { get; set;}

        public DeviceViewModel Device { get; set; }
        public DirectoryPathViewModel Directory { get; set; }

        public void Initialize(UIElementCollection uiElements)
        {
            UIElements = uiElements ?? throw new ArgumentNullException(nameof(uiElements));
        }

        public void Show(List<PathViewModel> pathes)
        {
            UIElements.Clear();

            foreach (var path in pathes)
            {
                if (Path.HasExtension(path.Value))
                {
                    UIElements.Add(MakeFileButton(path));
                }
                else
                {
                    UIElements.Add(MakeFolderButton(path));
                }
            }
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
        }

        private Button MakeFileButton(PathViewModel path)
        {
            var folderButton = new Button
            {
                Width = 50,
                Height = 50,
                Margin = new Thickness(0, 5, 5, 5),
                Content = Path.GetFileName(path.Value),
            };

            folderButton.Click += ElementSelected;
            return folderButton;
        }
    }
}
