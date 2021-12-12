using NLog;
using PictureLibraryModel.Services.FileSystemServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;

namespace PictureLibraryModel.Model
{
    public abstract class Directory : IFileSystemElement, INotifyPropertyChanged
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private bool _isExpanded;

        protected IDirectoryService DirectoryService { get; set; }
        protected string IconPath => ".\\Icon\\FolderIcon.png";

        public event PropertyChangedEventHandler PropertyChanged;

        #region Public Properties
        public string Name { get; set; }
        public string Extension { get; set; }
        public string Path { get; set; }
        public Icon Icon { get; protected set; }
        public ObservableCollection<Directory> SubDirectories { get; protected set; }
        #endregion

        #region Constructors
        public Directory()
        {
            SubDirectories = new ObservableCollection<Directory>();

            PropertyChanged += OnIsExpandedChanged;
        }

        public Directory(IDirectoryService directoryService)
        {
            SubDirectories = new ObservableCollection<Directory>();
            DirectoryService = directoryService;

            PropertyChanged += OnIsExpandedChanged;
        }

        public Directory(string path, string name, IDirectoryService directoryService)
        {
            Path = path;
            Name = name;
            DirectoryService = directoryService;
            SubDirectories = new ObservableCollection<Directory>();

            PropertyChanged += OnIsExpandedChanged;
        }
        #endregion

        #region Expanding and collapsing
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
        #endregion

        public virtual void LoadIcon()
        {
            Icon = Icon.ExtractAssociatedIcon(IconPath);
        }

        public virtual async Task LoadSubDirectoriesAsync()
        {
            SubDirectories.Clear();

            IEnumerable<Folder> directories = new List<Folder>();

            try
            {
                directories = await Task.Run(() => DirectoryService.GetSubFolders(Path));
            }
            catch(Exception e)
            {
                _logger.Error(e, e.Message);
                throw new Exception("Application failed loading sub directories of: " + Path + " directory.");
            }

            foreach (var t in directories)
            {
                SubDirectories.Add(t);
            }
        }
    }
}
