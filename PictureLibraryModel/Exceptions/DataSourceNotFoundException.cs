using System;

namespace PictureLibraryModel.Exceptions
{
    public class DataSourceNotFoundException : Exception
    {
        private readonly Guid? _remoteStorageId;

        public DataSourceNotFoundException(Guid? remoteStorageId)
        {
            _remoteStorageId = remoteStorageId;
        }

        public override string Message 
            => $"Data source with id {_remoteStorageId} was not found.";
    }
}
