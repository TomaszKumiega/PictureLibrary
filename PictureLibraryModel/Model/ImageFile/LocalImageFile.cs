namespace PictureLibraryModel.Model
{
    public class LocalImageFile : ImageFile
    {
        public string? Path { get; set; }
        public override string IconUrl => Path ?? string.Empty;

        public LocalImageFile() : base()
        {

        }
    }
}
