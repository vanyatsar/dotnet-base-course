using System.Collections.Generic;
using System.IO;

namespace FileSystemVisitor
{
    public class FileSystemVisitor
    {
        private string rootDirectory = @"C:\Users\Ivan_Tsar";

        public FileSystemVisitor()
        {
            IsDirectoryExists(rootDirectory);
        }

        public FileSystemVisitor(string rootDirectory)
        {
            IsDirectoryExists(rootDirectory);
            this.rootDirectory = rootDirectory;
        }

        public string GetRootDirectory() => rootDirectory;

        public IEnumerable<string> GetAllFoldersFromDirectory(string sDir)
        {
            foreach (var dir in Directory.GetDirectories(sDir))
            {
                yield return dir;
            }

            foreach (var directory in Directory.GetDirectories(sDir))
            {
                foreach (var dir in GetAllFilesFromDirectory(directory))
                {
                    yield return dir;
                }
            }
        }

        public IEnumerable<string> GetAllFilesFromDirectory(string sDir)
        {
            foreach (var file in Directory.GetFiles(sDir))
            {
                yield return file;
            }

            foreach (var directory in Directory.GetDirectories(sDir))
            {
                foreach (var file in GetAllFilesFromDirectory(directory))
                {
                    yield return file;
                }
            }
        }

        #region Private

        private bool IsDirectoryExists(string dir)
        {
            if (Directory.Exists(dir).Equals(false))
            {
                throw new DirectoryNotFoundException($"Directory {rootDirectory} was not found.");
            }
            return true;
        }

        #endregion
    }
}