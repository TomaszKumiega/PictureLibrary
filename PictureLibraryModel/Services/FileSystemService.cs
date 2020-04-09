﻿using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace PictureLibraryModel.Services
{
    public class FileSystemService : IFileSystemService
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();


        public FileSystemService()
        {

        }

        public void CopyFile(string sourceFilePath, string destinationFilePath, bool overwrite) 
        {
            File.Copy(sourceFilePath, destinationFilePath,overwrite);
        }

        public ObservableCollection<Model.Directory> GetAllDirectories(string topDirectory, SearchOption option)
        {

            if (System.IO.Directory.Exists(topDirectory))
            {
                string[] fullPaths=null;

                try
                {
                     fullPaths = System.IO.Directory.GetDirectories(topDirectory, "*", option);
                }
                catch (Exception e)
                {
                    _logger.Error(e, "Couldn't load directories from " + fullPaths);
                }

                ObservableCollection<Model.Directory> directories = new ObservableCollection<Model.Directory>();

                if (fullPaths != null)
                {
                    foreach (var t in fullPaths)
                    {
                        directories.Add(new Model.Directory(t, (new System.IO.DirectoryInfo(t)).Name, this));
                    }
                }

                return directories;
            }
            else
            {
                return null;
            }
        }

        public ObservableCollection<Drive> GetDrives()
        {
            var drives = new ObservableCollection<Drive>();
            drives.Add(new Drive("My Computer", true, new FileSystemService()));

            foreach(var driveInfo in System.IO.DriveInfo.GetDrives())
            {
                drives[0].Children.Add(new Drive(driveInfo.Name, driveInfo.IsReady, this));
            }

            return drives;
        }

        public List<ImageFile> GetAllImageFiles(string directory)
        {
            var files = System.IO.Directory.GetFiles(directory, "*");
            var listOfFiles = files.ToList<string>();
            var listOfImageFiles = new List<ImageFile>();

            foreach(var t in listOfFiles.ToList())
            {
                if (!ImageFile.IsFileAnImage(t))
                {
                    listOfFiles.Remove(t);
                }
                else
                {
                    listOfImageFiles.Add(new ImageFile(t));
                }
            }

            return listOfImageFiles;
        }

        public void MoveFile(string filePath, string destinationPath, bool overwrite)
        {
            File.Move(filePath, destinationPath, overwrite);        
        }

        public void DeleteFile(string filePath)
        {
            File.Delete(filePath);
        }
    }
}
