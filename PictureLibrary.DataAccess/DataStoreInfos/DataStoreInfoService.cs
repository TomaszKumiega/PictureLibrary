using Newtonsoft.Json;
using PictureLibrary.DataAccess.DataStoreInfos;
using PictureLibrary.FileSystem.API;
using PictureLibrary.Tools.StringEncryption;
using PictureLibraryModel.Model.DataStoreInfo;

namespace PictureLibrary.DataAccess
{
    public class DataStoreInfoService : IDataStoreInfoService
    {
        private Dictionary<Type, string?> _encryptedFileContents;

        private readonly IPathFinder _pathFinder;
        private readonly IFileService _fileService;
        private readonly IStringEncryptionService _stringEncryptionService;

        public DataStoreInfoService(
            IPathFinder pathFinder,
            IFileService fileService,
            IStringEncryptionService stringEncryptionService)
        {
            _pathFinder = pathFinder;
            _fileService = fileService;
            _stringEncryptionService = stringEncryptionService;
            _encryptedFileContents = new();
        }

        public TDataStoreInfo? GetDataStoreInfo<TDataStoreInfo>(Guid id)
            where TDataStoreInfo : class, IDataStoreInfo
        {
            string fileContent = GetFileContent(typeof(TDataStoreInfo));
            IEnumerable<TDataStoreInfo> dataStoreInfos = GetAllDataStoreInfosOfType<TDataStoreInfo>(fileContent);

            return dataStoreInfos.FirstOrDefault(x => x.Id == id);
        }

        private string GetFileContent(Type typeOfDataStoreInfo)
        {
            if (!_encryptedFileContents.TryGetValue(typeOfDataStoreInfo, out string? encryptedFileContent) 
                || string.IsNullOrEmpty(encryptedFileContent))
            {
                var filePath = _pathFinder.GetDataStoreInfoFilePath(typeOfDataStoreInfo);

                using var stream = _fileService.Open(filePath);
                using var streamReader = new StreamReader(stream);

                encryptedFileContent = streamReader.ReadToEnd();
                _encryptedFileContents.Add(typeOfDataStoreInfo, encryptedFileContent);
            }

            return _stringEncryptionService.Decrypt(encryptedFileContent);
        }

        private IEnumerable<TDataStoreInfo> GetAllDataStoreInfosOfType<TDataStoreInfo>(string decryptedFileContent)
            where TDataStoreInfo : class, IDataStoreInfo
        {
            string[] encryptedStoreInfos = decryptedFileContent.Split(';');

            foreach (var encryptedStoreInfo in encryptedStoreInfos)
            {
                var dataStoreInfo = JsonConvert.DeserializeObject<TDataStoreInfo?>(encryptedStoreInfo);

                if (dataStoreInfo != null)
                    yield return dataStoreInfo;
            }
        }
    }
}
