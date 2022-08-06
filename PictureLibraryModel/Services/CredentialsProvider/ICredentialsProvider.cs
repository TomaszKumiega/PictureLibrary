using Google.Apis.Auth.OAuth2;
using System.IO;

namespace PictureLibraryModel.Services.CredentialsProvider
{
    public interface ICredentialsProvider
    {
        ClientSecrets GetGoogleDriveAPIClientSecrets();
    }
}
