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
    public abstract class Directory : IFileSystemEntity, INotifyPropertyChanged
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

        public virtual async Task Expand()
        {
            foreach(var t in SubDirectories)
            {
               await Task.Run(() => t.LoadSubDirectories());
            }
        }

        public virtual async Task Collapse()
        {
            foreach(var t in SubDirectories)
            {
                await Task.Run(() => t.SubDirectories.Clear());
            }
        }

        private void InitializeIcon()
        {
            try
            {
                Icon = new Bitmap("Icons/FolderIcon.png");
            }
            catch(Exception e)
            {
                _logger.Error(e, "Couldn't load the folder icon");
            }
        }

        public virtual void LoadSubDirectories()
        {
            SubDirectories.Clear();

            var directories = FileProvider.GetSubFolders(FullPath, SearchOption.TopDirectoryOnly);

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
