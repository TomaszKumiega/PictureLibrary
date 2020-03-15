﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryModel.Model
{
    public class ImageFile
    {
        public FileInfo FileInfo { get; set; }
        public string Path { get; set; }
        public Icon Icon { get; set; }
        public List<string> Albums { get; set; }

        public ImageFile()
        {

        }

        public ImageFile(string path)
        {
            if (File.Exists(path) && (IsFileAnImage(path)==true))
            {
                Path = path;
                FileInfo = new FileInfo(path);
                Icon = Icon.ExtractAssociatedIcon(path);
            }
            else throw new Exception("File not found");
        }

        private static bool IsFileAnImage(string path)
        {
            string[] supportedExtensions = { ".jpg", ".jpeg", ".jpe", ".png", ".jfif", ".bmp", ".tif", ".tiff", ".gif" };
            bool pathEndsWithSupportedExtension = false;
            
            foreach(string t in supportedExtensions)
            {
                if (path.EndsWith(t)) pathEndsWithSupportedExtension = true;
            }

            return pathEndsWithSupportedExtension;
        }

        public Image GetImage()
        {
            return Image.FromFile(Path);
        }
    }
}
