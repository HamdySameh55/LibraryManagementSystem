using System;

namespace LibraryManagementSystem.Exceptions
{
    public class LibraryException : Exception
    {
        public LibraryException()
        {
        }

        public LibraryException(string message)
            : base(message)
        {
        }
    }
}
