using System;

namespace HelloLibrary
{
    public static class Greetings
    {
        public static string GetCurrentTimeGreetings(string userName)
        {
            return $"{DateTime.Now.ToShortTimeString()} - Hello, {userName}!";
        }
    }
}
