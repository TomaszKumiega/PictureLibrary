using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryViewModel.Helpers
{
    public class ExplorerHistory : IExplorerHistory
    {
        public Stack<IExplorableElement> BackStack { get; set; }
        public Stack<IExplorableElement> ForwardStack { get; set; }

        public ExplorerHistory()
        {
            BackStack = new Stack<IExplorableElement>();
            ForwardStack = new Stack<IExplorableElement>();
        }
    }
}
