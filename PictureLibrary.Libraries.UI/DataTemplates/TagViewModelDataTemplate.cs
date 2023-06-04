using PictureLibrary.Libraries.UI.DataViewModels;

namespace PictureLibrary.Libraries.UI.DataTemplates
{
    public class TagViewModelDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TagDataTemplate { get; set; }
        public DataTemplate CommandDataTemplate { get; set; }
        
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((TagViewModel)item).IsCommand 
                ? CommandDataTemplate
                : TagDataTemplate;
        }
    }
}
