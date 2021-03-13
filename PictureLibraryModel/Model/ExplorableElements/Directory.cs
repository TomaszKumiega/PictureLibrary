using NLog;
using PictureLibraryModel.Services.FileSystemServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace PictureLibraryModel.Model
{
    public abstract class Directory : IExplorableElement, INotifyPropertyChanged
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private bool _isExpanded;

        protected IDirectoryService DirectoryService { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public string FullName { get; set; }
        public string Name { get; set; }
        public string IconSource { get; protected set; }
        public ObservableCollection<Directory> SubDirectories { get; protected set; }
        public Origin Origin { get; set; }

        public Directory()
        {
            IconSource = "pack://application:,,,/Icons/FolderIcon.png";
            SubDirectories = new ObservableCollection<Directory>();

            PropertyChanged += OnIsExpandedChanged;
        }

        public Directory(IDirectoryService directoryService)
        {
            IconSource = "pack://application:,,,/Icons/FolderIcon.png";
            SubDirectories = new ObservableCollection<Directory>();
            DirectoryService = directoryService;

            PropertyChanged += OnIsExpandedChanged;
        }

        public Directory(string path, string name, IDirectoryService directoryService, Origin origin)
        {
            FullName = path;
            Name = name;
            DirectoryService = directoryService;
            Origin = origin;
            SubDirectories = new ObservableCollection<Directory>();

            PropertyChanged += OnIsExpandedChanged;

            IconSource = "pack://application:,,,/Icons/FolderIcon.png";
        }

        public bool IsExpanded
        {
            get => _isExpanded;
            
            set
            {
                _isExpanded = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsExpanded"));
            }
        }

        protected virtual async void OnIsExpandedChanged(object sender, PropertyChangedEventArgs args)
        {
            if(args.PropertyName == "IsExpanded")
            {
                if (IsExpanded) await Expand();
                else Collapse();
            }
        }

        protected virtual async Task Expand()
        {
            foreach(var t in SubDirectories)
            {
                await t.LoadSubDirectoriesAsync();
            }
        }

        protected virtual void Collapse()
        {
            foreach(var t in SubDirectories)
            {
                t.SubDirectories.Clear();
            }
        }

        public virtual async Task LoadSubDirectoriesAsync()
        {
            SubDirectories.Clear();

            IEnumerable<Folder> directories = new List<Folder>();

            try
            {
                directories = await Task.Run(() => DirectoryService.GetSubFolders(FullName));
            }
            catch(Exception e)
            {
                _logger.Error(e, e.Message);
                throw new Exception("Application failed loading sub directories of: " + this.FullName + " directory.");
            }

            foreach (var t in directories)
            {
                SubDirectories.Add(t);
            }
        }
    }
}
