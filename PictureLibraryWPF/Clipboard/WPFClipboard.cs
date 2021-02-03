using PictureLibraryModel.Model;
using PictureLibraryModel.Services.Clipboard;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace PictureLibraryWPF.Clipboard
{
    public class WPFClipboard : IClipboardService
    {
        public ClipboardFilesState FilesState { get; private set; }

        public event EventHandler ClipboardContentChanged;

        public void Clear()
        {
            FilesState = ClipboardFilesState.NoFiles;
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
            var filePaths = new List<string>();

            foreach (var t in System.Windows.Clipboard.GetFileDropList()) filePaths.Add(t);

            return filePaths;
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
            Clear();

            FilesState = filesState;

            var collection = new StringCollection();

            foreach (var t in paths) collection.Add(t);

            System.Windows.Clipboard.SetFileDropList(collection);

            ClipboardContentChanged?.Invoke(this, EventArgs.Empty);
        }

        public void SetImage(ImageFile image)
        {
            throw new NotImplementedException();
        }

        public void SetText(string text)
        {
            Clear();

            System.Windows.Clipboard.SetText(text);

            ClipboardContentChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
