using HelloLibrary;
using System;

namespace HelloCoreApp
{
    internal class Hello
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Greetings.GetCurrentTimeGreetings(args[0]));
        }
    }
}
