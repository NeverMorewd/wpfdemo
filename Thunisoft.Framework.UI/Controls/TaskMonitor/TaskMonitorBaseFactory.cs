using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Thunisoft.Framework.UI.Controls.TaskMonitor
{
    public abstract class TaskMonitorBaseFactory
    {
        public abstract ITaskMonitorDataContext GetTaskMonitorDataContext();
        public abstract void RunTaskMonitor(ITaskMonitorContext taskMonitorContext,IEnumerable<ITaskItemContext> aTaskItems, Func<ITaskItemContext, object[],Task> aTaskExecuteFunc, object[] aTaskActionParams);
        public abstract void ClearTaskCollection();
    }
}
