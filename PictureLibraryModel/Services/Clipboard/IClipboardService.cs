using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Services.Clipboard
{
    public interface IClipboardService
    {
        string SystemClipboard { get; set; }

        IExplorableElement CopiedElement { get; set; }
        IExplorableElement CutElement { get; set; }
    }
}
