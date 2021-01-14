using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Services.Clipboard
{
    public class PCClipboardService : IClipboardService
    {
        public string SystemClipboard
        {
            get => TextCopy.ClipboardService.GetText(); 
            set => TextCopy.ClipboardService.SetText(value);
        }
        public IExplorableElement CopiedElement { get; set; }
        public IExplorableElement CutElement { get; set; }
    }
}
