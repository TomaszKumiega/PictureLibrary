using Newtonsoft.Json;
using PictureLibrary.FileSystem.API;

namespace PictureLibrary.AppSettings
{
    public abstract class SettingsProviderBase<TSettings>
        where TSettings : class
    {
        protected readonly IPathFinder _pathFinder;
        private readonly IFileService _fileService;

        public SettingsProviderBase(
            IPathFinder pathFinder,
            IFileService fileService)
        {
            _pathFinder = pathFinder;
            _fileService = fileService;
        }

        protected abstract TSettings CreateDefaultSettings();

        public virtual TSettings GetSettings()
        {
            var path = _pathFinder.GetSettingsFilePath(typeof(TSettings));

            if (!_fileService.Exists(path))
            {
                using (var newFileStream = _fileService.Create(path))
                using (var newFileWriter = new StreamWriter(newFileStream))
                {
                    var defaultSettings = CreateDefaultSettings();
                    var defaultSettingsJson = JsonConvert.SerializeObject(defaultSettings);
                    newFileWriter.Write(defaultSettingsJson);
                }
            }

            using var stream = _fileService.Open(path);
            using var reader = new StreamReader(stream);

            var settingsJson = reader.ReadToEnd();

            return JsonConvert.DeserializeObject<TSettings>(settingsJson) ?? throw new JsonSerializationException();
        }

        public virtual bool SaveSettings(TSettings settings)
        {
            var path = _pathFinder.GetSettingsFilePath(typeof(TSettings));

            using var stream = _fileService.Exists(path)
                ? _fileService.Open(path)
                : _fileService.Create(path);

            using var writer = new StreamWriter(stream);

            var settingsJson = JsonConvert.SerializeObject(settings);

            writer.Write(settingsJson);

            return true;
        }
    }
}
