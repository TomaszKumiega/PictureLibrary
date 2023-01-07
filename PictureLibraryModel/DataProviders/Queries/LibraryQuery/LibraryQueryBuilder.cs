using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PictureLibraryModel.DataProviders.Queries
{
    public class LibraryQueryBuilder : QueryBuilder<ILibraryQueryBuilder, Library>, ILibraryQueryBuilder
    {
        public LibraryQueryBuilder(Func<StorageQuery> storageQuery) : base(storageQuery) 
        { 
        }

        public ILibraryQueryBuilder StartQuery(IDataSourceCollection dataSourceCollection)
        {
            _dataSourceCollection = dataSourceCollection;

            return this;
        }

        public override List<Library> ToList()
        {
            List<Library> libraries = new();

            foreach (var remoteStorageId in _query.DataSources)
            {
                IDataSource dataSource = _dataSourceCollection.GetDataSourceByRemoteStorageId(remoteStorageId);
                
                if (_query.GetAllData)
                {
                    libraries.AddRange(dataSource.LibraryProvider.GetAllLibraries().ToList());
                }
                else if (_query.Id.HasValue)
                {
                    libraries.Add(dataSource.LibraryProvider.FindLibrary(x => x.Id == _query.Id));
                }
            }

            return libraries;
        }
    }
}
