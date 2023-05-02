using Google.Apis.Auth.OAuth2;
using PictureLibrary.FileSystem.API;
using System.Reflection;

namespace PictureLibraryModel.Services.CredentialsProvider
{
    internal class CredentialsProvider : ICredentialsProvider
    {
        private readonly IPathFinder _pathFinder;
        private readonly IFileService _fileService;

        public CredentialsProvider(
            IPathFinder pathFinder,
            IFileService fileService)
        {
            _pathFinder = pathFinder;
            _fileService = fileService;
        }

        public ClientSecrets GetGoogleDriveAPIClientSecrets()
        {
            var credentialsFilePath = _pathFinder.AppFolderPath + Path.PathSeparator + "google_drive_api_secret.json";
            Stream fileStream = _fileService.Open(credentialsFilePath);

            return GoogleClientSecrets.FromStream(fileStream).Secrets;
        }
    }
}
