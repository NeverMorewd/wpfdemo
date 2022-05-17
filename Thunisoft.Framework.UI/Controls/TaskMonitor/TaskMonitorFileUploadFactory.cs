using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Thunisoft.Framework.UI.Controls.TaskMonitor
{
    public class TaskMonitorFileUploadFactory : TaskMonitorBaseFactory
    {
        private static readonly TaskMonitorFileUploadFactory TaskMonitorFileUploadFactoryInstance = new TaskMonitorFileUploadFactory();
        private TaskMonitorFileUploadDataContext TaskMonitorDataContext = null;
        private TaskMonitorFileUploadFactory()
        {

        }
        public static TaskMonitorFileUploadFactory CreateInstance()
        {
            return TaskMonitorFileUploadFactoryInstance;
        }

        public override ITaskMonitorDataContext GetTaskMonitorDataContext()
        {
            if (TaskMonitorDataContext == null)
            {
                TaskMonitorDataContext = new TaskMonitorFileUploadDataContext();
            }
            return TaskMonitorDataContext;
        }
        public override void RunTaskMonitor(ITaskMonitorContext taskMonitorContext,IEnumerable<ITaskItemContext> aTaskItems, Func<ITaskItemContext, object[], Task> aTaskExecuteFunc, object[] aTaskActionParams)
        {
            TaskMonitorDataContext?.RunTaskMonitor(taskMonitorContext,aTaskItems, aTaskExecuteFunc, aTaskActionParams);
        }
        public override void ClearTaskCollection()
        {
            TaskMonitorDataContext?.ClearTaskCollection();
        }
    }
}
