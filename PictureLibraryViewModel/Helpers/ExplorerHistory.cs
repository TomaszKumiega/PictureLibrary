using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryViewModel.Helpers
{
    public class ExplorerHistory : IExplorerHistory
    {
        public Stack<string> BackStack { get; set; }
        public Stack<string> ForwardStack { get; set; }

        public ExplorerHistory()
        {
            BackStack = new Stack<string>();
            ForwardStack = new Stack<string>();
        }
    }
}
