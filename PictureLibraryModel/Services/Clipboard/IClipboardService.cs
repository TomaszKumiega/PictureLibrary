using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Services.Clipboard
{
    public interface IClipboardService
    {
        string SystemClipboard { get; set; }

        List<IExplorableElement> CopiedElements { get; set; }
        List<IExplorableElement> CutElements { get; set; }
    }
}
