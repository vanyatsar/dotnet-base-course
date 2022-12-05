using System;
using System.Linq;
using FileSystemVisitor.FileUtilities;

namespace FileSystemVisitor
{
    internal class Program
    {
        const string root = @"C:\Users\Ivan_Tsar\Downloads\Test directory";
        static void Main(string[] args)
        {
            var fileVisitor = new FileSystemVisitor(root, Filter.FilterByName, ".txt");

            fileVisitor.OnStart += (sender, args) => 
            { 
                Console.WriteLine(args.Message);
            };

            fileVisitor.OnFinish += (sender, args) => 
            { 
                Console.WriteLine(args.Message);
            };

            fileVisitor.GetAllFilesFromDirectory(root).ToList().ForEach(f => Console.WriteLine(f));

            fileVisitor.SearchFile("Text Document.txt", root);
        }
    }
}
