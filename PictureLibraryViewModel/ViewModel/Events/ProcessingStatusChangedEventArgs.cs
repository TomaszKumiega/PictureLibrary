namespace PictureLibraryViewModel.ViewModel.Events
{
    public enum ProcessingStatus
    {
        Processing,
        Finished,
        Failed
    }

    public class ProcessingStatusChangedEventArgs
    {
        public ProcessingStatus Status { get; }

        public ProcessingStatusChangedEventArgs(ProcessingStatus status)
        {
            Status = status;
        }
    }

    public delegate void ProcessingStatusChangedEventHandler(object sender, ProcessingStatusChangedEventArgs args);
}
