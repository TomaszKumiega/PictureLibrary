using System;
using System.Collections.Generic;

namespace PictureLibraryModel.DataProviders.Queries
{
    public interface IQueryBuilder<TBuilderInterface, TEntity> where TBuilderInterface : class
    {
        TBuilderInterface FromAllDataSources();
        TBuilderInterface FromDataSources(params Guid?[] sources);
        TBuilderInterface GetAll();
        TBuilderInterface WithId(Guid id);
        List<TEntity> ToList();
    }
}