using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PictureLibraryModel.Model.Builders
{
    public class LocalFileSystemImageFileBuilder : IImageFileBuilder
    {
        private FileInfo _fileInfo;
        public ImageFile ImageFile { get; private set; }

        public LocalFileSystemImageFileBuilder(FileInfo fileInfo)
        {
            _fileInfo = fileInfo;
        }

        public void BuildCreationTime()
        {
            ImageFile.CreationTime = _fileInfo.CreationTimeUtc;
        }

        public void BuildExtension()
        {
            ImageFile.Extension = ImageExtensionHelper.GetExtension(_fileInfo.Extension);
        }

        public void BuildFullPath()
        {
            ImageFile.FullPath = _fileInfo.FullName;
        }

        public void BuildIconSource()
        {
            ImageFile.IconSource = _fileInfo.FullName;
        }

        public void BuildLastAccessTime()
        {
            ImageFile.LastAccessTime = _fileInfo.LastAccessTimeUtc;
        }

        public void BuildLastWriteTime()
        {
            ImageFile.LastWriteTime = _fileInfo.LastWriteTimeUtc;
        }

        public void BuildLibraryFullPath()
        {
            ImageFile.LibraryFullPath = null;
        }

        public void BuildName()
        {
            ImageFile.Name = _fileInfo.Name;
        }

        public void BuildOrigin()
        {
            ImageFile.Origin = Origin.Local;
        }

        public void BuildSize()
        {
            ImageFile.Size = _fileInfo.Length;
        }

        public void BuildTags()
        {
            ImageFile.Tags = null;
        }
    }
}
