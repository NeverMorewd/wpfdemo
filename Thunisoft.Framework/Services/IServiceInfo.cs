using System;

namespace Thunisoft.Framework.Services
{
    public interface IServiceInfo
    {
        #region Properties
        Type ServiceType
        {
            get;
        }

        object ServiceInstance
        {
            get;
        }
        #endregion
    }
}
