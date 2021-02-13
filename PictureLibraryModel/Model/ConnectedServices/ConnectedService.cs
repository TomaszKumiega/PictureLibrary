using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Model.ConnectedServices
{
    public class ConnectedService
    {
        public ConnectedServiceType Type { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
