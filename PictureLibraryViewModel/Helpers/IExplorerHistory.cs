using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryViewModel.Helpers
{
    public interface IExplorerHistory
    {
        Stack<IExplorableElement> BackStack { get; set; }
        Stack<IExplorableElement> ForwardStack { get; set; }
    }
}
