using System;

namespace FileSystemVisitor
{
    public class FileNotFoundArgs : EventArgs
    {
        public bool FileNotFound { get; }

        public FileNotFoundArgs(bool fileNotFound) => FileNotFound = fileNotFound;
    }
}