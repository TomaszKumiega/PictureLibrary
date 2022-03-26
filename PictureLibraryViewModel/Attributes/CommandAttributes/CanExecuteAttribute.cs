using System;

namespace PictureLibraryViewModel.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    internal class CanExecuteAttribute : Attribute
    {
        public string CommandName { get; }

        public CanExecuteAttribute(string commandName)
        {
            CommandName = commandName;
        }
    }
}
