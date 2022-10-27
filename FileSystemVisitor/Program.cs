using System;
using System.Linq;

namespace FileSystemVisitor
{
    internal class Program
    {
        const string root = @"C:\Users\Ivan_Tsar\Downloads\магистратура";
        static void Main(string[] args)
        {
            var fileVisitor = new FileSystemVisitor(root, Filter.NoFilter);
            fileVisitor.DisplayAllSubFolders(root);
            Console.WriteLine();
            fileVisitor.DisplayAllFilesInSubFolders(root);
        }
    }
}
