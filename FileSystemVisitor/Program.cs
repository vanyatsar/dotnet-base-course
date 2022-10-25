using System;

namespace FileSystemVisitor
{
    internal class Program
    {
        const string root = @"C:\Users\Ivan_Tsar\Downloads";
        static void Main(string[] args)
        {
            var fileVisitor = new FileSystemVisitor();
            var files = fileVisitor.GetAllFoldersFromDirectory(root);

            Console.WriteLine("List of Files");
            foreach (var file in files)
            {
                Console.WriteLine("{0}", file);
            }
        }
    }
}
