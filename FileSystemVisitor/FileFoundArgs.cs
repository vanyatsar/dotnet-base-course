using System;

namespace FileSystemVisitor
{
    public class FileFoundArgs : EventArgs
    {
        public string FoundFile { get; }
        public bool CancelRequested { get; set; } = false;

        public FileFoundArgs(string fileName) => FoundFile = fileName;
    }
}
