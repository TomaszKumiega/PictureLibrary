using System.Collections.Generic;

namespace PictureLibraryModel.Model
{
    public class ApiLibrary : RemoteLibrary
    {
        public List<APIUser>? Owners { get; set; }
    }
}
