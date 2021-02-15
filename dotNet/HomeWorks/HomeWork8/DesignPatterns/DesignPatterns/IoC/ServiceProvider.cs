using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.IoC
{
    class ServiceProvider : IServiceProvider
    {
        private readonly Dictionary<Type, object> transientCollection;
        private readonly Dictionary<Type, object> singletonCollection;

        public ServiceProvider(Dictionary<Type, object> transientCollection,
            Dictionary<Type, object> singletonCollection)
        {
            this.transientCollection = transientCollection;
            this.singletonCollection = singletonCollection;
        }
        public T GetService<T>()
        {
            if (singletonCollection.ContainsKey(typeof(T))) 
                return GetSingleton<T>(typeof(T));
            if (transientCollection.ContainsKey(typeof(T)))
                return GetTransient<T>(typeof(T));
            else return default(T);
        }

        private T GetSingleton<T>(Type type)
        {
            if(singletonCollection[type] == null) singletonCollection[type] = Activator.CreateInstance<T>();
            
            return (T)singletonCollection[type];
            
        }

        private T GetTransient<T>(Type type)
        {
            switch (transientCollection[type])
            {
                case Func<T> factory:
                    return factory.Invoke();

                case Func<IServiceProvider, T> serviceProvider:
                    return serviceProvider.Invoke(this);

                default:
                    return (T)type.GetConstructor(Type.EmptyTypes)?.Invoke(Type.EmptyTypes);
            }
        }
    }
}
