using System;
using System.Collections.Generic;

namespace FileSystemVisitor
{
    public class FileSearcher
    {
        public event EventHandler<FileFoundArgs> FileFound;
        public event EventHandler<FileNotFoundArgs> FileNotFound;

        public void Search(string file, IEnumerable<string> searchList)
        {
            FileFoundArgs args = null;
            foreach (var f in searchList)
            {
                if (f.Equals(file))
                {
                    args = RaiseFileFound(f);
                    if (args.CancelRequested)
                    {
                        break;
                    }
                }
            }
            
            if (args == null)
            {
                RaiseFileNotFound(true);
            } 
        }

        private FileFoundArgs RaiseFileFound(string file)
        {
            var args = new FileFoundArgs(file);
            FileFound?.Invoke(this, args);
            return args;
        }

        private FileNotFoundArgs RaiseFileNotFound(bool foundStatus)
        {
            var args = new FileNotFoundArgs(foundStatus);
            FileNotFound?.Invoke(this, args);
            return args;
        }
    }
}