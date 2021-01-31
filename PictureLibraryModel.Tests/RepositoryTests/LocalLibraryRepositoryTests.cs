using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PictureLibraryModel.Tests.RepositoryTests
{
    public class LocalLibraryRepositoryTests
    {
        #region Setup
        private string TestFolder = "Tests\\";

        public LocalLibraryRepositoryTests()
        {
            CleanupFiles();
        }

        ~LocalLibraryRepositoryTests()
        {
            CleanupFiles();
        }

        private void CleanupFiles()
        {
            try
            {
                Directory.Delete(TestFolder, true);
            }
            catch
            {
                return;
            }
        }
        #endregion
    }
}
