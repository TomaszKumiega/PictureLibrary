using System;

namespace PictureLibraryModel.Exceptions
{
    public class LibraryAlreadyExistsException : Exception
    {
        public override string Message => $"Library with name: {_libraryName} already exists";

        private string _libraryName;

        public LibraryAlreadyExistsException(string libraryName)
        {
            _libraryName = libraryName;
        }
    }
}
