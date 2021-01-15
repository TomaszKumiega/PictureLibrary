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

        public MoblieDeviceClipboardService()
        {
            CopiedElements = new List<IExplorableElement>();
            CutElements = new List<IExplorableElement>();
        }

        public List<IExplorableElement> CopiedElements { get; set; }
        public List<IExplorableElement> CutElements { get; set; }
    }
}
