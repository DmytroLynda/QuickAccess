using System;
using System.IO;
using System.Linq;
using ThesisProject.Internal.Enums;
using ThesisProject.Internal.Helpers;
using ThesisProject.Internal.Interfaces;
using ThesisProject.Internal.ViewModels;

namespace ThesisProject.Internal.Containers
{
    internal class FilesContainer : BaseContainer<PathViewModel>, IFilesContainer
    {
        public DeviceViewModel CurentDevice { get; set; }
        public DirectoryPathViewModel CurentDirectory { get; set; }

        public event EventHandler<DirectoryPathViewModel> OpenDirectory;
        public event EventHandler<FilePathViewModel> DownloadFile;
        public event EventHandler<FilePathViewModel> FileInfo;

        public FilesContainer()
        {
            var fileOptionsNames = Enum.GetValues<FileOption>().Select(option => option.ToString());
            foreach (var fileOptionName in fileOptionsNames)
            {
                AddContextOption(fileOptionName, (content) => content.IsFile());
            }
            ContextOptionSelected += OnContextOptionSelected;

            LeftClick += OnLeftClick;
        }

        private void OnContextOptionSelected(object sender, (PathViewModel path, string header) contextOption)
        {
            var selectedFileOption = Enum.GetValues<FileOption>().First(option => option.ToString() == contextOption.header);

            switch (selectedFileOption)
            {
                case FileOption.Download:
                    DownloadFile(this, contextOption.path.ToFilePath());
                    break;
                case FileOption.ShowInfo:
                    FileInfo(this, contextOption.path.ToFilePath());
                    break;
                default:
                    throw new ArgumentException($"Unknown {nameof(FileOption)} type: {selectedFileOption}.");
            }
        }

        private void OnLeftClick(object sender, PathViewModel path)
        {
            if (path.IsDirectory())
            {
                OpenDirectory(this, path.ToDirectoryPath());
            }
        }
    }
}
