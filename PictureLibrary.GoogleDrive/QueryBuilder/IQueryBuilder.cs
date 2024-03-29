﻿namespace PictureLibrary.GoogleDrive
{
    public interface IQueryBuilder
    {
        IQueryBuilder CreateQuery();
        IQueryBuilder FileName(string fileName);
        IQueryBuilder FileNameContains(string text);
        IQueryBuilder WithMimeType(string mimeType);
        IQueryBuilder ContainsText(string text);
        IQueryBuilder Trashed();
        IQueryBuilder Parents(string[] parentIds);
        IQueryBuilder SharedWithMe();
        IQueryBuilder OwnerEmailAddress(string[] ownersEmailAddresses);
        IQueryBuilder PageSize(short pageSize);
        IQueryBuilder WithFileExtension(string fileExtension);
        IQueryBuilder Or();
        IQueryBuilder And();
        string GetQuery();
    }
}
