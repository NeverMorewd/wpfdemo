using System;

namespace Thunisoft.Framework.Services
{
    public interface IServiceHost : IServiceProvider
    {
        #region Methods
        void RegisterService(Type aServiceType, object aServiceInstance);

        /// <summary>
        /// 注销一个服务
        /// </summary>
        /// <param name="aServiceType">
        /// 与一个服务实例关联的类型
        /// </param>
		void UnRegisterService(Type aServiceType);

        #endregion

        #region Properties
        /// <summary>
        /// 已注册的服务实例集合
        /// </summary>
        IServiceInfo[] RegisteredServices
        {
            get;
        }

        #endregion
    }
}
