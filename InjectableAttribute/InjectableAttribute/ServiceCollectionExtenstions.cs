using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace InjectableAttribute.ServiceCollection
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extensions
    /// </summary>
    public static class ServiceCollectionExtentions
    {
        #region Public Methods

        /// <summary>
        /// Adds the Injectable services to the native .net IoC container
        /// </summary>
        /// <param name="serviceCollection">The Service Collection</param>
        /// <param name="assemblies">The assemblies to search Injectable types</param>
        public static void AddInjectables(this IServiceCollection serviceCollection, IEnumerable<Assembly> assemblies = null)
        {
            // if there are no assemblies provided, load all from execution assembly folder
            if (assemblies == null)
            {
                var executionPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (executionPath == null)
                {
                    throw new Exception("Couldn't identify execution path");
                }

                var assemblyFileNames = System.IO.Directory.EnumerateFiles(
                    path: executionPath,
                    searchPattern: "*.dll",
                    searchOption: System.IO.SearchOption.TopDirectoryOnly
                );

                assemblies = new List<Assembly>();
                foreach (var assemblyFileName in assemblyFileNames)
                {
                    try
                    {
                        ((List<Assembly>)assemblies).Add(Assembly.LoadFrom(assemblyFileName));
                    }
                    catch
                    {
                        // skip invalid assemblies
                        // you may want to log some warning or error here!
                    }
                }
            }

            foreach (var assembly in assemblies)
            {
                IEnumerable<Type> assemblyTypes = null;
                try
                {
                    assemblyTypes = assembly.ExportedTypes;
                }
                catch
                {
                    // skip invalid assemblies
                    // you may want to log some warning or error here!
                    continue;
                }

                // scan all this assembly types
                foreach (var component in assemblyTypes)
                {
                    // check if type as the Injectable attribute
                    IEnumerable<Injectable> injectableAttrs = null;
                    try
                    {
                        injectableAttrs = component.GetCustomAttributes<Injectable>();
                    }
                    catch
                    {
                        // skip invalid types
                        // you may want to log some warning or error here!
                        continue;
                    }

                    // Note: type may implement multiple services
                    foreach (var attr in injectableAttrs)
                    {
                        // pick the needed registration .net core function
                        Func<Type, Type, IServiceCollection> registration = attr.Lifetime switch
                        {
                            ServiceLifetime.Transient => serviceCollection.AddTransient,
                            ServiceLifetime.Singleton => serviceCollection.AddSingleton,
                            ServiceLifetime.Scoped => serviceCollection.AddScoped,
                            _ => serviceCollection.AddTransient
                        };

                        // register each service that this component provides
                        foreach (var service in attr.Services)
                        {
                            registration(service, component);
                        }
                    }
                }
            }
        }

        #endregion Public Methods
    }
}