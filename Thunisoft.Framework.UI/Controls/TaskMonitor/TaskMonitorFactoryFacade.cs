using System;

namespace Thunisoft.Framework.UI.Controls.TaskMonitor
{
    public class TaskMonitorFactoryFacade
    {
        public static TaskMonitorBaseFactory CreateFactory(FactoryEnum factoryEnum)
        {
            switch (factoryEnum)
            {
                case FactoryEnum.FileDownloadFactory:
                    return TaskMonitorFileDownloadFactory.CreateInstance();
                case FactoryEnum.FileUploadFactory:
                    return TaskMonitorFileUploadFactory.CreateInstance();
                default:
                    throw new NotImplementedException();
            }
        }
    }
    public enum FactoryEnum
    {
        FileUploadFactory = 1,
        FileDownloadFactory = 2,
    }
}
