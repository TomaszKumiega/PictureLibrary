using PictureLibraryModel.Model.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.ConnectedServices
{
    public class ConnectedService : IDatabaseEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ConnectedServiceType Type { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
