using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PictureLibraryModel.Exceptions.DataAccessExceptions
{
    public class ImportedLibraryNotFoundException : FileNotFoundException
    {
        public ImportedLibraryNotFoundException(string message) : base(message)
        {

        }

        public ImportedLibraryNotFoundException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
