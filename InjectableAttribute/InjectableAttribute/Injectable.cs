using Microsoft.Extensions.DependencyInjection;
using System;

namespace InjectableAttribute
{
    /// <summary>
    /// Class attribute that can be used to define the services that the class provide
    /// Example:
    ///     [Injectable(ServiceLifetime.Transient, typeof(ILoggerErrors), typeof(ILoggerWarnings))]
    ///     [Injectable(ServiceLifetime.Singleton, typeof(ILoggerErrors)]
    ///     [Injectable(ServiceLifetime.Transient, typeof(ILoggerWarnings))]
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class Injectable : Attribute
    {
        #region Public Properties

        /// <summary>
        /// Gets the lifetime.
        /// </summary>
        /// <value>
        /// The lifetime.
        /// </value>
        public ServiceLifetime Lifetime { get; }

        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <value>
        /// The services.
        /// </value>
        public Type[] Services { get; }

        #endregion Public Properties

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Injectable" /> class.
        /// </summary>
        /// <param name="lifetime">The lifetime.</param>
        /// <param name="services">The services.</param>
        public Injectable(ServiceLifetime lifetime, params Type[] services)
        {
            Lifetime = lifetime;
            Services = services;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Injectable" /> class,
        /// using the default ServiceLifetime: <see cref="ServiceLifetime.Transient"/>
        /// </summary>
        /// <param name="services">The services.</param>
        public Injectable(params Type[] services)
        {
            Lifetime = ServiceLifetime.Transient;
            Services = services;
        }

        #endregion Public Constructors
    }
}