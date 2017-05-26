using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using jucardi.inject.Exceptions;

namespace jucardi.inject.Definitions
{
    internal class BeanInfo
    {
        #region Fields

        private readonly Type type;
        private Dictionary<string, IDependencyInfo> beans = new Dictionary<string, IDependencyInfo>();
        private Dictionary<string, object> instances = new Dictionary<string, object>();
        private IDependencyInfo primary;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:jucardi.inject.Definitions.BeanInfo"/> class.
        /// </summary>
        /// <param name="type">Type.</param>
        public BeanInfo(Type type)
        {
            this.type = type;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Resolve the specified bean.
        /// </summary>
        /// <returns>The instance of the resolved dependency.</returns>
        /// <param name="beanName">The bean name.</param>
        public object Resolve(string beanName = null)
        {
            string beanKey = !String.IsNullOrEmpty(beanName) ? beanName : String.Empty;

            if (instances.ContainsKey(beanKey))
            {
                return instances[beanKey];
            }

            if (String.IsNullOrEmpty(beanKey))
            {
                IDependencyInfo info = primary ??
                    (beans.Count == 1 ? beans.Values.First() :
                     throw new BeanNotFoundException(String.Format(beans.Count > 1 ? 
                                                                   "Multiple beans found for {0}, none marked as primary." : 
                                                                   "No primary bean found for {0}.", type.Name)));

                object val = Injector.Autowire(info.Create());
                instances.Add(String.Empty, val);

                return val;
            }

            if (!beans.ContainsKey(beanKey))
            {
                throw new BeanNotFoundException(String.Format("No bean found under the name of {0} for type {1}", beanKey, type.Name));
            }

            object value = Injector.Autowire(beans[beanKey].Create());
            instances.Add(beanKey, value);

            return value;
        }

        /// <summary>
        /// Adds the bean.
        /// </summary>
        /// <param name="name">The bean name.</param>
        /// <param name="mInfo">The Method info.</param>
        /// <param name="isPrimary">If set to <c>true</c> is primary.</param>
        public void AddBean(string name, MemberInfo mInfo, bool isPrimary = false)
        {
            if (isPrimary && primary != null)
            {
                throw new InjectStartupException(String.Format("Multiple primary beans detected for type: {0}. Bean names: {1}, {2}", type.Name, primary.Name, name));
            }

            if (beans.ContainsKey(name))
            {
                throw new InjectStartupException(String.Format("Duplicated bean found for type: {0}. Bean name: {1}", type.Name, name));
            }

            IDependencyInfo info = CreateDependencyInfo(name, mInfo, isPrimary);

            if (isPrimary)
            {
                primary = info;
            }

            beans.Add(name, info);
        }

        /// <summary>
        /// Creates the dependency info.
        /// </summary>
        /// <returns>The dependency info.</returns>
        /// <param name="methodBase">Method base.</param>
        /// <param name="name">The Bean name.</param>
        /// <param name="isPrimary">If set to <c>true</c> is primary.</param>
        private IDependencyInfo CreateDependencyInfo(string name, MemberInfo methodBase, bool isPrimary)
        {
            if (methodBase is MethodInfo)
            {
                return new MethodDependencyInfo(methodBase as MethodInfo, name, isPrimary);
            }

            if (methodBase is ConstructorInfo)
            {
                return new ConstructorDependencyInfo(methodBase as ConstructorInfo, name, isPrimary);
            }

            if (methodBase is PropertyInfo)
            {
                return new PropertyDependencyInfo(methodBase as PropertyInfo, name, isPrimary);
            }

            throw new InjectStartupException("Invalid injection method");
        }

        #endregion
    }
}
