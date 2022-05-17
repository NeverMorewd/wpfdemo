using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Thunisoft.Framework.UI.Controls.TaskMonitor
{
    /// <summary>
    /// TaskMonitorControl.xaml 的交互逻辑
    /// </summary>
    public partial class TaskMonitorControl : UserControl
    {
        public static readonly DependencyProperty TaskCollectionProperty = DependencyProperty.Register("TaskCollection", typeof(ObservableCollection<ITaskItemContext>), typeof(TaskMonitorControl), new PropertyMetadata(default(ObservableCollection<ITaskItemContext>))); 

        public ObservableCollection<ITaskItemContext> TaskCollection
        {
            get { return (ObservableCollection<ITaskItemContext>)GetValue(TaskCollectionProperty); }
            set { SetValue(TaskCollectionProperty, value); }
        }
        public TaskMonitorControl()
        {
            InitializeComponent(); 
        }
    }
}
