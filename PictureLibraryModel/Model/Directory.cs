using PictureLibraryModel.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
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

        public Directory(string fullPath, string name, IFileSystemService fileSystemService)
        {
            FullPath = fullPath;
            Name = name;
            FileSystemService = fileSystemService;
            this.Children = new ObservableCollection<object>();
            Initialize();
        }

        private void Initialize()
        {
            //TODO change to dynamic loading
            ObservableCollection<Directory> directories = FileSystemService.GetAllDirectories(FullPath, System.IO.SearchOption.TopDirectoryOnly).Result;

            if (directories != null)
            {
                foreach (var t in directories)
                {
                    Children.Add(t);
                }

                if (Children.Count == 0) IsReady = false; // When drive has no children, item will be disabled in treeview
            }
            
        }
    }
}
