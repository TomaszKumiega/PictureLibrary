using System.ComponentModel;
using System.Reflection;

namespace PictureLibrary.Infrastructure.UI
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModelBase()
        {
            InitializeCommands(this);
        }

        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private static void InitializeCommands(object viewModel)
        {
            var commandProperties = viewModel.GetType().GetProperties().Where(x => x.PropertyType.IsAssignableFrom(typeof(IPictureLibraryCommand)));

            if (!commandProperties.Any())
                return;

            Dictionary<string, MethodInfo> canExecuteMethodsDictionary = new();
            Dictionary<string, MethodInfo> executeMethodsDictionary = new();

            IEnumerable<MethodInfo> canExecuteMethods = viewModel.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(x => x.IsDefined(typeof(CanExecuteAttribute), false));
            IEnumerable<MethodInfo> executeMethods = viewModel.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(x => x.IsDefined(typeof(ExecuteAttribute), false));

            foreach (var method in canExecuteMethods)
            {
                var attribute = (CanExecuteAttribute)method.GetCustomAttributes(typeof(CanExecuteAttribute)).FirstOrDefault();

                if (attribute != null)
                    canExecuteMethodsDictionary.Add(attribute.CommandName, method);
            }

            foreach (var method in executeMethods)
            {
                var attribute = (ExecuteAttribute)method.GetCustomAttributes(typeof(ExecuteAttribute)).FirstOrDefault();

                if (attribute != null)
                    executeMethodsDictionary.Add(attribute.CommandName, method);
            }

            foreach (var commandProperty in commandProperties)
            {
                var command = new Command
                {
                    CanExecuteProp = (Func<object, bool>)canExecuteMethodsDictionary.GetValueOrDefault(commandProperty.Name)?.CreateDelegate(typeof(Func<object, bool>), viewModel),
                    ExecuteProp = (Action<object>)executeMethodsDictionary.GetValueOrDefault(commandProperty.Name)?.CreateDelegate(typeof(Action<object>), viewModel)
                                    ?? throw new Exception($"No execute method of command {commandProperty.Name} in object of type {viewModel.GetType().Name}")
                };

                commandProperty.SetValue(viewModel, command);
            }
        }
    }
}
