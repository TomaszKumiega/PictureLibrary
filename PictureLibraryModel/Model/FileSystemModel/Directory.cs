using ImageMagick;
using NLog;
using PictureLibraryModel.Services.FileSystemServices;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace PictureLibraryModel.Model
{
    public abstract class Directory : IFileSystemElement, INotifyPropertyChanged
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        protected IDirectoryService DirectoryService { get; set; }
        protected string IconPath => ".\\Icons\\FolderIcon.png";

        public event PropertyChangedEventHandler PropertyChanged;

        #region Public Properties
        public string Name { get; set; }
        public string Extension { get; set; }
        public string Path { get; set; }
        public MagickImage Icon { get; protected set; }
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
        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            
            set
            {
                _isExpanded = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsExpanded)));
            }
        }

        protected virtual async void OnIsExpandedChanged(object sender, PropertyChangedEventArgs args)
        {
            if(args.PropertyName == nameof(IsExpanded))
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
            var settings = new MagickReadSettings();
            settings.Width = 50;
            settings.Height = 50;
            
            Icon = new MagickImage(".\\Icons\\FolderIcon.png", settings);
        }

        public virtual async Task LoadSubDirectoriesAsync()
        {
            SubDirectories.Clear();

            try
            {
                var directories = await Task.Run(() => DirectoryService.GetSubFolders(Path));

                foreach (var t in directories)
                {
                    SubDirectories.Add(t);
                    t.LoadIcon();
                }
            }
            catch(Exception e)
            {
                _logger.Error(e, e.Message);
                throw new Exception("Application failed loading sub directories of: " + Path + " directory.");
            }
        }
    }
}
