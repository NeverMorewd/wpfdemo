using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Thunisoft.Framework.Commands;
using Thunisoft.Framework.UI.Controls.TaskMonitor;

namespace Thunisoft.Framework.UI.Windows
{
    /// <summary>
    /// TaskMonitorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TaskMonitorWindow : Window, ITaskMonitorContext, ITaskMonitorWindowContext
    {
        private ITaskMonitorDataContext taskMonitorDataContext = null;
        private readonly ITaskMonitorContext taskMonitorContext = null;

        public event ExitMeetingDelegate ExitMeetingEvent;

        public ICommand CancelTaskCommand { get; set; }
        public ICommand RetryTaskCommand { get; set; }
        public FactoryEnum FactoryType { get; set; }
        public long MaxFileSize { get; set; }

        public TaskMonitorWindow()
        {
            InitializeComponent();
            FactoryType = FactoryEnum.FileUploadFactory;
            taskMonitorContext = this;
            Initialize();
        }
        public TaskMonitorWindow(ITaskMonitorContext aTaskMonitorContext)
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
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Border_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        public void OnWhenAllTaskComplete(bool isCompleteAll)
        {
            //todo
        }

        public void OnReportTaskCountInProgress(int aTaskCountInProgress)
        {
            //todo
        }
    }
}
