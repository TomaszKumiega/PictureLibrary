using PictureLibraryModel.Model;
using PictureLibraryModel.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PictureLibraryViewModel.ViewModels
{
    public class FileSystemViewModel : IFileSystemViewModel
    {
        public string CurrentDirectory { get; }

        public ObservableCollection<Drive> Drives { get; private set; }

        private IFileSystemService FileSystemService { get; }

        public FileSystemViewModel(IFileSystemService fileSystemService)
        {
            FileSystemService = fileSystemService;
            Initialize();
        }

        private void Initialize()
        {
            Drives = FileSystemService.GetDrives();
            foreach(Drive t in Drives[0].Children) // Drives[0] is MyComputer, Children items are the drives on the computer
            {
                var directories = FileSystemService.GetAllDirectories(t.Name, System.IO.SearchOption.TopDirectoryOnly);

                if (directories != null)
                {
                    foreach (var i in directories)
                    {
                        t.Children.Add(i);
                    }

                    if (t.Children.Count == 0) t.IsReady = false;
                }
            }
        }
    }
}
