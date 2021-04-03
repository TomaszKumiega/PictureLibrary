using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Repositories.LibraryRepositories
{
    public interface ILibraryRepositoryContext : IRepository<Library>
    {
        ILibraryRepositoryStrategy Strategy { get; set; }
    }
}
