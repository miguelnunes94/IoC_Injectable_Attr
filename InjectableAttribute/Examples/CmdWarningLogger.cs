using System;
using InjectableAttribute;
using Microsoft.Extensions.DependencyInjection;

namespace Examples
{
    /// <summary>
    /// Console logger
    /// </summary>
    [Injectable(ServiceLifetime.Singleton, typeof(ILoggerWarnings))]
    public class CmdWarningLogger : ILoggerWarnings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CmdWarningLogger"/> class.
        /// </summary>
        public CmdWarningLogger()
        {
            Console.WriteLine("Initialized a new instance of the CmdWarningLogger class.");
        }

        public void LogWarning(string message)
        {
            Console.WriteLine($"Warning: {message}");
        }
    }
}