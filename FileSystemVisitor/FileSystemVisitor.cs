using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using FileSystemVisitor.EventArguments;
using FileSystemVisitor.FileUtilities;

namespace FileSystemVisitor
{
    public class FileSystemVisitor
    {
        private readonly string rootDirectory = @"C:\Users\Ivan_Tsar\Downloads\Test directory";
        private readonly string filterValue;
        private readonly FilterAction filterAction;

        public delegate List<string> FilterAction(List<string> collection, string filterValue);
        public event EventHandler<NewMessageEventArgs> OnStart;
        public event EventHandler<NewMessageEventArgs> OnFinish;

        public FileSearcher FileSearcher { get; private set; } = new FileSearcher();

        public FileSystemVisitor(string rootDirectory, FilterAction filterAction, string filterValue = "")
        {
            IsDirectoryExists(rootDirectory);
            this.rootDirectory = rootDirectory;
            this.filterValue = filterValue;
            this.filterAction = filterAction;
        }

        public string GetRootDirectory() => rootDirectory;

        public List<string> ExecuteFiltering(IEnumerable<string> files)
        {
            return filterAction?.Invoke(files.ToList(), filterValue);
        }

        public void SearchFile(string fileName, string sDir)
        {
            OnStartMessage("\nSearch was started.");

            FileSearcher.FileFound += new EventHandler<FileFoundArgs>((sender, eventArgs) =>
            {
                Console.WriteLine("Found file is: {0}", eventArgs.FoundFile);
                eventArgs.CancelRequested = true;
            });

            FileSearcher.FileNotFound += new EventHandler<FileNotFoundArgs>((sender, eventArgs) =>
            {
                Console.WriteLine("File was not found.");
            });

            FileSearcher.Search(fileName, GetAllFilesFromDirectory(sDir));

            OnFinishMessage("Search was finished.\n");
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

        private void OnStartMessage(string message)
        {
            NewMessageEventArgs e = new NewMessageEventArgs(message);
            EventHandler<NewMessageEventArgs> temp = Volatile.Read(ref OnStart);

            temp?.Invoke(this, e);
        }

        private void OnFinishMessage(string message)
        {
            NewMessageEventArgs e = new NewMessageEventArgs(message);
            EventHandler<NewMessageEventArgs> temp = Volatile.Read(ref OnFinish);

            temp?.Invoke(this, e);
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