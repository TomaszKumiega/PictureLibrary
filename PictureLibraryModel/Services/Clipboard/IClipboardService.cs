using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PictureLibraryModel.Services.Clipboard
{
    public enum ClipboardFilesState
    {
        Copied,
        Cut,
        NoFiles
    }

    public interface IClipboardService
    {
        ClipboardFilesState FilesState { get; }
        event EventHandler ClipboardContentChanged;
        bool ContainsText();
        bool ContainsImage();
        bool ContainsFiles();
        void Clear();
        string GetText();
        void SetText(string text);
        void SetImage(ImageFile image);
        ImageFile GetImage();
        void SetFiles(IEnumerable<string> paths, ClipboardFilesState filesState);
        IEnumerable<string> GetFiles();
    }
}
