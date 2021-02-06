using System;
using System.Globalization;
using System.Windows;
using ThesisProject.Internal.ViewModels;

namespace ThesisProject.Internal.Windows
{
    /// <summary>
    /// Interaction logic for FileInfoWindow.xaml
    /// </summary>
    internal partial class FileInfoWindow : Window
    {
        public FileInfoWindow()
        {
            InitializeComponent();
        }

        public void Show(FileInfoViewModel fileInfo)
        {
            this.Show();

            FileNameTextBox.Text = fileInfo.Name;
            DirectoryTextBox.Text = fileInfo.Directory;
            SizeTextBox.Text = FormFileSize(fileInfo.Size);
            CreatedTextBox.Text = FormDate(fileInfo.Created);
            LastChangedTextBox.Text = FormDate(fileInfo.LastChanged);
        }

        private string FormDate(DateTime date)
        {
            var englishCulture = new CultureInfo("en-US");

            return date.ToString("dddd, dd MMMM yyyy", englishCulture);
        }

        private string FormFileSize(long size)
        {
            const int bytesInMegabyte = 1048576;
            double megabyteSize = (double) size / bytesInMegabyte;
            string megabyteSizeText = Math.Round(megabyteSize, 2).ToString() + " " + "MB";

            const int bytesInGigabyte = 1073741824;
            double gigabyteSize = (double) size / bytesInGigabyte;
            string gigabyteSizeText = Math.Round(gigabyteSize, 2).ToString() + " " + "GB";

            return string.Format($"{megabyteSizeText} ({gigabyteSizeText})");
        }
    }
}
