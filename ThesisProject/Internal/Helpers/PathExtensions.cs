using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return !path.IsFile();
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

        public static PathViewModel ToPath(FilePathViewModel filePath)
        {
            return new PathViewModel { Value = filePath.Path };
        }
    }
}
