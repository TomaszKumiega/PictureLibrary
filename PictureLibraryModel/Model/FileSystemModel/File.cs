using System.Drawing;

namespace PictureLibraryModel.Model
{
    public abstract class File : IFileSystemElement
    {
        public string Name { get; set; }
        public string Extension { get; set; }
        public Image Icon { get; protected set; }
        public string Path { get; set; }
        
        public File()
        {
          
        }

        public abstract void LoadIcon();
        public virtual bool Exists()
        {
            return System.IO.File.Exists(Path);
        }
    }
}
