namespace PictureLibrary.Infrastructure.UI
{
    public class PopupService : IPopupService
    {
        public void DisplayAlert(string title, string message, string cancelButtonText, string? acceptButtonText = null, FlowDirection? flowDirection = null)
        {
            Page page = Application.Current?.MainPage ?? throw new InvalidOperationException("Page is not available to display a popup");

            if (acceptButtonText == null && flowDirection == null)
            {
                page.DisplayAlert(title, message, cancelButtonText);
            }
            else if (flowDirection == null)
            {
                page.DisplayAlert(title, message, acceptButtonText, cancelButtonText);
            }
            else if (acceptButtonText == null)
            {
                page.DisplayAlert(title, message, cancelButtonText, flowDirection.Value);
            }
            else
            {
                page.DisplayAlert(title, message, acceptButtonText, cancelButtonText, flowDirection.Value);
            }
        }
    }
}
