using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            CopiedElements = new ObservableCollection<IExplorableElement>();
            CutElements = new ObservableCollection<IExplorableElement>();
        }

        public ObservableCollection<IExplorableElement> CopiedElements { get; set; }
        public ObservableCollection<IExplorableElement> CutElements { get; set; }
    }
}
