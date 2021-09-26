using System;
using InjectableAttribute;
using Microsoft.Extensions.DependencyInjection;

namespace Examples
{
    /// <summary>
    /// Console logger
    /// </summary>
    [Injectable(ServiceLifetime.Transient, typeof(ILoggerErrors))]
    public class CmdErrorLogger : ILoggerErrors
    {
        public CmdErrorLogger()
        {
            Console.WriteLine("Initialized a new instance of the CmdErrorLogger class.");
        }

        public void LogError(string message)
        {
            Console.WriteLine($"Error detected, blowing up with the system: {message}");
        }
    }
}