using Google.Apis.Auth.OAuth2;
using PictureLibrary.FileSystem.API;
using System.Reflection;

namespace PictureLibraryModel.Services.CredentialsProvider
{
    internal class CredentialsProvider : ICredentialsProvider
    {
        private readonly IFileService _fileService;

        public CredentialsProvider(IFileService fileService)
        {
            _fileService = fileService;
        }

        public ClientSecrets GetGoogleDriveAPIClientSecrets()
        {
            var credentialsFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + "google_drive_api_secret.json";
            Stream fileStream = _fileService.Open(credentialsFilePath);

            return GoogleClientSecrets.FromStream(fileStream).Secrets;
        }
    }
}
