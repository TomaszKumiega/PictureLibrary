using NLog;
using PictureLibraryModel.Services.FileSystemServices;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace PictureLibraryModel.Model
{
    public abstract class Directory : IFileSystemEntity
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        protected IFileProvider FileProvider { get; set; }

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

        public virtual void Expand()
        {
            foreach(var t in SubDirectories)
            {
                t.LoadSubDirectories();
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
