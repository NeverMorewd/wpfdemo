using System;
using System.Threading;
using System.Threading.Tasks;

namespace Thunisoft.Framework.UI.Controls.TaskMonitor
{
    public interface ITaskItemContext
    {
        string TaskName { get; set; }
        int TaskId { get; set; }
        float TaskProgressRatio { get; set; }
        TaskStatusEnum TaskStatus { get; set; }
        Task TaskInstance { get; set; }
        string TaskMessage { get; set; }
        IProgress<float> Progress { get; }
        string FilePath { get; set; }
        string FileLength { get; set; }
        CancellationTokenSource TaskCancellationTokenSource { get; set; }
    }
}
