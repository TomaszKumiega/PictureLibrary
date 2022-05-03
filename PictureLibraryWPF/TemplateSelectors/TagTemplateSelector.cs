using System.Windows;
using System.Windows.Controls;

namespace PictureLibraryWPF.TemplateSelectors
{
    public class TagTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NullTemplate { get; set; }
        public DataTemplate TagTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            return ((dynamic)container).ActualHeight != 0
                ? NullTemplate 
                : TagTemplate;
        }

    }
}
