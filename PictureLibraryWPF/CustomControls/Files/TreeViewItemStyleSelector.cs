using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace PictureLibraryWPF.CustomControls.Files
{
    public class TreeViewItemStyleSelector : StyleSelector 
    {
        public Style DirectoryStyle { get; set; }
        public Style FileStyle { get; set; }
        public Style DriveStyle { get; set; }

        public override System.Windows.Style SelectStyle(object item, System.Windows.DependencyObject container)
        {
            if (item is Drive) return this.DriveStyle;
            else if(item is Directory) return this.DirectoryStyle;
            else if (item is ImageFile) return this.FileStyle;
            return base.SelectStyle(item, container);
        }


    }
}
