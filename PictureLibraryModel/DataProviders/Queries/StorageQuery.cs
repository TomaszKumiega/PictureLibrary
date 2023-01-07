using System;
using System.Collections.Generic;

namespace PictureLibraryModel.DataProviders.Queries
{
    public class StorageQuery
    {
        public bool GetAllData { get; set; }
        public Guid? Id { get; set; }
        public List<Guid> DataSources { get; set; }
    }
}
