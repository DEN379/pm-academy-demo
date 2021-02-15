using System;
using System.Collections.Generic;

namespace DesignPatterns.IoC
{
    public class ServiceCollection : IServiceCollection
    {
        private readonly Dictionary<Type, object> transientCollection = new Dictionary<Type, object>();
        private readonly Dictionary<Type, object> singletonCollection = new Dictionary<Type, object>();

        public IServiceCollection AddTransient<T>()
        {
            transientCollection.Add(typeof(T), null);
            return this;
        }

        public IServiceCollection AddTransient<T>(Func<T> factory)
        {
            transientCollection.Add(typeof(T), factory);
            return this;
        }

        public IServiceCollection AddTransient<T>(Func<IServiceProvider, T> factory)
        {
            transientCollection.Add(typeof(T), factory);
            return this;
        }

        public IServiceCollection AddSingleton<T>()
        {
            if (!singletonCollection.ContainsKey(typeof(T)))
                singletonCollection.TryAdd(typeof(T), null);
            return this;
        }

        public IServiceCollection AddSingleton<T>(T service)
        {
            singletonCollection.Add(typeof(T), service);
            return this;
        }

        public IServiceCollection AddSingleton<T>(Func<T> factory)
        {
            singletonCollection.Add(typeof(T), factory.Invoke());
            return this;
        }

        public IServiceCollection AddSingleton<T>(Func<IServiceProvider, T> factory)
        {
            T t = factory.Invoke(BuildServiceProvider());
            singletonCollection.Add(typeof(T), t);
            return this;
        }

        public IServiceProvider BuildServiceProvider()
        {
            return new ServiceProvider(transientCollection, singletonCollection);
        }
    }
}