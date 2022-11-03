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

        public FileSearcher FileSearcher { get; private set; } = new FileSearcher();

        public FileSystemVisitor(string rootDirectory)
        {
            filteringValue = string.Empty;
            IsDirectoryExists(rootDirectory);
            this.rootDirectory = rootDirectory;
        }

        public FileSystemVisitor(string rootDirectory, FilterAction filterAction, string filteringValue = "")
        {
            IsDirectoryExists(rootDirectory);
            this.rootDirectory = rootDirectory;
            this.filteringValue = filteringValue;
            this.filterAction = filterAction;
        }

        public string GetRootDirectory() => rootDirectory;

        public List<string> ExecuteFiltering(IEnumerable<string> files) => filterAction?.Invoke(files.ToList(), filteringValue);

        public void SearchFile(string fileName, string sDir)
        {
            FileSearcher.FileFound += new EventHandler<FileFoundArgs>((sender, eventArgs) =>
            {
                Console.WriteLine(eventArgs.FoundFile);
                eventArgs.CancelRequested = true;
            });

            FileSearcher.FileNotFound += new EventHandler<FileNotFoundArgs>((sender, eventArgs) =>
            {
                Console.WriteLine("File was not found.");
            });

            FileSearcher.Search(fileName, GetAllFilesFromDirectory(sDir));
        }

        public void DisplayAllSubFolders(string dir)
        {
            var foldersList = GetAllFoldersFromDirectory(dir).ToList();
            if (filteringValue.Equals(string.Empty))
            {
                foldersList.ForEach(folder => Console.WriteLine($"{folder} found."));
            }
            else
            {
                foldersList.ForEach(folder => Console.WriteLine($"Filtered {folder} found."));
            }

        }

        public void DisplayAllFilesInSubFolders(string dir)
        {
            var filesList = GetAllFilesFromDirectory(dir).ToList();
            if (filteringValue.Equals(string.Empty))
            {
                filesList.ForEach(file => Console.WriteLine($"{file} found."));
            }
            else
            {
                filesList.ForEach(file => Console.WriteLine($"Filtered {file} found."));
            }
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