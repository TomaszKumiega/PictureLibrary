using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Repositories.LibraryRepositories
{
    public interface ILibraryRepositoriesFactory
    {
        ILibraryRepository GetLocalLibraryRepository();
    }
}
