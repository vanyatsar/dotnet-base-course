using System;

namespace Task3.Exceptions
{
    public class TaskAlreadyExistsException : Exception
    {
        public TaskAlreadyExistsException(string message) : base(message)
        {
        }
    }
}
