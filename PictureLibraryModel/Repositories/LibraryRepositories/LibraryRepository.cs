using PictureLibraryModel.Model;
using PictureLibraryModel.Services.ConnectedServicesInfoProvider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryModel.Repositories.LibraryRepositories
{
    public class LibraryRepository : IRepository<Library>
    {
        private IConnectedServicesInfoProviderService ConnectedServices { get; }
        private ILibraryRepositoryStrategyFactory RepositoryStrategyFactory { get; }
        private ILibraryRepositoryContext Context { get; }

        public LibraryRepository(IConnectedServicesInfoProviderService connectedServices, ILibraryRepositoryStrategyFactory repositoryStrategyFactory, ILibraryRepositoryContext libraryRepositoryContext)
        {
            ConnectedServices = connectedServices;
            RepositoryStrategyFactory = repositoryStrategyFactory;
            Context = libraryRepositoryContext;
        }

        public async Task AddAsync(Library entity)
        {
            switch(entity.Origin)
            {
                case Origin.Local:
                    {
                        Context.Strategy = RepositoryStrategyFactory.GetLocalLibraryRepository();
                    }
                    break;
            }

            await Context.AddAsync(entity);
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
                        Context.Strategy = RepositoryStrategyFactory.GetLocalLibraryRepository();   
                    }
                    break;
            }

            return await Context.FindAsync(predicate);
        }

        public async Task<IEnumerable<Library>> GetAllAsync()
        {
            var libraries = new List<Library>();

            Context.Strategy = RepositoryStrategyFactory.GetLocalLibraryRepository();
            libraries.AddRange(await Context.GetAllAsync());

            return libraries;
        }

        public async Task RemoveAsync(Library entity)
        {
            switch (entity.Origin)
            {
                case Origin.Local:
                    {
                        Context.Strategy = RepositoryStrategyFactory.GetLocalLibraryRepository();
                        await Context.RemoveAsync(entity);
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
                        Context.Strategy = RepositoryStrategyFactory.GetLocalLibraryRepository();
                        await Context.UpdateAsync(entity);
                    }
                    break;
            }
        }
    }
}
