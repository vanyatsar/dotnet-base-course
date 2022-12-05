using System.Collections.Generic;
using System.Linq;

namespace FileSystemVisitor.FileUtilities
{
    public static class Filter
    {
        public static List<string> FilterByName(List<string> files, string filterValue)
        {
            return files.FindAll(f => f.Contains(filterValue)).ToList();
        }
    }
}
