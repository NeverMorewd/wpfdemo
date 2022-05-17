using System;
using System.Collections.Generic;
using System.Reflection;

namespace Thunisoft.Framework.Services
{
    public class ServiceHost : IServiceHost
    {
        #region ServiceInfoInternal Class

        protected class ServiceInfoInternal : ServiceInfo
        {
            #region Protected Member Variables

            protected bool m_isDefault;
            protected bool m_isResolved;

            #endregion

            #region Constructors

            public ServiceInfoInternal(
                Type aServiceType,
                object aServiceInstance,
                bool anIsDefault)
                : base(aServiceType, aServiceInstance)
            {
                // Store arguments into members
                m_isDefault = anIsDefault;
                m_isResolved = false;
            }

            #endregion

            #region Protected Methods

            protected override object GetServiceInstance()
            {
                // Do we already cache an actual service reference?
                if (m_serviceInstance == null && !m_isResolved)
                {
                    // Try to create a new instance
                    lock (this)
                    {
                        if (m_serviceInstance == null && !m_isResolved)
                        {
                            m_serviceInstance = CreateServiceInstance(m_serviceType);
                            m_isResolved = true;
                        }
                    }
                }
                // Return the reference
                return m_serviceInstance;
            }

            #endregion

            #region Public Properties

            public virtual bool IsDefault
            {
                get { return m_isDefault; }
            }

            #endregion
        }

        #endregion

        #region Protected Member Variables

        protected Dictionary<Type, ServiceInfoInternal> m_registeredServices;
        protected IServiceInfo[] m_cache;

        #endregion

        #region Constructors
        public ServiceHost() : this(false)
        { }

        public ServiceHost(bool anInitializeFlag)
        {
            // Create members
            m_registeredServices = new Dictionary<Type, ServiceInfoInternal>();
            m_cache = null;

            // Initialize this instance with default services
            if (anInitializeFlag)
                Initialize();
        }

        #endregion

        #region Public Methods

        public virtual void RegisterService(
            Type aServiceType,
            object aServiceInstance)
        {
            // Clear cache
            m_cache = null;

            // Register the service
            lock (m_registeredServices)
            {
                // Add to collection
                m_registeredServices[aServiceType] = new ServiceInfoInternal(
                    aServiceType,
                    aServiceInstance,
                    false);
            }
        }

		public virtual void UnRegisterService(Type aServiceType)
        {
            // Clear cache
            m_cache = null;

            // Unregister the service
            lock (m_registeredServices)
            {
                // Remove from collection
                m_registeredServices.Remove(aServiceType);
            }
        }

        public static object CreateServiceInstance(Type aServiceType)
        {
            // Check if a valid type is specified
            if (aServiceType == null)
                throw new ArgumentNullException(
                    "Invalid ServiceType specified; type '<null>' not allowed.");

            // Try to get parameterless constructor
            ConstructorInfo ctor = aServiceType.GetConstructor(new Type[0]);
            if (ctor == null)
                throw new InvalidOperationException(string.Format(
                    "Invalid ServiceType specified, type '{0}' has no parameterless constructor.",
                    aServiceType.FullName));

            // Try to create a new service instance and return its reference
            return ctor.Invoke(new object[0]);
        }

        #endregion

        #region Protected Methods

        protected virtual void Initialize()
        {
            // Declare variables
            //LocalRepository repository = CreateLocalRepositoryService();
            //TransformerStore store = CreateTransformerStoreService();
            //ITransformer[] transformers = new ITransformer[]
            //{
            //    CreateSchemeRepositoryTransformerService(),
            //    CreateViewSettingRepositoryTransformerService()
            //};

            //// Register transformer with specific store
            //foreach (ITransformer transformer in transformers)
            //    store.RegisterTransformer(transformer.Namespace, transformer);

            // Add default services
            //m_registeredServices.Add(
            //    typeof(IEditorService),
            //    new ServiceInfoInternal(typeof(IEditorService), CreateEditorService(), true));
            //m_registeredServices.Add(
            //    typeof(IViewerService),
            //    new ServiceInfoInternal(typeof(IViewerService), CreateViewerService(), true));
            //m_registeredServices.Add(
            //    typeof(IBusinessService),
            //    new ServiceInfoInternal(typeof(IBusinessService), CreateBusinessService(), true));
            //m_registeredServices.Add(
            //    typeof(IRepositoryService),
            //    new ServiceInfoInternal(typeof(IRepositoryService), repository, true));
            //m_registeredServices.Add(
            //    typeof(LocalRepository),
            //    new ServiceInfoInternal(typeof(LocalRepository), repository, true));
            //m_registeredServices.Add(
            //    typeof(ITransformerStoreService),
            //    new ServiceInfoInternal(typeof(ITransformerStoreService), store, true));
        }
        #endregion

        #region Public Properties

        public virtual IServiceInfo[] RegisteredServices
        {
            get
            {
                // Check if we already cache the list
                if (m_cache == null)
                {
                    lock (m_registeredServices)
                    {
                        // Create new cache
                        int i = 0;
                        m_cache = new IServiceInfo[m_registeredServices.Count];
                        foreach (ServiceInfoInternal info in m_registeredServices.Values)
                            m_cache[i++] = info;
                    }
                }

                // Return list
                return m_cache;
            }
        }

        #endregion

        #region IServiceProvider Interface

        public virtual object GetService(Type aServiceType)
        {
            // Declare variables
            ServiceInfoInternal info;

            // Try to get service
            if (m_registeredServices.TryGetValue(aServiceType, out info))
                return info.ServiceInstance;
            else
                return null;
        }

        #endregion
    }
}
