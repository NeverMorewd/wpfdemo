using System;
using System.Threading;
using System.Threading.Tasks;
using Thunisoft.Framework.Bindings;

namespace Thunisoft.Framework.UI.Controls.TaskMonitor
{
    public class TaskItemFileDownload : TFBindingProperty,ITaskItemContext
    {
        public string TaskName { get; set; }
        public int TaskId { get; set; }
        public float TaskProgressRatio { get; set; }
        public TaskStatusEnum TaskStatus { get; set; }
        public Task TaskInstance { get; set; }
        public string TaskMessage { get; set; }
        public IProgress<float> Progress { get;}
        public string FilePath { get; set; }
        public string FileLength { get; set; }
        public CancellationTokenSource TaskCancellationTokenSource { get; set; }
    }
}
