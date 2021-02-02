using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryModel.Repositories
{
    public interface ILibraryRepository : IRepository<Library>
    {
        Task<Library> GetByPathAsync(string path);
        Task RemoveAsync(string path);
    } 
}
