namespace PictureLibrary.Infrastructure.UI
{
    [AttributeUsage(AttributeTargets.Method)]
    internal class ExecuteAttribute : Attribute
    {
        public string CommandName { get; }

        public ExecuteAttribute(string commandName)
        {
            CommandName = commandName;
        }
    }
}
