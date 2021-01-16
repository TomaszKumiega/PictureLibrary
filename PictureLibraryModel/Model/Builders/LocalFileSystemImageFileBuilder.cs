using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SystemInterface.IO;

namespace PictureLibraryModel.Model.Builders
{
    public class LocalFileSystemImageFileBuilder : IImageFileBuilder
    {
        private IFileInfo _fileInfo;
        public ImageFile ImageFile { get; private set; }

        public LocalFileSystemImageFileBuilder(IFileInfo fileInfo)
        {
            _fileInfo = fileInfo;
        }

        public void BuildCreationTime()
        {
            ImageFile.CreationTime = _fileInfo.FileInfoInstance.CreationTimeUtc;
        }

        public void BuildExtension()
        {
            ImageFile.Extension = ImageExtensionHelper.GetExtension(_fileInfo.FileInfoInstance.Extension);
        }

        public void BuildFullPath()
        {
            ImageFile.FullPath = _fileInfo.FileInfoInstance.FullName;
        }

        public void BuildIconSource()
        {
            ImageFile.IconSource = _fileInfo.FileInfoInstance.FullName;
        }

        public void BuildLastAccessTime()
        {
            ImageFile.LastAccessTime = _fileInfo.FileInfoInstance.LastAccessTimeUtc;
        }

        public void BuildLastWriteTime()
        {
            ImageFile.LastWriteTime = _fileInfo.FileInfoInstance.LastWriteTimeUtc;
        }

        public void BuildLibraryFullPath()
        {
            ImageFile.LibraryFullPath = null;
        }

        public void BuildName()
        {
            ImageFile.Name = _fileInfo.FileInfoInstance.Name;
        }

        public void BuildOrigin()
        {
            ImageFile.Origin = Origin.Local;
        }

        public void BuildSize()
        {
            ImageFile.Size = _fileInfo.FileInfoInstance.Length;
        }

        public void BuildTags()
        {
            ImageFile.Tags = null;
        }
    }
}
