using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PictureLibraryModel.Model
{
    public class Drive
    {
        public string Name { get; }
        public bool IsReady { get; }
        public ObservableCollection<object> Children { get; }

        public Drive(string name, bool isReady)
        {
            Name = name;
            IsReady = isReady;
            this.Children = new ObservableCollection<object>();
        }

    }
}
