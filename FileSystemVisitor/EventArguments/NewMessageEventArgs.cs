using System;

namespace FileSystemVisitor.EventArguments
{
    public class NewMessageEventArgs : EventArgs
    {
        private readonly string _message;

        public NewMessageEventArgs(string message)
        {
            _message = message;
        }

        public string Message { get { return _message; } }
    }
}
