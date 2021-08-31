using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PictureLibraryModel.Exceptions.DataAccessExceptions
{
    public class LibraryFileIOException : IOException
    {
        public LibraryFileIOException(string message) : base(message)
        {

        }

        public LibraryFileIOException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
