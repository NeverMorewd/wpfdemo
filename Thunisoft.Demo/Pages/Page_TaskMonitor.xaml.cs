using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Thunisoft.Framework.Commands;
using Thunisoft.Framework.Log;
using Thunisoft.Framework.UI.Controls.TaskMonitor;

namespace Thunisoft.Demo.Pages
{
    /// <summary>
    /// Page_TaskMonitor.xaml 的交互逻辑
    /// </summary>
    public partial class Page_TaskMonitor : Page, ITaskMonitorContext, ITaskMonitorWindowContext
    {
        private ITaskMonitorDataContext taskMonitorDataContext = null;
        private readonly ITaskMonitorContext taskMonitorContext = null;

        public event ExitMeetingDelegate ExitMeetingEvent;

        public ICommand CancelTaskCommand { get; set; }
        public ICommand RetryTaskCommand { get; set; }
        public FactoryEnum FactoryType { get; set; }
        public long MaxFileSize { get; set; }
        public Page_TaskMonitor()
        {
            InitializeComponent();
            FactoryType = FactoryEnum.FileUploadFactory;
            taskMonitorContext = this;
            Initialize();
        }
        public Page_TaskMonitor(ITaskMonitorContext aTaskMonitorContext)
        {
            InitializeComponent();
            taskMonitorContext = aTaskMonitorContext;
            Initialize();

        }

        private void Initialize()
        {
            taskMonitorDataContext = TaskMonitorFactoryFacade.CreateFactory(taskMonitorContext.FactoryType).GetTaskMonitorDataContext();
            this.DataContext = taskMonitorDataContext;
            CancelTaskCommand = new TFCommand<TaskItemFileUpload>(taskMonitorDataContext.OnCancelTask);
            RetryTaskCommand = new TFCommand<TaskItemFileUpload>(taskMonitorDataContext.OnRetryTask);
            taskMonitorContext.ExitMeetingEvent += TaskMonitorContext_ExitMeetingEvent;
        }

        private void TaskMonitorContext_ExitMeetingEvent(object arg)
        {
            TaskMonitorFactoryFacade.CreateFactory(taskMonitorContext.FactoryType).ClearTaskCollection();
        }

        public void Run(IEnumerable<ITaskItemContext> aTaskItems, Func<ITaskItemContext, object[], Task> aTaskExecuteAction, object[] aTaskActionParams)
        {
            TaskMonitorFactoryFacade.CreateFactory(taskMonitorContext.FactoryType).RunTaskMonitor(taskMonitorContext, aTaskItems, aTaskExecuteAction, aTaskActionParams);
        }

        public void OnWhenAllTaskComplete(bool isCompleteAll)
        {
            //todo
        }

        public void OnReportTaskCountInProgress(int aTaskCountInProgress)
        {
            //todo
        }
        private async Task UpLoadFileAsync(ITaskItemContext aTaskItem, object[] paramArray)
        {
            await Task.Run(async () =>
            {
                int percentComplete = 0;
                aTaskItem.TaskMessage = string.Empty;
                while (true)
                {
                    aTaskItem.TaskStatus = TaskStatusEnum.InProgress;
                    if (aTaskItem.TaskCancellationTokenSource.IsCancellationRequested)
                    {
                        percentComplete = 0;
                        aTaskItem.TaskStatus = TaskStatusEnum.Cancel;
                        aTaskItem.Progress.Report(percentComplete);
                        return;
                    }
                    await Task.Delay(1);
                    percentComplete++;
                    if (aTaskItem.Progress != null)
                    {
                        aTaskItem.Progress.Report(percentComplete);
                    }
                    if (percentComplete == 100)
                    {
                        aTaskItem.TaskStatus = TaskStatusEnum.Completed;
                        break;
                    }
                }
            }, aTaskItem.TaskCancellationTokenSource.Token).ConfigureAwait(false);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                InitialDirectory = @"C:\desktop",
                Filter = "图像文件、音频文件、文档文件 | *.jpg; *.jpeg; *.bmp; *.png; *.mp3; *.mp4; *.pdf; *.doc; *.docx",
                Multiselect = true, //是否可以多选true=ok/false=no
                AddExtension = true,
                Title = "请选择要上传的材料，每次最多可选10个",
                CheckFileExists = true,
            };

            if (openFileDialog.ShowDialog() == true)
            {
                if (openFileDialog.FileNames.Length > 10)
                {
                    return;
                }
                List<TaskItemFileUpload> taskItems = new List<TaskItemFileUpload>();
                openFileDialog.FileNames.ToList().ForEach(f =>
                {
                    TaskItemFileUpload t = new TaskItemFileUpload
                    {
                        TaskId = new Random().Next(),
                        TaskName = System.IO.Path.GetFileName(f),
                        TaskProgressRatio = 0,
                        FilePath = f,
                        TaskStatus = TaskStatusEnum.Ready,
                        FileUploadId =  System.DateTime.Now.ToString() + Convert.ToInt32(new Random().Next(0, 999)).ToString("D6"),
                    };
                    taskItems.Add(t);
                });
                Run(taskItems, UpLoadFileAsync, new object[] { });
            }
        }
    }
}
