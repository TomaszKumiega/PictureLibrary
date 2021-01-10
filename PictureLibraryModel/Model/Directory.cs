﻿using PictureLibraryModel.Services;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace PictureLibraryModel.Model
{
    public abstract class Directory : IFileSystemEntity
    {
        protected FileSystemService FileSystemService { get; set; }

        public string FullPath { get; set; }
        public string Name { get; set; }
        public Bitmap Icon { get; protected set; }
        public ObservableCollection<Directory> SubDirectories { get; protected set; }
        public Origin Origin { get; set; }

        public Directory(string path, string name, FileSystemService fileSystemService, Origin origin)
        {
            FullPath = path;
            Name = name;
            FileSystemService = fileSystemService;
            Origin = origin;
            Icon = new Bitmap("Icons/FolderIcon.png");
            SubDirectories = new ObservableCollection<Directory>();
            LoadSubDirectories();
        }

        protected void LoadSubDirectories()
        {
            SubDirectories.Clear();

            var directories = FileSystemService.GetDirectories(FullPath, SearchOption.TopDirectoryOnly);

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
