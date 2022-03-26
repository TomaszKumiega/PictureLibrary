using System;

namespace PictureLibraryViewModel.ViewModel.Events
{
    public class InvalidInputEventArgs : EventArgs
    {
        public string PropertyName { get; set; }

        public InvalidInputEventArgs(string propertyName)
        {
            PropertyName = propertyName;
        }
    }

    public delegate void InvalidInputEventHandler(object sender, InvalidInputEventArgs args);
}
