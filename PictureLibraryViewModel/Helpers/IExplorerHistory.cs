using PictureLibraryModel.Model;
using System.Collections.Generic;

namespace PictureLibraryViewModel.Helpers
{
    public interface IExplorerHistory
    {
        Stack<IExplorableElement> BackStack { get; set; }
        Stack<IExplorableElement> ForwardStack { get; set; }
    }
}
