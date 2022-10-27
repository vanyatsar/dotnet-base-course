using System.Collections.Generic;
using System.Linq;

namespace FileSystemVisitor
{
    public static class Filter
    {
        public static List<string> FilterByName(List<string> files, string filterValue)
        {
            return files.Where(f => f.Contains(filterValue)).ToList();
        }

        public static List<string> NoFilter(List<string> files, string filterValue)
        {
            return files;
        }
    }
}
