using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSystemVisitor
{
    public class FileSystemVisitor
    {
        private string rootDirectory = @"C:\Users\Ivan_Tsar";

        private Filter Filter => new Filter();

        private delegate List<string> FilterAction(List<string> arg1, string arg2);

        private readonly FilterAction filterAction;

        public FileSystemVisitor()
        {
            IsDirectoryExists(rootDirectory);
        }

        public FileSystemVisitor(string rootDirectory, FilterOption filterOption)
        {
            IsDirectoryExists(rootDirectory);
            this.rootDirectory = rootDirectory;

            switch (filterOption)
            {
                case FilterOption.FileName:
                    filterAction = Filter.FilterByName;
                    break;
                case FilterOption.NumberOfNameSymbols:   
                    break;
                default:
                    break;
            }
        }

        public FileSystemVisitor(string rootDirectory)
        {
            IsDirectoryExists(rootDirectory);
            this.rootDirectory = rootDirectory;
        }

        public string GetRootDirectory() => rootDirectory;

        public List<string> ExecuteFiltering(IEnumerable<string> files, string filterName)
        {
            return filterAction(files.ToList(), filterName);
        }

        public string SearchFile(string fileName, string sDir) => GetAllFilesFromDirectory(sDir).Any(f => f.Contains(fileName))
            ? GetAllFilesFromDirectory(sDir).First(file => file.Contains(fileName))
            : $"File {fileName} was not found.";

        public void DisplayAllSubFolders(string dir) => GetAllFoldersFromDirectory(dir).ToList().ForEach(file => Console.WriteLine(file));
        public void DisplayAllFilesInSubFolders(string dir) => GetAllFilesFromDirectory(dir).ToList().ForEach(file => Console.WriteLine(file));

        public IEnumerable<string> GetAllFoldersFromDirectory(string sDir)
        {
            foreach (var dir in Directory.GetDirectories(sDir))
            {
                yield return dir;
            }

            foreach (var directory in Directory.GetDirectories(sDir))
            {
                foreach (var dir in GetAllFoldersFromDirectory(directory))
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