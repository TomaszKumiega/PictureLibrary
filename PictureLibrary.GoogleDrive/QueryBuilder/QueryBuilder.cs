namespace PictureLibrary.GoogleDrive
{
    public class QueryBuilder : IQueryBuilder
    {
        private string? _query;
        

        public IQueryBuilder ContainsText(string text)
        {
            _query += $"fullText='{text}'";

            return this;
        }

        public IQueryBuilder CreateQuery()
        {
            _query = string.Empty;

            return this;
        }

        public IQueryBuilder FileName(string fileName)
        {
            _query += $"name='{fileName}'";
                
            return this;
        }

        public IQueryBuilder FileNameContains(string text)
        {
            _query += $"name contains '{text}'";

            return this;
        }

        public string GetQuery()
        {
            return _query!;
        }

        public IQueryBuilder OwnerEmailAddress(string[] ownersEmailAddresses)
        {
            _query += $"owner'{string.Join(',', ownersEmailAddresses)}'";

            return this;
        }

        public IQueryBuilder PageSize(short pageSize)
        {
            _query += $"pageSize={pageSize}";

            return this;
        }

        public IQueryBuilder Parents(string[] parentIds)
        {
            _query += $"parents in '{string.Join(',', parentIds)}'";

            return this;
        }

        public IQueryBuilder SharedWithMe()
        {
            _query += $"sharedWithMe";

            return this;
        }

        public IQueryBuilder Trashed()
        {
            _query += $"trashed";

            return this;
        }

        public IQueryBuilder WithFileExtension(string fileExtension)
        {
            _query += $"fileExtension='{fileExtension}'";

            return this;
        }

        public IQueryBuilder WithMimeType(string mimeType)
        {
            _query += $"mimeType='{mimeType}'";

            return this;
        }

        public IQueryBuilder And()
        {
            _query += " and ";

            return this;
        }

        public IQueryBuilder Or()
        {
            _query += " or ";

            return this;
        }
    }
}
