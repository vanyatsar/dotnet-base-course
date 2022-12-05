using System;

namespace FileSystemVisitor.EventArguments
{
    public class FileNotFoundArgs : EventArgs
    {
        public bool FileNotFound { get; }

        public FileNotFoundArgs(bool fileNotFound) => FileNotFound = fileNotFound;
    }
}