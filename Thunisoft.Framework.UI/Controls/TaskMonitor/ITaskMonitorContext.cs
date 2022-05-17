namespace Thunisoft.Framework.UI.Controls.TaskMonitor
{
    public interface ITaskMonitorContext
    {
        FactoryEnum FactoryType { get; set; }
        long MaxFileSize { get; set; }
        void OnWhenAllTaskComplete(bool isCompleteAll);
        void OnReportTaskCountInProgress(int aTaskCountInProgress);
        event ExitMeetingDelegate ExitMeetingEvent;
    }
    public delegate void ExitMeetingDelegate(object arg);
}
