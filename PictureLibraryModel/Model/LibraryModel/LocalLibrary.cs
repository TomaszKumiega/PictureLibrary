using System.Collections.Generic;

namespace PictureLibraryModel.Model
{
    public class LocalLibrary : Library
    { 
        public string FilePath { get; set; }
        public IEnumerable<LocalImageFile> ImageFiles { get; set; }
    }
}
