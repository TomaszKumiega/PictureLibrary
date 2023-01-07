using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using File = System.IO.File;

namespace PictureLibraryModel.Services.FileSystemServices
{
    public class FileService : FileSystemService, IFileService
    {
        public override void Copy(string sourcePath, string destinationPath)
        {
            File.Copy(sourcePath, destinationPath, true);
        }

        public override void Create(string path)
        {
            File.Create(path).Close();
        }

        public override bool Exists(string path)
        {
            return File.Exists(path);
        }

        public async Task<IEnumerable<string>> FindFilesAsync(string searchPattern, string directory)
        {
            var files = new List<string>();
            string[] subDirectories = null;

            files.AddRange((await Task.Run(() => Directory.GetFiles(directory, searchPattern))));
            subDirectories = await Task.Run(() => Directory.GetDirectories(directory));

            if(subDirectories == null)
            {
                return files;
            }

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

        public Stream OpenFile(string path, FileMode fileMode)
        {
            return File.Open(path, FileMode.Open);
        }

        public Stream OpenFile(string path, FileMode fileMode, FileAccess fileAccess, FileShare fileShare)
        {
            return File.Open(path, fileMode, fileAccess, fileShare);
        }

        public override void Remove(string path)
        {
            File.Delete(path);
        }

        public override void Rename(string path, string name)
        {
            var extension = new FileInfo(path).Extension;
            var directoryPath = Directory.GetParent(path).FullName;

            File.Move(path, directoryPath + Path.DirectorySeparatorChar + name + extension);
        }

        public byte[] ReadAllBytes(string path)
        {
            return File.ReadAllBytes(path);
        }

        public void WriteAllBytes(string path, byte[] bytes)
        {
            File.WriteAllBytes(path, bytes);
        }

        public void WriteAllLines(string path, string[] text)
        {
            File.WriteAllLines(path, text);
        }
    }
}
