namespace PictureLibrary.DataAccess.Queries
{
    public class StorageQuery
    {
        public bool GetAllData { get; set; }
        public Guid? Id { get; set; }
        public List<Guid?>? DataSources { get; set; }
    }
}
