using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Directory = PictureLibraryModel.Model.Directory;

namespace PictureLibraryModel.Services
{
    public class WindowsFileSystemService : FileSystemService
    {
        public override IEnumerable<Directory> GetRootDirectories()
        {
            var rootDirectories = new List<Directory>();

            foreach(var driveinfo in DriveInfo.GetDrives())
            {
                if(System.IO.Directory.Exists(driveinfo.Name))
                {
                    rootDirectories.Add(new Drive(driveinfo.Name, driveinfo.Name, this, Origin.Local));
                }
            }

            return rootDirectories;
        }
    }
}
