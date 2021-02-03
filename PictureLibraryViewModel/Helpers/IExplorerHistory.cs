using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryViewModel.Helpers
{
    public interface IExplorerHistory
    {
        Stack<string> BackStack { get; }
        Stack<string> ForwardStack { get; }
    }
}
