using PictureLibraryViewModel.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PictureLibraryViewModel.Commands
{
    public class CommandCreator : ICommandCreator
    {
        private readonly Func<IPictureLibraryCommand> _commandLocator;

        public CommandCreator(Func<IPictureLibraryCommand> commandLocator)
        {
            _commandLocator = commandLocator;
        }

        public void InitializeCommands(object viewModel)
        {
            var commandProperties = viewModel.GetType().GetProperties().Where(x => x.IsDefined(typeof(CommandAttribute), false));

            if (!commandProperties.Any())
                return;

            Dictionary<string, MethodInfo> canExecuteMethodsDictionary = new Dictionary<string, MethodInfo>();
            Dictionary<string, MethodInfo> executeMethodsDictionary = new Dictionary<string, MethodInfo>();

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
                var command = _commandLocator();

                command.CanExecuteProp = (Func<object, bool>)canExecuteMethodsDictionary.GetValueOrDefault(commandProperty.Name)?.CreateDelegate(typeof(Func<object, bool>), viewModel);
                command.ExecuteProp = (Action<object>)executeMethodsDictionary.GetValueOrDefault(commandProperty.Name)?.CreateDelegate(typeof(Action<object>), viewModel) 
                    ?? throw new Exception($"No execute method of command {commandProperty.Name} in object of type {viewModel.GetType().Name}");
                
                commandProperty.SetValue(viewModel, command);
            }
        }
    }
}
