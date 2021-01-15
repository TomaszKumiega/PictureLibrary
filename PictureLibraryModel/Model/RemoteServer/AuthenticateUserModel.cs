using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PictureLibraryModel.Model.RemoteServerServe
{
    public class AuthenticateUserModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public AuthenticateUserModel()
        {

        }

        public AuthenticateUserModel(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
