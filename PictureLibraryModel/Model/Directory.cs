using NLog;
using PictureLibraryModel.Services.FileSystemServices;
using System;
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

        protected IFileProvider FileProvider { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public string FullPath { get; set; }
        public string Name { get; set; }
        public Bitmap Icon { get; protected set; }
        public ObservableCollection<Directory> SubDirectories { get; protected set; }
        public Origin Origin { get; set; }

        public Directory()
        {

        }

        public Directory(string path, string name, IFileProvider fileProvider, Origin origin)
        {
            FullPath = path;
            Name = name;
            FileProvider = fileProvider;
            Origin = origin;
            SubDirectories = new ObservableCollection<Directory>();

            PropertyChanged += OnIsExpandedChanged;

            InitializeIcon();
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

        public virtual async Task Expand()
        {
            foreach(var t in SubDirectories)
            {
                await t.LoadSubDirectoriesAsync();
            }
        }

        public virtual void Collapse()
        {
            foreach(var t in SubDirectories)
            {
                t.SubDirectories.Clear();
            }
        }

        private void InitializeIcon()
        {
            try
            {
                Icon = new Bitmap("Icons\\FolderIcon.png");
            }
            catch(Exception e)
            {
                _logger.Error(e, "Couldn't load the folder icon");
            }
        }

        public virtual async Task LoadSubDirectoriesAsync()
        {
            SubDirectories.Clear();

            var directories = await Task.Run(() => FileProvider.GetSubFolders(FullPath, SearchOption.TopDirectoryOnly));

            foreach (var t in directories)
            {
                SubDirectories.Add(t);
            }
        }

        ~Directory()
        {
            Icon?.Dispose();
        }
    }
}
