using PictureLibraryModel.Model.Builders.ImageFileBuilder;
using PictureLibraryModel.Services.FileSystemServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Services.ImageProviderService
{
    public class ImageProviderStrategyFactory : IImageProviderStrategyFactory
    {
        private IFileService FileService { get; }
        private IImageFileBuilder ImageFileBuilder { get; }

        public ImageProviderStrategyFactory(IFileService fileService, IImageFileBuilder imageFileBuilder)
        {
            FileService = fileService;
            ImageFileBuilder = imageFileBuilder;
        }

        public IImageProviderStrategy GetLocalImageProviderStrategy()
        {
            return new LocalImageProviderStrategy(FileService, ImageFileBuilder);
        }
    }
}
