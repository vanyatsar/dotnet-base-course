using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSystemVisitor
{
    public class FileSystemVisitor
    {
        public delegate List<string> FilterAction(List<string> arg1, string arg2);
        public delegate void FoundItemsHandler(string message);

        private string rootDirectory = @"C:\Users\Ivan_Tsar";
        private event FoundItemsHandler? Notify;

        private FilterAction? filterAction;
        private readonly string filteringValue;

        public FileSystemVisitor(string rootDirectory)
        {
            filteringValue = string.Empty;
            IsDirectoryExists(rootDirectory);
            this.rootDirectory = rootDirectory;
            Notify += DisplayMessage;
        }

        public FileSystemVisitor(string rootDirectory, FilterAction filterAction, string filteringValue = "")
        {
            IsDirectoryExists(rootDirectory);
            this.rootDirectory = rootDirectory;
            this.filteringValue = filteringValue;
            this.filterAction = filterAction;
            Notify += DisplayMessage;
        }

        public string GetRootDirectory() => rootDirectory;

        public List<string> ExecuteFiltering(IEnumerable<string> files) => filterAction?.Invoke(files.ToList(), filteringValue);

        public string SearchFile(string fileName, string sDir) => GetAllFilesFromDirectory(sDir).Any(f => f.Contains(fileName))
            ? GetAllFilesFromDirectory(sDir).First(file => file.Contains(fileName))
            : $"File {fileName} was not found.";

        public void DisplayAllSubFolders(string dir)
        {
            Notify?.Invoke("Start searching...");

            var foldersList = GetAllFoldersFromDirectory(dir).ToList();
            if (filteringValue.Equals(string.Empty))
            {
                foldersList.ForEach(folder => Notify?.Invoke($"{folder} found."));
            }
            else
            {
                foldersList.ForEach(folder => Notify?.Invoke($"Filtered {folder} found."));
            }

            Notify?.Invoke($"Finish searching...\nFound {foldersList.Count} folders total.");
        }

        public void DisplayAllFilesInSubFolders(string dir)
        {
            Notify?.Invoke("Start searching...");
            var filesList = GetAllFilesFromDirectory(dir).ToList();
            if (filteringValue.Equals(String.Empty))
            {
                filesList.ForEach(file => Notify?.Invoke($"{file} found."));
            }
            else
            {
                filesList.ForEach(file => Notify?.Invoke($"Filtered {file} found."));
            }

            Notify?.Invoke($"Finish searching...\nFound {filesList.Count} files total.");
        }

        public IEnumerable<string> GetAllFoldersFromDirectory(string sDir)
        {

            if (filteringValue == "")
            {
                foreach (var dir in Directory.GetDirectories(sDir))
                {
                    yield return dir;
                }
            }
            else
            {
                foreach (var dir in ExecuteFiltering(Directory.GetDirectories(sDir)))
                {
                    yield return dir;
                }
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
            if (filteringValue == "")
            {
                foreach (var file in Directory.GetFiles(sDir))
                {
                    yield return file;
                }
            }
            else
            {
                foreach (var file in ExecuteFiltering(Directory.GetFiles(sDir)))
                {
                    yield return file;
                }
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

        private FilterAction SelectFilteringOption(FilterOption filterOption)
        {
            switch (filterOption)
            {
                case FilterOption.FileName: return Filter.FilterByName;
                default: return Filter.FilterByName;
            }
        }

        private void DisplayMessage(string message) => Console.WriteLine(message);

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