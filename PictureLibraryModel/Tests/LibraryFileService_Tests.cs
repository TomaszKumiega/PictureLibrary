using Moq;
using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PictureLibraryModel.Tests
{
    public class LibraryFileService_Tests
    {
        [Fact]
        void FindLibraries_ShouldReturnLibraries_WhenArgumentIsADrive()
        {
            var mock = new Mock<Drive>();
            
        }
    }
}
