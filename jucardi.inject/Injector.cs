using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using jucardi.inject.Attributes;
using jucardi.inject.Definitions;
using jucardi.inject.Exceptions;

namespace jucardi.inject
{
    public static class Injector
    {
        private static readonly Dictionary<Type, object> CONFIGURATION_INSTANCES = new Dictionary<Type, object>();
        private static readonly Dictionary<Type, BeanInfo> BEAN_INFO = new Dictionary<Type, BeanInfo>();

        /// <summary>
        /// Load the specified assembly and scans for dependencies.
        /// </summary>
        /// <param name="assembly">The assembly to scan.</param>
        public static void Load(Assembly assembly)
        {
            assembly.GetTypes()
                    .ToList()
                    .FindAll(x => x.GetTypeInfo().GetCustomAttributes(typeof(DependencyConfigurationAttribute)).Any())
                    .ForEach(LoadConfigurationClass);
        }

        /// <summary>
        /// Load the specified assembly and scans for dependencies.
        /// </summary>
        /// <param name="assemblyName">The assembly name to scan.</param>
        public static void Load(string assemblyName)
        {
            AppDomain.CurrentDomain.GetAssemblies(assemblyName)
                     .ToList()
                     .ForEach(Load);
        }

        /// <summary>
        /// Resolve the specified dependency.
        /// </summary>
        /// <returns>The requested instance.</returns>
        /// <param name="beanName">Bean name. If none provided, will return the primary.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T Resolve<T>(string beanName = null)
        {
            return (T)Resolve(typeof(T), beanName);
        }

        internal static object GetConfiguration(Type type)
        {
            // TODO: Validate the type exists.
            return CONFIGURATION_INSTANCES[type];
        }

        public static object Resolve(Type type, string beanName = null)
        {
            if (!BEAN_INFO.ContainsKey(type))
                throw new BeanNotFoundException(String.Format("No beans found for type {0}", type.Name));

            return BEAN_INFO[type].Resolve(beanName);
        }

        private static void LoadConfigurationClass(Type configurationClass)
        {
            if (CONFIGURATION_INSTANCES.ContainsKey(configurationClass)) return;

            configurationClass
                .GetMethods()
                .ToList()
                .ForEach(x =>
                {
                    BeanAttribute beanAttr = x.GetCustomAttribute<BeanAttribute>();
                    PrimaryAttribute primaryAttr = x.GetCustomAttribute<PrimaryAttribute>();

                    if (beanAttr == null) return;

                    if (!BEAN_INFO.ContainsKey(x.ReturnType))
                    {
                        BEAN_INFO.Add(x.ReturnType, new BeanInfo(x.ReturnType));
                    }

                    BEAN_INFO[x.ReturnType].AddBean(beanAttr.Name, x, primaryAttr != null);
                });
        }
    }
}
