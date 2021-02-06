using System;
using System.IO;
using ThesisProject.Internal.ViewModels;

namespace ThesisProject.Internal.Helpers
{
    internal static class PathExtensions
    {
        public static bool IsDirectory(this string path)
        {
            return !Path.HasExtension(path);
        }

        public static bool IsDirectory(this PathViewModel path)
        {
            return !Path.HasExtension(path.Value);
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
            if (path.IsFile())
            {
                return new PathViewModel { Value = Path.GetFileName(path.Value) };
            }
            else if(path.IsDirectory() && !Path.IsPathRooted(path.Value))
            {
                return new PathViewModel { Value = Path.GetDirectoryName(path.Value) };
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

        public static DirectoryPathViewModel Back(this DirectoryPathViewModel directory)
        {
            var oneBackDirectory = Directory.GetParent(directory.Path);

            if (oneBackDirectory is null)
            {
                return new DirectoryPathViewModel { Path = string.Empty };
            }

            return new DirectoryPathViewModel { Path = oneBackDirectory.FullName };
        }
    }
}
