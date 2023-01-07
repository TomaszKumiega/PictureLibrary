using System;

namespace PictureLibraryModel.Exceptions
{
    public class InvalidQueryException : Exception
    {
        private readonly string _message;

        public InvalidQueryException(string message)
        {
            _message = message;
        }

        public override string Message 
            => $"Invalid query. {_message}";
    }
}
