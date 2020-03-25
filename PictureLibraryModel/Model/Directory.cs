using PictureLibraryModel.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PictureLibraryModel.Model
{
    public class Directory
    {
        public string FullPath { get; }
        public string Name { get; }
        public ObservableCollection<object> Children { get; }
        private IFileSystemService FileSystemService { get; }
        public bool IsReady { get; private set; }
        private bool _isExpanded;

        public Directory(string fullPath, string name, IFileSystemService fileSystemService)
        {
            FullPath = fullPath;
            Name = name;
            FileSystemService = fileSystemService;
            this.Children = new ObservableCollection<object>();
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
                LoadChildrenSubDirectories();
            }
        }

        private async Task LoadChildrenSubDirectories()
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

                if (c.Children.Count == 0) IsReady = false; // When drive has no children, item will be disabled in treeview
            }
        }
    }
}
