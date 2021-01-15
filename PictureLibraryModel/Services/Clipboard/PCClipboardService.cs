using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Services.Clipboard
{
    public class PCClipboardService : IClipboardService
    {
        public PCClipboardService()
        {
            CopiedElements = new List<IExplorableElement>();
            CutElements = new List<IExplorableElement>();
        }

        public string SystemClipboard
        {
            get => TextCopy.ClipboardService.GetText(); 
            set => TextCopy.ClipboardService.SetText(value);
        }

        public List<IExplorableElement> CopiedElements { get; set; }
        public List<IExplorableElement> CutElements { get; set; }
    }
}
