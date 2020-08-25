using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using Moq;
using PictureLibraryModel.Model;
using PictureLibraryModel.Services;
using PictureLibraryViewModel.ViewModels;
using Xunit;
using Directory = PictureLibraryModel.Model.Directory;

namespace PictureLibraryViewModel.Tests
{
    public class FileSystemViewModel_Tests
    {
        [Fact]
        public void DrivesGet_ShouldReturnListOfDrivesAfterConstruction()
        {
            var mock = new Mock<IFileSystemService>();
            mock.Setup(x => x.GetDrives())
                .Returns(GetDrivesSample());

            var fileSystemVM = new FileSystemViewModel(mock.Object);
            Task.WaitAll(fileSystemVM.Initialize());

            Assert.True(fileSystemVM.Drives.Count == 3);
        }

        [Fact]
        public void CurrentDirectoryContentGet_ShouldReturnListOfDirectoriesAndImageFiles_WhenCurrentDirectoryPathDoesNotEqualMyComputer()
        {
            string currDirectoryPath = "ff";
            var mock = new Mock<IFileSystemService>();
            mock.Setup(x => x.GetAllImageFiles(currDirectoryPath))
                .Returns(GetImageFilesSample());
            mock.Setup(x => x.GetAllDirectories(currDirectoryPath, SearchOption.TopDirectoryOnly))
                .Returns(GetDirectoriesSample());

            var fileSystemVM = new FileSystemViewModel(mock.Object);
            Task.WaitAll(fileSystemVM.Initialize());
            fileSystemVM.CurrentDirectoryPath = currDirectoryPath;


            Assert.True(fileSystemVM.CurrentDirectoryContent.Count == 10);
        }

        [Fact]
        public async Task CurrentDirectoryContentGet_ShouldReturnListOfDrives_WhenCurrentDirectoryPathEqualsMyComputer()
        {
            var mock = new Mock<IFileSystemService>();
            mock.Setup(x => x.GetDrives())
                .Returns(GetDrivesWithChildrenSample());

            var fileSystemVM = new FileSystemViewModel(mock.Object);
            Task.WaitAll(fileSystemVM.Initialize());
            fileSystemVM.CurrentDirectoryPath = "My Computer";


            Assert.True(fileSystemVM.CurrentDirectoryContent.Count == 3);
        }

        private ObservableCollection<Drive> GetDrivesSample()
        {
            var drives = new ObservableCollection<Drive>();

            drives.Add(new Drive("Drive", new FileSystemService(new FileSystemEntitiesFactory())));
            drives.Add(new Drive("Drive", new FileSystemService(new FileSystemEntitiesFactory())));
            drives.Add(new Drive("Drive", new FileSystemService(new FileSystemEntitiesFactory())));

            return drives;
        }

        private ObservableCollection<Drive> GetDrivesWithChildrenSample()
        {
            var drives = new ObservableCollection<Drive>();

            drives.Add(new Drive("Drive", new FileSystemService(new FileSystemEntitiesFactory())));
            drives[0].Children.Add(new Drive("Drive", new FileSystemService(new FileSystemEntitiesFactory())));
            drives[0].Children.Add(new Drive("Drive", new FileSystemService(new FileSystemEntitiesFactory())));
            drives[0].Children.Add(new Drive("Drive", new FileSystemService(new FileSystemEntitiesFactory())));

            return drives;
        }

        private List<ImageFile> GetImageFilesSample()
        {
            var imageFiles = new List<ImageFile>();
            
            imageFiles.Add(new ImageFile());
            imageFiles.Add(new ImageFile());
            imageFiles.Add(new ImageFile());
            imageFiles.Add(new ImageFile());
            imageFiles.Add(new ImageFile());

            return imageFiles;
        }

        private ObservableCollection<Directory> GetDirectoriesSample()
        {
            var directories = new ObservableCollection<Directory>();

            directories.Add(new Directory(null,""));
            directories.Add(new Directory(null, ""));
            directories.Add(new Directory(null, ""));
            directories.Add(new Directory(null, ""));
            directories.Add(new Directory(null, ""));

            return directories;
        }
    }
}
