using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Thunisoft.Framework.UI.Controls.TaskMonitor
{
    public class TaskMonitorFileDownloadFactory : TaskMonitorBaseFactory
    {
        private static readonly TaskMonitorFileDownloadFactory TaskMonitorFileDownloadFactoryInstance = new TaskMonitorFileDownloadFactory();
        private TaskMonitorFileDownloadDataContext TaskMonitorDataContext = null;
        private TaskMonitorFileDownloadFactory()
        {
            
        }
        public static TaskMonitorFileDownloadFactory CreateInstance()
        {
            return TaskMonitorFileDownloadFactoryInstance;
        }

        public override void ClearTaskCollection()
        {
            TaskMonitorDataContext.ClearTaskCollection();
        }

        public override ITaskMonitorDataContext GetTaskMonitorDataContext()
        {
            if (TaskMonitorDataContext == null)
            {
                TaskMonitorDataContext = new TaskMonitorFileDownloadDataContext();
            }
            return TaskMonitorDataContext;
        }

        public override void RunTaskMonitor(ITaskMonitorContext taskMonitorContext, IEnumerable<ITaskItemContext> aTaskItems, Func<ITaskItemContext, object[],Task> aTaskExecuteFunc, object[] aTaskActionParams)
        {
            TaskMonitorDataContext?.RunTaskMonitor(taskMonitorContext, aTaskItems, aTaskExecuteFunc, aTaskActionParams);
        }
    }
}
