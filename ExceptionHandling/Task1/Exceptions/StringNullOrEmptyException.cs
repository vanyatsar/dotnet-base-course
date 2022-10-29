using System;

namespace Task1.Exceptions
{
    internal class StringNullOrEmptyException: Exception
    {
        public StringNullOrEmptyException(string message) : base(message)
        {
        }
    }
}
