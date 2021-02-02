using PictureLibraryModel.Model.RemoteServer;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Repositories
{
    public class UserRepository : DatabaseRepository<User>
    {
        public UserRepository() : base("Users")
        {

        }
    }
}
