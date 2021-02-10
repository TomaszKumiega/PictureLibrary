using PictureLibraryModel.Model.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Repositories.DatabaseRepositories
{
    public class UserRepository : DatabaseRepository<User>
    {
        public UserRepository() : base("Users")
        {

        }
    }
}
