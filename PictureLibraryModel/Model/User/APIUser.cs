using System;

namespace PictureLibraryModel.Model
{
    public class APIUser : IRemoteEntity
    {
        public Guid Id { get; set; }
        public Guid DataStoreInfoId { get; set; }
        public required string Username { get; set; }
        public required string EmailAddress { get; set; }
        public required string Role { get; set; }
    }
}
