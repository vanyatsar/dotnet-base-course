using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSystemVisitor
{
    public class FileSystemVisitor
    {
        public delegate List<string> FilterAction(List<string> arg1, string arg2);
        public delegate void DisplayMessageAction(string message);
        private readonly string rootDirectory = @"C:\Users\Ivan_Tsar\Downloads\Test directory";
        private readonly string filteringValue;
        private FilterAction? filterAction;
        private event DisplayMessageAction MessageDisplayed;
        private FilterOption? filterOption;

        public FileSearcher FileSearcher { get; private set; } = new FileSearcher();

        public FileSystemVisitor(string rootDirectory)
        {
            filteringValue = string.Empty;
            filterOption = FilterOption.NoFilter;
            IsDirectoryExists(rootDirectory);
            this.rootDirectory = rootDirectory;
            
        }

        public FileSystemVisitor(string rootDirectory, FilterAction filterAction, FilterOption filterOption, string filteringValue = "")
        {
            IsDirectoryExists(rootDirectory);
            this.rootDirectory = rootDirectory;
            this.filteringValue = filteringValue;
            this.filterAction = filterAction;
            this.filterOption = filterOption;
        }

        public string GetRootDirectory() => rootDirectory;

        public List<string> ExecuteFiltering(IEnumerable<string> files)
        {
            return filterAction?.Invoke(files.ToList(), filteringValue);
        }

        public void SearchFile(string fileName, string sDir)
        {
            MessageDisplayed += DisplayMessage;

            MessageDisplayed?.Invoke("Start searching");
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
            MessageDisplayed?.Invoke("Search was finished");

            MessageDisplayed -= DisplayMessage;
        }

        public void DisplayAllSubFolders(string dir)
        {
            MessageDisplayed += DisplayMessage;

            var foldersList = GetAllFoldersFromDirectory(dir).ToList();
            if (filterOption == FilterOption.NoFilter || filterOption == FilterOption.FileName)
            {
                foldersList.ForEach(folder => MessageDisplayed?.Invoke($"{folder} found."));
            }
            else
            {
                ExecuteFiltering(foldersList).ForEach(folder => MessageDisplayed?.Invoke($"Filtered {folder} found."));
            }

            MessageDisplayed -= DisplayMessage;
        }

        public void DisplayAllFilesInSubFolders(string dir, bool exclude = false, string filesToExclude = "")
        {
            MessageDisplayed += DisplayMessage;
            var filesList = GetAllFilesFromDirectory(dir).ToList();

            if (filterOption == FilterOption.FolderName || filterOption == FilterOption.NoFilter)
            {
                filesList.ForEach(file => MessageDisplayed?.Invoke($"{file} found."));
            }
            else
            {
                ExecuteFiltering(filesList).ForEach(file => MessageDisplayed?.Invoke($"Filtered {file} found."));
            }

            if (exclude)
            {
                filesList.RemoveAll(f => f.Equals(filesToExclude));
            }

            MessageDisplayed -= DisplayMessage;
        }

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
                yield return Path.GetFileName(file);
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

        private void DisplayMessage(string message) => Console.WriteLine(message);

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