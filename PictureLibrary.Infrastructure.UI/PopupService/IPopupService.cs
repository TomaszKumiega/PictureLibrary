namespace PictureLibrary.Infrastructure.UI
{
    public interface IPopupService
    {
        void DisplayAlert(string title, string message, string cancelButtonText, string acceptButtonText = null, FlowDirection? flowDirection = null);
    }
}