using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PictureLibraryModel.Services.Clipboard
{
    public interface IClipboardService
    {
        string SystemClipboard { get; set; }

        ObservableCollection<IExplorableElement> CopiedElements { get; set; }
        ObservableCollection<IExplorableElement> CutElements { get; set; }
    }
}
