using Google.Apis.Auth.OAuth2;
using PictureLibraryModel.Services.FileSystemServices;
using System.IO;
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
            var credentialsFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\google_drive_api_secret.json";
            Stream fileStream = _fileService.OpenFile(credentialsFilePath, FileMode.Open);

            return GoogleClientSecrets.FromStream(fileStream).Secrets;
        }
    }
}
