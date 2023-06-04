using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PictureLibraryModel.Model;

namespace PictureLibrary.Libraries.UI.DataViewModels
{
    public class TagViewModel : ObservableObject
    {
        public TagViewModel(Tag tag)
        {
            Name = tag.Name;
            Description = tag.Description;
            Color = string.IsNullOrEmpty(tag.Color)
                ? Colors.Transparent
                : Color.FromArgb(tag.Color);
        }

        public TagViewModel(IRelayCommand command, string commandName, Color color)
        {
            RelayCommand = command;
            CommandName = commandName;
            Color = color;
            IsCommand = true;
        }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public Color Color { get; set; }

        public IRelayCommand? RelayCommand { get; }
        public string? CommandName { get; }
        public bool IsCommand { get; }
    }
}
