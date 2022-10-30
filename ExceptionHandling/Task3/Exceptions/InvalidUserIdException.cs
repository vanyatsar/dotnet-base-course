using System;

namespace Task3.Exceptions
{
    public class InvalidUserIdException : ArgumentException
    {
        public InvalidUserIdException(string message, int userId) : base(message)
        {
            UserId = userId;
        }

        public int UserId { get; }
    }
}
