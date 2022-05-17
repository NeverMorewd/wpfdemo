using System;
using System.Threading;
using System.Threading.Tasks;
using Thunisoft.Framework.Bindings;

namespace Thunisoft.Framework.UI.Controls.TaskMonitor
{
    public class TaskItemFileUpload : TFBindingProperty,ITaskItemContext
    {
        private float taskProgressRatio;
        private TaskStatusEnum taskStatus = TaskStatusEnum.Ready;
        private string taskMessage;
        public string TaskName { get; set; }
        public int TaskId { get; set; } = new Random().Next();
        public float TaskProgressRatio
        {
            get
            {
                return taskProgressRatio;
            }
            set
            {
                SetProperty(ref taskProgressRatio,value);
            }
        }
        public TaskStatusEnum TaskStatus
        {
            get
            {
                return taskStatus;
            }
            set
            {
                SetProperty(ref taskStatus, value);
            }
        }
        public FileTypeEnum FileType { get; set; }
        public Task TaskInstance { get; set; }
        public string TaskMessage
        {
            get
            {
                return taskMessage;
            }
            set
            {
                SetProperty(ref taskMessage, value);
            }
        }
        public IProgress<float> Progress
        {
            get
            {
                return new Progress<float>(ratio =>
                {
                    TaskProgressRatio = ratio;
                });
            }
        }
        public string FilePath { get; set; }
        public string FileLength { get; set; }
        public string FileUploadId { get; set; }
        public CancellationTokenSource TaskCancellationTokenSource { get; set; }
    }
}
