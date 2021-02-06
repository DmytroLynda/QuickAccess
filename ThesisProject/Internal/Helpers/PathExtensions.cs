using System;
using System.IO;
using ThesisProject.Internal.ViewModels;

namespace ThesisProject.Internal.Helpers
{
    internal static class PathExtensions
    {
        public static bool IsDirectory(this string path)
        {
            bool isFile = IsFile(path);
            bool isDrive = IsDrive(path);
            
            return !isFile && !isDrive;
        }

        public static bool IsDirectory(this PathViewModel path)
        {
            return path.Value.IsDirectory();
        }

        public static bool IsDrive(this PathViewModel path)
        {
            return path.Value.IsDrive();
        }

        public static bool IsDrive(this string path)
        {
            return path.EndsWith(@":\");
        }

        public static bool IsFile(this string path)
        {
            return Path.HasExtension(path);
        }

        public static bool IsFile(this PathViewModel path)
        {
            return Path.HasExtension(path.Value);
        }

        public static FilePathViewModel ToFilePath(this PathViewModel path)
        {
            if (path.IsDirectory())
            {
                throw new InvalidOperationException("Curent path value is not a file path.");
            }

            return new FilePathViewModel { Path = path.Value };
        }

        public static DirectoryPathViewModel ToDirectoryPath(this PathViewModel path)
        {
            if (path.IsFile())
            {
                throw new InvalidOperationException("Curent path value is not a directory path.");
            }

            return new DirectoryPathViewModel { Path = path.Value };
        }

        public static PathViewModel ToPath(this DirectoryPathViewModel directoryPath)
        {
            return new PathViewModel { Value = directoryPath.Path };
        }

        public static PathViewModel ToPath(this FilePathViewModel filePath)
        {
            return new PathViewModel { Value = filePath.Path };
        }

        public static PathViewModel GetName(this PathViewModel path)
        {
            if (path.IsFile() || path.IsDirectory())
            {
                var preparedPath = Path.TrimEndingDirectorySeparator(path.Value);
                var pathName = preparedPath.Remove(startIndex: 0, count: path.Value.LastIndexOf('\\') + 1);

                return new PathViewModel(pathName);
            }
            else
            {
                return path;
            }
        }

        public static PathViewModel JoinAsRoot(this PathViewModel path, DirectoryPathViewModel directory)
        {
            if (Path.IsPathRooted(path.Value))
            {
                return path;
            }
            else
            {
                return new PathViewModel { Value = Path.Join(directory.Path, path.Value) };
            }
        }

        public static DirectoryPathViewModel GetParrent(this DirectoryPathViewModel directory)
        {
            string parrentDirectory;
            if (string.IsNullOrWhiteSpace(directory.Path) ||
                directory.Path.IsDrive())
            {
                parrentDirectory = string.Empty;
            }
            else if(directory.Path.IsDirectory())
            {
                parrentDirectory = Directory.GetParent(directory.Path).FullName;
            }
            else
            {
                throw new ArgumentException("Passed directory is not a directory or a drive.");
            }

            return new DirectoryPathViewModel { Path = parrentDirectory };
        }
    }
}
