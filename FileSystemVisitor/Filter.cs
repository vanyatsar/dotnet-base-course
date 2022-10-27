using System.Collections.Generic;
using System.Linq;

namespace FileSystemVisitor
{
    public class Filter
    {
        public List<string> FilterByName(List<string> files, string filterName)
        {
            return files.Where(f => f.Contains(filterName)).ToList();
        }
    }
}
