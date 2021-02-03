using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryViewModel.Helpers
{
    public class ExplorerHistory : IExplorerHistory
    {
        public Stack<string> BackStack { get; }
        public Stack<string> ForwardStack { get; }

        public ExplorerHistory()
        {
            BackStack = new Stack<string>();
            ForwardStack = new Stack<string>();
        }
    }
}
