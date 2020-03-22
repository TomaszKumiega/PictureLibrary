using PictureLibraryModel.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryModel.Model
{
    public class Drive
    {
        public string Name { get; }
        public bool IsReady { get; set; }
        private bool _isExpanded;
        public ObservableCollection<object> Children { get; set; }
        private IFileSystemService FileSystemService { get; }

        public Drive(string name, bool isReady, IFileSystemService fileSystemService)
        {
            Name = name;
            IsReady = isReady;
            this.Children = new ObservableCollection<object>();
            this.FileSystemService = fileSystemService;
            Initialize();
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

        private async Task Initialize()
        {


            var directories = await Task.Run(() => FileSystemService.GetAllDirectories(Name, System.IO.SearchOption.TopDirectoryOnly));

            if (directories != null)
            {
                foreach (var t in directories)
                {
                    Children.Add(t);
                }

                if (Children.Count == 0) IsReady = false; // When drive has no children, item will be disabled in treeview
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
