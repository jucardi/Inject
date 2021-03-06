﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jucardi.Inject.Attributes;
using Jucardi.Inject.Definitions;
using Jucardi.Inject.Exceptions;
using Jucardi.Inject.stereotype;

namespace Jucardi.Inject
{
    public class ApplicationContext
    {
        public static ApplicationContext Default { get; private set; } = new ApplicationContext();

        #region Constants

        private readonly Dictionary<Type, object> configurationInstances = new Dictionary<Type, object>();
        private readonly Dictionary<Type, TypeContainer> beanInfo = new Dictionary<Type, TypeContainer>();

        #endregion

        /// <summary>
        /// Load the specified assembly and scans for dependencies.
        /// </summary>
        /// <param name="assembly">The assembly to scan.</param>
        public void Scan(Assembly assembly)
        {
            ScanConfigurations(assembly);
            ScanComponents(assembly);
        }

        /// <summary>
        /// Load the assemblies matching the given start pattern and scans for dependencies.
        /// </summary>
        /// <param name="assemblyPattern">The assembly name pattern to scan.</param>
        public void Scan(string assemblyPattern)
        {
            AppDomain.CurrentDomain.GetAssemblies(assemblyPattern)
                     .ToList()
                     .ForEach(Scan);
        }

        /// <summary>
        /// Scans all loaded assemblies into the current domain (not recommended).
        /// </summary>
        public void Scan()
        {
            AppDomain.CurrentDomain.GetAssemblies()
                     .ToList()
                     .ForEach(Scan);
        }

        /// <summary>
        /// Autowires field dependencies in the specified instance.
        /// </summary>
        /// <returns>The autowire.</returns>
        /// <param name="instance">Instance.</param>
        public object Autowire(object instance)
        {
            // TODO: Add support for Required boolean.

            instance
                .GetType()
                .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .ToList()
                .FindAll(x => x.GetCustomAttribute<AutowiredAttribute>() != null)
                .ForEach(x =>
                {
                    QualifierAttribute qualifierArr = x.GetCustomAttribute<QualifierAttribute>();
                    string beanName = qualifierArr != null ? qualifierArr.Name : null;
                    x.SetValue(instance, Resolve(x.FieldType, beanName));
                });

            return instance;
        }

        /// <summary>
        /// Resolve the specified dependency.
        /// </summary>
        /// <returns>The requested instance.</returns>
        /// <param name="beanName">Bean name. If none provided, will return the primary.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public T Resolve<T>(string beanName = null)
        {
            return (T)Resolve(typeof(T), beanName);
        }

        /// <summary>
        /// Attempts to resolve the specified bean for the given type
        /// </summary>
        /// <returns>The resolved dependency.</returns>
        /// <param name="type">Type.</param>
        /// <param name="beanName">Bean name.</param>
        public object Resolve(Type type, string beanName = null)
        {
            // TODO: Detect circular dependencies to avoid Stack Overflow.

            if (!beanInfo.ContainsKey(type))
                throw new BeanNotFoundException(String.Format("No beans found for type {0}", type.Name));

            return beanInfo[type].Resolve(beanName);
        }

        /// <summary>
        /// Gets the configuration instance for bean resolve purposes.
        /// </summary>
        /// <returns>The configuration instance.</returns>
        /// <param name="type">Type.</param>

        internal object GetConfiguration(Type type)
        {
            if (!configurationInstances.ContainsKey(type))
                throw new ConfigurationClassNotFound(String.Format("Configuration class {0} not found", type.Name));

            return configurationInstances[type];
        }

        /// <summary>
        /// Scans the assembly for configuration classes.
        /// </summary>
        /// <param name="assembly">Assembly.</param>
        private void ScanConfigurations(Assembly assembly)
        {
            assembly.GetTypes()
                    .ToList()
                    .FindAll(x => x.GetTypeInfo().GetCustomAttributes(typeof(ConfigurationAttribute)).Any())
                    .ForEach(LoadConfigurationClass);
        }

        /// <summary>
        /// Scans the assembly for classes marked as components.
        /// </summary>
        /// <param name="assembly">Assembly.</param>
        private void ScanComponents(Assembly assembly)
        {
            assembly.GetTypes()
                    .ToList()
                    .FindAll(x => x.GetTypeInfo().GetCustomAttributes(typeof(ComponentAttribute)).Any())
                    .ForEach(LoadComponent);
        }

        /// <summary>
        /// Loads all the bean information by the given configuration class.
        /// </summary>
        /// <param name="configType">Configuration class.</param>
        private void LoadConfigurationClass(Type configType)
        {
            if (configurationInstances.ContainsKey(configType)) return; // Already loaded.

            configType
                .GetMethods()
                .ToList()
                .ForEach(x =>
                {
                    BeanAttribute beanAttr = x.GetCustomAttribute<BeanAttribute>();
                    PrimaryAttribute primaryAttr = x.GetCustomAttribute<PrimaryAttribute>();

                    if (beanAttr == null) return;

                    if (!beanInfo.ContainsKey(x.ReturnType))
                    {
                        beanInfo.Add(x.ReturnType, new TypeContainer(this, x.ReturnType));
                    }

                    string beanName = beanAttr.Name ?? x.Name;

                    beanInfo[x.ReturnType].AddBean(beanName, x, primaryAttr != null, beanAttr.InitMethod);
                });

            object configInstance = Activator.CreateInstance(configType);
            configurationInstances.Add(configType, configInstance);

            // TODO: Add beans declared as properties.
        }

        /// <summary>
        /// Loads the compoenent information.
        /// </summary>
        /// <param name="componentType">Component type.</param>
        private void LoadComponent(Type componentType)
        {
            if (!beanInfo.ContainsKey(componentType))
            {
                beanInfo.Add(componentType, new TypeContainer(this, componentType));
            }

            ConstructorInfo[] ctorInfos = componentType.GetConstructors();

            if (ctorInfos.Length > 1 && ctorInfos.ToList().FindAll(x => x.GetCustomAttribute(typeof(AutowiredAttribute)) != null).Count() != 1)
            {
                throw new ComponentLoadException(
                    String.Format("Multiple constructors found for type {0}. When multiple constructors are present in a component, exactly one must be marked with the Autowire attribute",
                                  componentType.Name));
            }

            ComponentAttribute attr = componentType.GetTypeInfo().GetCustomAttribute<ComponentAttribute>();
            ConstructorInfo ctor = ctorInfos.Length == 1 ? ctorInfos[0] : ctorInfos.First(x => x.GetCustomAttribute(typeof(AutowiredAttribute)) != null);
            string beanName = attr.Value ?? componentType.Name;

            beanInfo[componentType].AddBean(beanName, ctor, true);
        }
    }
}
