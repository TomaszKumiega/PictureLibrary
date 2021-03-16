using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PictureLibraryModel.Model.Builders
{
    public class LocalFileSystemImageFileBuilder : IImageFileBuilder
    {
        private FileInfo FileInfo { get; }
        public ImageFile ImageFile { get; private set; }

        public LocalFileSystemImageFileBuilder(FileInfo fileInfo)
        {
            ImageFile = new ImageFile();
            FileInfo = fileInfo;
        }

        public void BuildCreationTime()
        {
            ImageFile.CreationTime = FileInfo.CreationTimeUtc;
        }

        public void BuildExtension()
        {
            ImageFile.Extension = ImageExtensionHelper.GetExtension(FileInfo.Extension);
        }

        public void BuildFullPath()
        {
            ImageFile.FullName = FileInfo.FullName;
        }

        public void BuildIconSource()
        {
            ImageFile.IconSource = FileInfo.FullName;
        }

        public void BuildLastAccessTime()
        {
            ImageFile.LastAccessTime = FileInfo.LastAccessTimeUtc;
        }

        public void BuildLastWriteTime()
        {
            ImageFile.LastWriteTime = FileInfo.LastWriteTimeUtc;
        }

        public void BuildLibraryFullPath()
        {
            ImageFile.LibraryFullPath = null;
        }

        public void BuildName()
        {
            ImageFile.Name = FileInfo.Name;
        }

        public void BuildOrigin()
        {
            ImageFile.Origin = Origin.Local;
        }

        public void BuildSize()
        {
            ImageFile.Size = FileInfo.Length;
        }

        public void BuildTags()
        {
            ImageFile.Tags = null;
        }
    }
}
