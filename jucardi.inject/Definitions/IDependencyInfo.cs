using System;

namespace Jucardi.Inject.Definitions
{
    internal interface IDependencyInfo
    {
        /// <summary>
        /// Gets the bean name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:jucardi.inject.Definitions.MethodDependencyInfo`1"/> is primary.
        /// </summary>
        /// <value><c>true</c> if is primary; otherwise, <c>false</c>.</value>
        bool IsPrimary { get; }

        /// <summary>
        /// Inidicates the method to be invoked to initialize the class after all dependencies have been injected.
        /// </summary>
        /// <value>The init method.</value>
        string InitMethod { get; }

        /// <summary>
        /// Gets the parent container.
        /// </summary>
        /// <value>The parent container.</value>
        ApplicationContext Context { get; }

        /// <summary>
        /// Creates a new instance of the bean defined by this this isntance of <see cref="IDependencyInfo"/>
        /// </summary>
        /// <returns>The create.</returns>
        object Create();
    }
}
