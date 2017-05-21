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
        #region Constants

        private static readonly Dictionary<Type, object> CONFIGURATION_INSTANCES = new Dictionary<Type, object>();
        private static readonly Dictionary<Type, BeanInfo> BEAN_INFO = new Dictionary<Type, BeanInfo>();

        #endregion

        /// <summary>
        /// Load the specified assembly and scans for dependencies.
        /// </summary>
        /// <param name="assembly">The assembly to scan.</param>
        public static void Load(Assembly assembly)
        {
            assembly.GetTypes()
                    .ToList()
                    .FindAll(x => x.GetTypeInfo().GetCustomAttributes(typeof(ConfigurationAttribute)).Any())
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

        /// <summary>
        /// Attempts to resolve the specified bean for the given type
        /// </summary>
        /// <returns>The resolved dependency.</returns>
        /// <param name="type">Type.</param>
        /// <param name="beanName">Bean name.</param>
        public static object Resolve(Type type, string beanName = null)
        {
            if (!BEAN_INFO.ContainsKey(type))
                throw new BeanNotFoundException(String.Format("No beans found for type {0}", type.Name));

            return BEAN_INFO[type].Resolve(beanName);
        }

        /// <summary>
        /// Gets the configuration instance for bean resolve purposes.
        /// </summary>
        /// <returns>The configuration instance.</returns>
        /// <param name="type">Type.</param>

        internal static object GetConfiguration(Type type)
        {
            if (!CONFIGURATION_INSTANCES.ContainsKey(type))
                throw new ConfigurationClassNotFound(String.Format("Configuration class {0} not found", type.Name));

            return CONFIGURATION_INSTANCES[type];
        }

        /// <summary>
        /// Loads all the bean information by the given configuration class.
        /// </summary>
        /// <param name="configType">Configuration class.</param>
        private static void LoadConfigurationClass(Type configType)
        {
            if (CONFIGURATION_INSTANCES.ContainsKey(configType)) return; // Already loaded.

            configType
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

                    string beanName = beanAttr.Name ?? x.Name;

                    BEAN_INFO[x.ReturnType].AddBean(beanName, x, primaryAttr != null);
                });

            object configInstance = Activator.CreateInstance(configType);
            CONFIGURATION_INSTANCES.Add(configType, configInstance);
        }
    }
}
