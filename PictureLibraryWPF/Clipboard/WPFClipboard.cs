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
            throw new NotImplementedException();
        }

        public bool ContainsText()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
