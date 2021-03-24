using NLog;
using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = System.IO.File;

namespace PictureLibraryModel.Services.FileSystemServices
{
    public class FileService : FileSystemService, IFileService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public override void Copy(string sourcePath, string destinationPath)
        {
            File.Copy(sourcePath, destinationPath);
        }

        public override void Create(string path)
        {
            var fileStream = File.Create(path);
            fileStream.Close();
        }

        public override bool Exists(string path)
        {
            return File.Exists(path);
        }

        public async Task<IEnumerable<string>> FindFilesAsync(string searchPattern, string directory)
        {
            var files = new List<string>();
            string[] subDirectories = null;

            try
            {
                files.AddRange((await Task.Run(() => System.IO.Directory.GetFiles(directory, searchPattern))));
                subDirectories = await Task.Run(() => System.IO.Directory.GetDirectories(directory));
            }
            catch { }

            if(subDirectories != null)
            foreach(var t in subDirectories)
            {
                files.AddRange(await FindFilesAsync(searchPattern, t));
            }

            return files;
        }

        public override FileSystemInfo GetInfo(string path)
        {
            return new FileInfo(path);
        }

        public override void Move(string sourcePath, string destinationPath)
        {
            File.Move(sourcePath, destinationPath);
        }

        public Stream OpenFile(string path)
        {
            return File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        public override void Remove(string path)
        {
            File.Delete(path);
        }

        public override void Rename(string path, string name)
        {
            var extension = new FileInfo(path).Extension;
            var directoryPath = System.IO.Directory.GetParent(path).FullName;

            File.Move(path, directoryPath + "\\" + name + extension);
        }
    }
}
