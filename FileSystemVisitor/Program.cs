using System;

namespace FileSystemVisitor
{
    internal class Program
    {
        const string root = @"C:\Users\Ivan_Tsar\Downloads\магистратура";
        static void Main(string[] args)
        {
            var fileVisitor = new FileSystemVisitor(root, Filter.FilterByName, ".docx");
            fileVisitor.DisplayAllSubFolders(root);
            Console.WriteLine();
            fileVisitor.DisplayAllFilesInSubFolders(root);
        }
    }
}
