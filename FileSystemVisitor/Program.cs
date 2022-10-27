using System;
using System.Linq;

namespace FileSystemVisitor
{
    internal class Program
    {
        const string root = @"C:\Users\Ivan_Tsar\Downloads\магистратура";
        static void Main(string[] args)
        {
            var fileVisitor = new FileSystemVisitor(root, FilterOption.FileName);
            var files = fileVisitor.GetAllFilesFromDirectory(root);
            fileVisitor.DisplayAllFilesInSubFolders(root);
            Console.WriteLine();
            var filtered = fileVisitor.ExecuteFiltering(files, ".doc");

            foreach (var item in filtered)
            {
                Console.WriteLine(item);
            }
        }
    }
}
