using NLog;
using PictureLibraryModel.Model.ConnectedServices;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryModel.Services.StringEncryption;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace PictureLibraryModel.Services.ConnectedServicesInfoProvider
{
    public class ConnectedServicesInfoProviderService : IConnectedServicesInfoProviderService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private IFileService _fileService;
        private IStringEncryptionService _encryptionService;

        public ConnectedServiceInfo RemoteServerInfo { get; set; }
        public ConnectedServiceInfo GoogleDriveInfo { get; set; }

        public ConnectedServicesInfoProviderService(IFileService fileService, IStringEncryptionService encryptionService)
        {
            _fileService = fileService;
            _encryptionService = encryptionService;

            LoadServicesInfo();
        }

        public void LoadServicesInfo()
        {
            if (!_fileService.Exists("servicesInfo.xml"))
            {
                _fileService.Create("servicesInfo.xml");
            }
            else
            {
                var remoteServerInfo = new ConnectedServiceInfo();
                var googleDriveInfo = new ConnectedServiceInfo();

                var fileStream = _fileService.OpenFile("servicesInfo.xml");

                XmlReaderSettings xmlSettings = new XmlReaderSettings();
                xmlSettings.DtdProcessing = DtdProcessing.Parse;

                using (var reader = XmlReader.Create(fileStream, xmlSettings))
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            switch (reader.Name)
                            {
                                case "remoteServer":
                                    {
                                        var remoteServerElement = XNode.ReadFrom(reader) as XElement;

                                        remoteServerInfo.Type = ConnectedServiceType.RemoteServer;
                                        remoteServerInfo.Token = _encryptionService.Decrypt(remoteServerElement.Attribute("token").Value);
                                        remoteServerInfo.RefreshToken = _encryptionService.Decrypt(remoteServerElement.Attribute("refreshToken").Value);
                                    }
                                    break;
                                case "googleDrive":
                                    {
                                        var googleDriveElement = XNode.ReadFrom(reader) as XElement;

                                        googleDriveInfo.Type = ConnectedServiceType.GoogleDrive;
                                        googleDriveInfo.Token = _encryptionService.Decrypt(googleDriveElement.Attribute("token").Value);
                                        googleDriveInfo.RefreshToken = _encryptionService.Decrypt(googleDriveElement.Attribute("refreshToken").Value);
                                    }
                                    break;
                            }
                        }
                    }
                }

                RemoteServerInfo = remoteServerInfo;
                GoogleDriveInfo = googleDriveInfo;
            }
        }

        public async Task SaveServicesInfoAsync()
        {
            var rootElement = new XElement("services_info");

            if (RemoteServerInfo != null)
            {
                var encryptedToken = _encryptionService.Encrypt(RemoteServerInfo.Token);
                var encryptedRefreshToken = _encryptionService.Encrypt(RemoteServerInfo.RefreshToken);

                var remoteServerElement = new XElement("remoteServer", new XAttribute("token", encryptedToken), new XAttribute("refreshToken", encryptedRefreshToken));
                rootElement.Add(remoteServerElement);
            }

            if (GoogleDriveInfo != null)
            {
                var encryptedToken = _encryptionService.Encrypt(GoogleDriveInfo.Token);
                var encryptedRefreshToken = _encryptionService.Encrypt(GoogleDriveInfo.RefreshToken);

                var googleDriveElement = new XElement("googleDrive", new XAttribute("token", encryptedToken), new XAttribute("refreshToken", encryptedRefreshToken));
                rootElement.Add(googleDriveElement);
            }

            if (!_fileService.Exists("servicesInfo.xml")) _fileService.Create("servicesInfo.xml");

            var fileStream = _fileService.OpenFile("servicesInfo.xml");

            try
            {
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    var xmlWriter = new XmlTextWriter(streamWriter);

                    xmlWriter.Formatting = Formatting.Indented;
                    xmlWriter.Indentation = 4;

                    await Task.Run(() => rootElement.Save(xmlWriter));
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, e.Message);
                throw new Exception("Couldn't save the settings");
            }
        }
    }
}
