using System;

namespace FileSystemVisitor
{
    internal class Program
    {
        const string root = @"C:\Users\Ivan_Tsar\Downloads\Test directory";
        static void Main(string[] args)
        {
            var fileVisitor = new FileSystemVisitor(root);
            fileVisitor.SearchFile(".docx", root);
            fileVisitor.DisplayAllSubFolders(root);
        }
    }
}
