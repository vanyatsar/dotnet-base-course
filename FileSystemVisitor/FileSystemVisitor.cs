using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSystemVisitor
{
    public class FileSystemVisitor
    {
        public delegate List<string> FilterAction(List<string> arg1, string arg2);

        private string rootDirectory = @"C:\Users\Ivan_Tsar";
        private FilterAction? filterAction;
        private readonly string filteringValue;

        public FileSystemVisitor()
        {
            IsDirectoryExists(rootDirectory);
        }

        public FileSystemVisitor(string rootDirectory, FilterAction filterAction, string filteringValue = "")
        {
            IsDirectoryExists(rootDirectory);
            this.rootDirectory = rootDirectory;
            this.filteringValue = filteringValue;
            this.filterAction = filterAction;
        }

        public string GetRootDirectory() => rootDirectory;

        public List<string> ExecuteFiltering(IEnumerable<string> files)
        {
            return filterAction?.Invoke(files.ToList(), filteringValue);
        }

        private FilterAction SelectFilteringOption(FilterOption filterOption)
        {
            switch (filterOption)
            {
                case FilterOption.FileName: return Filter.FilterByName;
                case FilterOption.NoFilter: return Filter.NoFilter;
                default: return Filter.NoFilter;
            }
        }

        public string SearchFile(string fileName, string sDir) => GetAllFilesFromDirectory(sDir).Any(f => f.Contains(fileName))
            ? GetAllFilesFromDirectory(sDir).First(file => file.Contains(fileName))
            : $"File {fileName} was not found.";

        public void DisplayAllSubFolders(string dir) => GetAllFoldersFromDirectory(dir).ToList().ForEach(file => Console.WriteLine(file));
        public void DisplayAllFilesInSubFolders(string dir) => GetAllFilesFromDirectory(dir).ToList().ForEach(file => Console.WriteLine(file));

        public IEnumerable<string> GetAllFoldersFromDirectory(string sDir)
        {
            foreach (var dir in ExecuteFiltering(Directory.GetDirectories(sDir)))
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
            foreach (var file in ExecuteFiltering(Directory.GetFiles(sDir)))
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