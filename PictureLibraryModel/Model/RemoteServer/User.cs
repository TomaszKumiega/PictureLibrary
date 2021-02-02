using PictureLibraryModel.Model.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Model.RemoteServer
{
    public class User : IDatabaseEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
