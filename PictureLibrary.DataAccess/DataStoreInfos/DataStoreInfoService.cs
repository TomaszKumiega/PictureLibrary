using Newtonsoft.Json;
using PictureLibrary.DataAccess.DataStoreInfos;
using PictureLibrary.FileSystem.API;
using PictureLibrary.Tools.StringEncryption;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.DataStoreInfo;

namespace PictureLibrary.DataAccess
{
    public class DataStoreInfoService : IDataStoreInfoService
    {
        #region Private fields
        private readonly Dictionary<Type, string?> _encryptedFileContents;

        private readonly IPathFinder _pathFinder;
        private readonly IFileService _fileService;
        private readonly IStringEncryptionService _stringEncryptionService;
        #endregion

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

        #region Public methods
        public IEnumerable<IDataStoreInfo> GetAllDataStoreInfos()
        {
            var dataStoreInfos = new List<IDataStoreInfo>();

            dataStoreInfos.AddRange(GetAllDataStoreInfosOfType<ApiDataStoreInfo>());
            dataStoreInfos.AddRange(GetAllDataStoreInfosOfType<GoogleDriveDataStoreInfo>());

            return dataStoreInfos;
        }

        public IEnumerable<TDataStoreInfo> GetAllDataStoreInfosOfType<TDataStoreInfo>()
            where TDataStoreInfo : class, IDataStoreInfo
        {
            var fileContent = GetFileContent(typeof(TDataStoreInfo));

            return fileContent != null
                ? GetAllDataStoreInfosOfType<TDataStoreInfo>(fileContent)
                : Enumerable.Empty<TDataStoreInfo>();
        }

        public TDataStoreInfo? GetDataStoreInfo<TDataStoreInfo>(Guid id)
            where TDataStoreInfo : class, IDataStoreInfo
        {
            string? fileContent = GetFileContent(typeof(TDataStoreInfo));
            
            if (fileContent == null)
                return null;

            IEnumerable<TDataStoreInfo> dataStoreInfos = GetAllDataStoreInfosOfType<TDataStoreInfo>(fileContent);

            return dataStoreInfos.FirstOrDefault(x => x.Id == id);
        }

        public bool AddDataStoreInfo<TDataStoreInfo>(TDataStoreInfo dataStoreInfo)
            where TDataStoreInfo : class, IDataStoreInfo
        {
            string fileContent = AddDataStoreInfoToFileContent(dataStoreInfo);
            string encryptedFileContent = _stringEncryptionService.Encrypt(fileContent);

            UpdateEncryptetFileContents(typeof(TDataStoreInfo), encryptedFileContent);
            return SaveUpdatedDataStoreInfo(typeof(TDataStoreInfo), encryptedFileContent);
        }

        public IDataStoreInfo? GetDataStoreInfoFromLibrary(Library library)
        {
            return library is RemoteLibrary remoteLibrary
                ? GetAllDataStoreInfos().FirstOrDefault(x => x.Id == remoteLibrary.Id)
                : null;
        }
        #endregion

        #region Private methods
        private bool SaveUpdatedDataStoreInfo(Type dataStoreInfoType, string encryptedFileContent)
        {
            string path = _pathFinder.GetDataStoreInfoFilePath(dataStoreInfoType);

            using var stream = _fileService.Open(path);
            using var streamWriter = new StreamWriter(stream);

            streamWriter.Write(encryptedFileContent);

            return true;
        }

        private string AddDataStoreInfoToFileContent<TDataStoreInfo>(TDataStoreInfo dataStoreInfo)
        {
            string? fileContent = GetFileContent(typeof(TDataStoreInfo));
            string serializedDataStoreInfo = JsonConvert.SerializeObject(dataStoreInfo);

            if (fileContent == null)
            {
                CreateFileForDataStoreOfType(typeof(TDataStoreInfo));
                fileContent = serializedDataStoreInfo;
            }
            else
            {
                fileContent += $";{serializedDataStoreInfo}";
            }

            return fileContent;
        }

        private void UpdateEncryptetFileContents(Type typeOfDataStoreInfo, string encryptedFileContent)
        {
            _ = _encryptedFileContents.Remove(typeOfDataStoreInfo);
            _encryptedFileContents.Add(typeOfDataStoreInfo, encryptedFileContent);
        }

        private string? GetFileContent(Type typeOfDataStoreInfo)
        {
            if (!_encryptedFileContents.TryGetValue(typeOfDataStoreInfo, out string? encryptedFileContent) 
                || string.IsNullOrEmpty(encryptedFileContent))
            {
                var filePath = _pathFinder.GetDataStoreInfoFilePath(typeOfDataStoreInfo);

                try
                {
                    using var stream = _fileService.Open(filePath);
                    using var streamReader = new StreamReader(stream);

                    encryptedFileContent = streamReader.ReadToEnd();
                    _encryptedFileContents.Add(typeOfDataStoreInfo, encryptedFileContent);
                }
                catch (FileNotFoundException)
                {
                    return null;
                }
            }

            return _stringEncryptionService.Decrypt(encryptedFileContent);
        }

        private static IEnumerable<TDataStoreInfo> GetAllDataStoreInfosOfType<TDataStoreInfo>(string decryptedFileContent)
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

        private void CreateFileForDataStoreOfType(Type typeOfDataStore)
        {
            string filePath = _pathFinder.AppFolderPath + Path.DirectorySeparatorChar + $"{typeOfDataStore.Name}.dsi";
            using var stream = _fileService.Create(filePath);
            stream.Close();
        }
        #endregion
    }
}
