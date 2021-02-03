using PictureLibraryModel.Model;
using PictureLibraryModel.Services.Clipboard;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryWPF.Clipboard
{
    public class WPFClipboard : IClipboardService
    {
        public ClipboardFilesState FilesState { get; private set; }

        public event EventHandler ClipboardContentChanged;

        public void Clear()
        {
            System.Windows.Clipboard.Clear();
        }

        public bool ContainsFiles()
        {
            return System.Windows.Clipboard.ContainsFileDropList();
        }

        public bool ContainsImage()
        {
            return System.Windows.Clipboard.ContainsImage();
        }

        public bool ContainsText()
        {
            return System.Windows.Clipboard.ContainsText();
        }

        public IEnumerable<string> GetFiles()
        {
            throw new NotImplementedException();
        }

        public ImageFile GetImage()
        {
            throw new NotImplementedException();
        }

        public string GetText()
        {
            return System.Windows.Clipboard.GetText();
        }

        public void SetFiles(IEnumerable<string> paths, ClipboardFilesState filesState)
        {
            throw new NotImplementedException();
        }

        public void SetImage(ImageFile image)
        {
            throw new NotImplementedException();
        }

        public void SetText(string text)
        {
            throw new NotImplementedException();
        }
    }
}
