using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryModel.Services.ImageProviderService
{
    public class ImageProviderService : IImageProviderService
    {
        private IImageProviderContext Context { get; }
        private IImageProviderStrategyFactory StrategyFactory { get; }

        public ImageProviderService(IImageProviderContext context, IImageProviderStrategyFactory strategyFactory)
        {
            Context = context;
            StrategyFactory = strategyFactory;
        }

        public async Task<ImageFile> AddImageToLibraryAsync(ImageFile imageFile, string libraryFullName)
        {
            switch(imageFile.Origin)
            {
                case Origin.Local:
                    {
                        Context.Strategy = StrategyFactory.GetLocalImageProviderStrategy();
                        return await Context.AddImageToLibraryAsync(imageFile, libraryFullName);
                    }

                default:
                    {
                        return null;
                    }
            }
        }

        public async Task<byte[]> GetImageAsync(ImageFile imageFile)
        {
            switch(imageFile.Origin)
            {
                case Origin.Local:
                    {
                        Context.Strategy = StrategyFactory.GetLocalImageProviderStrategy();
                        return await Context.GetImageAsync(imageFile);
                    }

                default:
                    {
                        return null;
                    }
            }
        }

        public async Task LoadImagesIconsAsync(IEnumerable<ImageFile> imageFiles)
        {
            foreach(var t in imageFiles)
            {
                switch (t.Origin)
                {
                    case Origin.Local:
                        {
                            Context.Strategy = StrategyFactory.GetLocalImageProviderStrategy();
                            await Context.LoadImageIcon(t);
                        }
                        break;
                }
            }
        }

        public async Task RemoveImageAsync(ImageFile imageFile)
        {
            switch (imageFile.Origin)
            {
                case Origin.Local:
                    {
                        Context.Strategy = StrategyFactory.GetLocalImageProviderStrategy();
                        await Context.RemoveImageAsync(imageFile);
                    }
                    break;
            }
        }

        public async Task UpdateImageAsync(ImageFile imageFile, byte[] image)
        {
            switch (imageFile.Origin)
            {
                case Origin.Local:
                    {
                        Context.Strategy = StrategyFactory.GetLocalImageProviderStrategy();
                        await Context.UpdateImageAsync(imageFile, image);
                    }
                    break;
            }
        }
    }
}
