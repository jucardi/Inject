using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;


/// <summary>
/// Simulates the behavior of "System.AppDomain" from legacy .NET frameworks.
/// </summary>
public class AppDomain
{
    #region Fields

    public static AppDomain CurrentDomain { get; private set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes the <see cref="T:AppDomain"/> class.
    /// </summary>
    static AppDomain()
    {
        CurrentDomain = new AppDomain();
    }

    #endregion

    #region Methods

    /// <summary>
    /// Gets the loaded assemblies in the current context.
    /// </summary>
    /// <returns>The assemblies.</returns>
    public Assembly[] GetAssemblies()
    {
        var assemblies = new List<Assembly>();
        var dependencies = DependencyContext.Default.RuntimeLibraries;

        dependencies.ToList().ForEach(x => Add(assemblies, x));

        return assemblies.ToArray();
    }

    /// <summary>
    /// Gets the loaded assemblies in the current context filtered by the given assembly name.
    /// </summary>
    /// <returns>The assemblies.</returns>
    /// <param name="assemblyName">Assembly name.</param>
    public Assembly[] GetAssemblies(string assemblyName)
    {
        var assemblies = new List<Assembly>();
        var dependencies = DependencyContext.Default.RuntimeLibraries;

        dependencies.ToList().ForEach(x =>
        {
            if (!IsCandidateCompilationLibrary(x, assemblyName))
                return;

            Add(assemblies, x);
        });

        return assemblies.ToArray();
    }

    /// <summary>
    /// Attempts to add load the specified library as an assembly and add it to the specified assembly list.
    /// </summary>
    /// <param name="assemblies">The assembly list</param>
    /// <param name="library">The <see cref="RuntimeLibrary"/></param>
    private static void Add(List<Assembly> assemblies, RuntimeLibrary library)
    {
        try
        {
            Assembly assembly = Assembly.Load(new AssemblyName(library.Name));
            assemblies.Add(assembly);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    /// <summary>
    /// Indicates whether the library should be loaded based on the matching criteria
    /// </summary>
    /// <returns><c>true</c>, if candidate compilation library matched the criteria, <c>false</c> otherwise.</returns>
    /// <param name="compilationLibrary">Compilation library.</param>
    /// <param name="assemblyName">Assembly name.</param>
    private static bool IsCandidateCompilationLibrary(RuntimeLibrary compilationLibrary, String assemblyName)
    {
        return compilationLibrary.Name == assemblyName
            || compilationLibrary.Dependencies.Any(d => d.Name.StartsWith(assemblyName, StringComparison.Ordinal));
    }

    #endregion
}