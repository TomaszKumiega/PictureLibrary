namespace PictureLibrary.Infrastructure.UI
{
    public class Command : IPictureLibraryCommand
    {
        public Func<object, bool> CanExecuteProp { get; set; }
        public Action<object> ExecuteProp { get; set; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return CanExecuteProp?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            ExecuteProp?.Invoke(parameter);
        }

        public void RaiseCanExecuteChanged()
            => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
