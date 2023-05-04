using Google.Apis.Auth.OAuth2;

namespace PictureLibraryModel.Services.CredentialsProvider
{
    internal interface ICredentialsProvider
    {
        ClientSecrets GetGoogleDriveAPIClientSecrets();
    }
}
