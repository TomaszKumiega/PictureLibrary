using PictureLibraryModel.Services;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace PictureLibraryModel.Model
{
    public class Directory
    {
        private bool _isExpanded;
        private IFileSystemService FileSystemService { get; }

        public string FullPath { get; }
        public string Name { get; }
        public ObservableCollection<object> Children { get; }


        public Directory(string fullPath, string name, IFileSystemService fileSystemService)
        {
            FullPath = fullPath;
            Name = name;
            FileSystemService = fileSystemService;
            this.Children = new ObservableCollection<object>();
        }

        /// <summary>
        /// Initializes new instance of <see cref="Directory"/> class, children aren't loaded on runtime and are specified as argument in constructor
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="children"></param>
        public Directory(string fullPath, ObservableCollection<object> children)
        {
            FullPath = fullPath;
            Name = (new System.IO.DirectoryInfo(fullPath)).Name;
            FileSystemService = null;
            this.Children = new ObservableCollection<object>();
        }

        /// <summary>
        /// Initializes new instance of <see cref="Directory"/> class. Children aren't loaded on runtime and are specified as argument in constructor.
        /// Path of the directory is null.
        /// </summary>
        /// <param name="children">Specified children files and directories</param>
        /// <param name="name">Name of the directory</param>
        public Directory(ObservableCollection<object> children, string name)
        {
            FullPath = null;
            FileSystemService = null;
            Name = name;
            this.Children = children;
        }

        public bool IsExpanded
        {
            get
            {
                return _isExpanded;
            }

            set
            {
                _isExpanded = value;
                LoadChildrenDirectories();
            }
        }

        private async Task LoadChildrenDirectories()
        {
            foreach (Directory c in Children)
            {
                var directories = await Task.Run(() => FileSystemService.GetAllDirectories(c.FullPath, System.IO.SearchOption.TopDirectoryOnly));

                if (directories != null)
                {
                    foreach (var t in directories)
                    {
                        c.Children.Add(t);
                    }
                }
            }
        }
    }
}
