using System;

namespace FileSystemVisitor
{
    internal class Program
    {
        const string root = @"C:\Users\Ivan_Tsar\Downloads\Test directory";
        static void Main(string[] args)
        {
            var fileVisitor = new FileSystemVisitor(root, Filter.FilterByName, FilterOption.FileName, ".txt");
            fileVisitor.DisplayAllFilesInSubFolders(root, true, "Text Document.txt");
            Console.WriteLine();

            fileVisitor.SearchFile("Text Document.txt", root);
            fileVisitor.DisplayAllSubFolders(root);
        }
    }
}
