using System.Windows.Input;

namespace Thunisoft.Framework.UI.Controls.TaskMonitor
{
    public interface ITaskMonitorWindowContext
    {
        ICommand CancelTaskCommand { get; set; }
        ICommand RetryTaskCommand { get; set; }
    }
}
