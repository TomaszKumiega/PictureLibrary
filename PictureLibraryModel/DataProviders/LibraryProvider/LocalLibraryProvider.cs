﻿using NLog;
using PictureLibraryModel.Exceptions.DataAccessExceptions;
using PictureLibraryModel.Model;
using PictureLibraryModel.Resources;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryModel.Services.LibraryFileService;
using PictureLibraryModel.Services.SettingsProvider;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace PictureLibraryModel.DataProviders
{
    public class LocalLibraryProvider : ILibraryProvider
    {
        private static Logger Logger { get; } = LogManager.GetCurrentClassLogger();
        private IFileService FileService { get; }
        private ISettingsProviderService SettingsProvider { get; }
        private ILibraryFileService LibraryFileService { get; }

        public LocalLibraryProvider(IFileService fileService, ISettingsProviderService settingsProvider, ILibraryFileService libraryFileService)
        {
            FileService = fileService;
            SettingsProvider = settingsProvider;
            LibraryFileService = libraryFileService;
        }

        public void AddLibraries(IEnumerable<Library> libraries)
        {
            if (libraries == null)
                throw new ArgumentNullException(nameof(libraries));
            foreach (var library in libraries)
            {
                AddLibrary(library);
            }
        }

        public void AddLibrary(Library library)
        {
            if (library == null)
                throw new ArgumentNullException("library");

            FileService.Create(library.FullName);
            var fileStream = FileService.OpenFile(library.FullName, FileMode.Open, FileAccess.Write, FileShare.ReadWrite);

            SettingsProvider.Settings.ImportedLibraries.Add(library.FullName);
            SettingsProvider.SaveSettingsAsync();

            LibraryFileService.WriteLibraryToStreamAsync(fileStream, library);
        }

        public IEnumerable<Library> GetAllLibraries()
        {
            var libraries = new List<Library>();

            if (SettingsProvider.Settings.ImportedLibraries == null) return libraries;

            foreach (var t in SettingsProvider.Settings.ImportedLibraries)
            {
                try
                {
                    var stream = FileService.OpenFile(t, FileMode.Open, FileAccess.Read, FileShare.Read);
                    var library = LibraryFileService.ReadLibraryFromStreamAsync(stream, Guid.Empty).Result;
                    libraries.Add(library);
                }
                catch (FileNotFoundException e)
                {
                    Logger.Debug(e, "Library file not found.");
                    SettingsProvider.Settings.ImportedLibraries.Remove(t);
                    SettingsProvider.SaveSettingsAsync();
                    Logger.Info("Removed library entry: " + t + " from settings");
                }
                catch (Exception e)
                {
                    Logger.Debug(e, "Couldnt read " + t);
                }
            }

            return libraries;
        }

        public void RemoveLibrary(Library library)
        {
            if (library == null) 
                throw new ArgumentNullException(nameof(library));

            FileService.Remove(library.FullName);
        }

        public void UpdateLibrary(Library library)
        {
            if (library == null) 
                throw new ArgumentNullException(nameof(library));
            if (!FileService.Exists(library.FullName)) 
                throw new ArgumentException(nameof(library));

            // load file for potential recovery
            XmlDocument document = new XmlDocument();
            document.Load(library.FullName);

            // remove contents of the file
            string[] text = { "" };
            FileService.WriteAllLines(library.Name, text);

            try
            {
                // write updated library to the file
                var stream = FileService.OpenFile(library.FullName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
                LibraryFileService.WriteLibraryToStreamAsync(stream, library);
            }
            catch (Exception e)
            {
                Logger.Error(e, "Library update error:" + e.Message);
                document.Save(library.FullName);
            }
        }    
    }
}