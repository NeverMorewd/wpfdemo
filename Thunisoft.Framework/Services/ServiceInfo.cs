using System;

namespace Thunisoft.Framework.Services
{
    public class ServiceInfo : IServiceInfo
    {
        #region Protected Member Variables

        protected Type m_serviceType;
        protected object m_serviceInstance;

        #endregion

        #region Constructors

        public ServiceInfo(Type aServiceType, object aServiceInstance)
        {
            m_serviceType = aServiceType;
            m_serviceInstance = aServiceInstance;
        }

        #endregion

        #region Protected Methods

        protected virtual object GetServiceInstance()
        {
            return m_serviceInstance;
        }

        #endregion

        #region IServiceInfo Interface
        public virtual Type ServiceType
        {
            get { return m_serviceType; }
        }

		public virtual object ServiceInstance
        {
            get { return GetServiceInstance(); }
        }

        #endregion
    }
}
