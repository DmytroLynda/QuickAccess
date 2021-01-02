namespace ThesisProject.Internal.ViewModels
{
    public class DirectoryPathViewModel
    {
        public string Path { get; set; }

        public static DirectoryPathViewModel Root { get => new DirectoryPathViewModel { Path = string.Empty }; }
    }
}
