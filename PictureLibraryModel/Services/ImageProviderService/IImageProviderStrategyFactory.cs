using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Services.ImageProviderService
{
    public interface IImageProviderStrategyFactory
    {
        IImageProviderStrategy GetLocalImageProviderStrategy();
    }
}
