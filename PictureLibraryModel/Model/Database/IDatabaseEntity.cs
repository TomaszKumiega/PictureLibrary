using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Model.Database
{
    public interface IDatabaseEntity
    {
        Guid Id { get; }
    }
}
