using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Services.Clipboard
{
    public class MoblieDeviceClipboardService : IClipboardService
    {
        public string SystemClipboard
        {
            get => Xamarin.Essentials.Clipboard.GetTextAsync().Result;
            set => Xamarin.Essentials.Clipboard.SetTextAsync(value).Wait();
        }

        public IExplorableElement CopiedElement { get; set; }
        public IExplorableElement CutElement { get; set; }
    }
}
