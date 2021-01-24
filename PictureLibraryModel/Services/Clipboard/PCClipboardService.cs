using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PictureLibraryModel.Services.Clipboard
{
    public class PCClipboardService : IClipboardService
    {
        public PCClipboardService()
        {
            CopiedElements = new ObservableCollection<IExplorableElement>();
            CutElements = new ObservableCollection<IExplorableElement>();
        }

        public string SystemClipboard
        {
            get => TextCopy.ClipboardService.GetText(); 
            set => TextCopy.ClipboardService.SetText(value);
        }

        public ObservableCollection<IExplorableElement> CopiedElements { get; set; }
        public ObservableCollection<IExplorableElement> CutElements { get; set; }
    }
}
