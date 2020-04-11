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
        public async Task DrivesGet_ShouldReturnListOfDrivesAfterConstruction()
        {
            var mock = new Mock<IFileSystemService>();
            mock.Setup(x => x.GetDrives())
                .Returns(GetDrivesSample());

            var fileSystemVM = new FileSystemViewModel(mock.Object);
            Task.WaitAll(fileSystemVM.Initialize());

            Assert.True(fileSystemVM.Drives.Count == 5);
        }

        private ObservableCollection<Drive> GetDrivesSample()
        {
            var drives = new ObservableCollection<Drive>();

            drives.Add(new Drive("Drive1", new FileSystemService()));
            drives.Add(new Drive("Drive2", new FileSystemService()));
            drives.Add(new Drive("Drive3", new FileSystemService()));
            drives.Add(new Drive("Drive4", new FileSystemService()));
            drives.Add(new Drive("Drive5", new FileSystemService()));

            return drives;
        }
    }
}
