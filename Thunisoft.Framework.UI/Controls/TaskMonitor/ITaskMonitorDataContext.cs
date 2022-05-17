using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace Thunisoft.Framework.UI.Controls.TaskMonitor
{
    public interface ITaskMonitorDataContext
    {
        Action<ITaskItemContext> OnRetryTask { get;  }
        Action<ITaskItemContext> OnCancelTask { get;  }
        ObservableCollection<ITaskItemContext> TaskCollection { get; set; }
        Visibility NullTaskNoteVisibility { get; set; }
        void RunTaskMonitor(ITaskMonitorContext aTaskMonitorContext, IEnumerable<ITaskItemContext> aTaskItem, Func<ITaskItemContext, object[], Task> aTaskExcuteDelegate, params object[] optionalParams);
        void ClearTaskCollection();

    }
}
