using System;

namespace PictureLibraryModel.Model
{
    public class APIUser
    {
        public Guid Id { get; set; }
        public required string Username { get; set; }
        public required string EmailAddress { get; set; }
        public required string Role { get; set; }
    }
}
