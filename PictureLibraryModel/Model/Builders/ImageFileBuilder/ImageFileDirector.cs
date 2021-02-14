using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Model.Builders
{
    public class ImageFileDirector : IImageFileDirector
    {
        private IImageFileBuilder Builder { get; }

        public ImageFileDirector(IImageFileBuilder builder)
        {
            Builder = builder;
        }

        public void MakeImageFile()
        {
            Builder.BuildCreationTime();
            Builder.BuildExtension();
            Builder.BuildFullPath();
            Builder.BuildIconSource();
            Builder.BuildLastAccessTime();
            Builder.BuildLastWriteTime();
            Builder.BuildLibraryFullPath();
            Builder.BuildName();
            Builder.BuildOrigin();
            Builder.BuildSize();
            Builder.BuildTags();
        }

        public ImageFile GetImageFile()
        {
            return Builder.ImageFile;
        }
    }
}
