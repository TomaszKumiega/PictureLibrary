using PictureLibraryModel.Model;
using PictureLibraryModel.Services.ConnectedServicesInfoProvider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryModel.Repositories.LibraryRepositories
{
    public class LibraryRepository : ILibraryRepository
    {
        private IConnectedServicesInfoProviderService ConnectedServices { get; }
        private ILibraryRepositoriesFactory RepositoriesFactory { get; }

        public LibraryRepository(IConnectedServicesInfoProviderService connectedServices, ILibraryRepositoriesFactory repositoriesFactory)
        {
            ConnectedServices = connectedServices;
            RepositoriesFactory = repositoriesFactory;
        }

        public async Task AddAsync(Library entity)
        {
            switch(entity.Origin)
            {
                case Origin.Local:
                    {
                        var repository = RepositoriesFactory.GetLocalLibraryRepository();
                        await repository.AddAsync(entity);
                    }
                    break;
            }
        }

        public async Task AddRangeAsync(IEnumerable<Library> entities)
        {
            foreach(var t in entities)
            {
                await AddAsync(t);
            }
        }

        public async Task<IEnumerable<Library>> FindAsync(Predicate<Library> predicate)
        {
            var entity = predicate.Target as Library;

            switch (entity.Origin)
            {
                case Origin.Local:
                    {
                        var repository = RepositoriesFactory.GetLocalLibraryRepository();
                        return await repository.FindAsync(predicate);
                    };
            }

            return null;
        }

        public async Task<IEnumerable<Library>> GetAllAsync()
        {
            var libraries = new List<Library>();

            var localRepository = RepositoriesFactory.GetLocalLibraryRepository();
            libraries.AddRange(await localRepository.GetAllAsync());

            return libraries;
        }

        public Task<Library> GetByPathAsync(string path)
        {
            throw new NotSupportedException();
        }

        public Task RemoveAsync(string path)
        {
            throw new NotSupportedException();
        }

        public async Task RemoveAsync(Library entity)
        {
            switch (entity.Origin)
            {
                case Origin.Local:
                    {
                        var repository = RepositoriesFactory.GetLocalLibraryRepository();
                        await repository.RemoveAsync(entity);
                    }
                    break;
            }
        }

        public async Task RemoveRangeAsync(IEnumerable<Library> entities)
        {
            foreach(var t in entities)
            {
                await RemoveAsync(t);
            }
        }

        public async Task UpdateAsync(Library entity)
        {
            switch (entity.Origin)
            {
                case Origin.Local:
                    {
                        var repository = RepositoriesFactory.GetLocalLibraryRepository();
                        await repository.UpdateAsync(entity);
                    }
                    break;
            }
        }
    }
}
