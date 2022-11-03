using System.IO;
using System;
using System.Collections.Generic;

namespace FileSystemVisitor
{
    public class FileSearcher
    {
        public event EventHandler<FileFoundArgs> FileFound;

        public void Search(string file, IEnumerable<string> searchList)
        {
            foreach (var f in searchList)
            {
                if (f.Equals(file))
                {
                    FileFoundArgs args = RaiseFileFound(f);
                    if (args.CancelRequested)
                    {
                        break;
                    }
                }
            }
        }

        private FileFoundArgs RaiseFileFound(string file)
        {
            var args = new FileFoundArgs(file);
            FileFound?.Invoke(this, args);
            return args;
        }
    }
}