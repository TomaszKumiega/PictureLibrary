using PictureLibraryModel.Model.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Model.Settings
{
    public class Settings : IDatabaseEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public bool LightMode { get; set; }
        public string AccentColor { get; set; }
        public string Language { get; set; }
    }
}
