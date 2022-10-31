using System;
using System.Linq;
using Task1.Exceptions;

namespace Task1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                PrintNewLineFirstCharacter("Multiline\ntest\nexample");
                PrintNewLineFirstCharacter(null);
            }
            catch (StringNullOrEmptyException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void PrintNewLineFirstCharacter(string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
            {
                throw new StringNullOrEmptyException($"String value was null or empty. Value: {inputString}");
            }

            inputString.Split('\n').ToList().ForEach(s => Console.WriteLine(s.First()));
        }
    }
}