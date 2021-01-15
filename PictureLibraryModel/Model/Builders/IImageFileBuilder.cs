using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Model.Builders
{
    public interface IImageFileBuilder
    {
        void BuildName();
        void BuildExtension();
        void BuildFullPath();
        void BuildLibraryFullPath();
        void BuildCreationTime();
        void BuildLastAccessTime();
        void BuildLastWriteTime();
        void BuildSize();
        void BuildTags();
        void BuildIconSource();
        void BuildOrigin();
    }
}
