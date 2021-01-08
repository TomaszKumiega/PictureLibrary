using PictureLibraryModel.Services;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace PictureLibraryModel.Model
{
    public class Directory : IFileSystemEntity
    {
        private FileSystemService FileSystemService { get; set; }

        public string FullPath { get; set; }
        public string Name { get; set; }
        public string IconSource { get; }
        public ObservableCollection<Directory> SubDirectories { get; }

        public Directory(string path, string name, FileSystemService fileSystemService)
        {
            FullPath = path;
            Name = name;
            FileSystemService = fileSystemService;
            IconSource = "pack://application:,,,/Icons/FolderIcon.png";
            SubDirectories = new ObservableCollection<Directory>();
            LoadChildren();
        }

        private void LoadChildren()
        {
            SubDirectories.Clear();

            var directories = FileSystemService.GetDirectories(FullPath, SearchOption.TopDirectoryOnly);

            foreach (var t in directories)
            {
                SubDirectories.Add(t);
            }
        }
    }
}
