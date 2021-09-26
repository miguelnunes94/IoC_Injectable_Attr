using InjectableAttribute.ServiceCollection;
using Microsoft.Extensions.DependencyInjection;

namespace Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            // init service collection and register all services using Injectable attribute
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddInjectables();

            // build service provider
            using var services = serviceCollection.BuildServiceProvider();


            // tests:

            var errorLogger = services.GetService<ILoggerErrors>();
            var errorLogger2 = services.GetService<ILoggerErrors>();

            var warningLogger = services.GetService<ILoggerWarnings>();
            var warningLogger2 = services.GetService<ILoggerWarnings>();

            //Output (ILoggerWarnings is registered as singleton):

            //Initialized a new instance of the CmdErrorLogger class.
            //Initialized a new instance of the CmdErrorLogger class.
            //Initialized a new instance of the CmdWarningLogger class.

        }
    }
}
