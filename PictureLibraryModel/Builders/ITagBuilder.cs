using PictureLibraryModel.Model;
using PictureLibraryModel.Model.DataStoreInfo;

namespace PictureLibraryModel.Builders
{
    public interface ITagBuilder
    {
        public ITagBuilder CreateTag();
        public ITagBuilder WithName(string name);
        public ITagBuilder WithDescription(string? description);
        public ITagBuilder WithColor(string color);
        public Tag GetTag();
    }
}
