using DigitalDias.Domain.Contracts;
using DigitalDias.Domain;
using StructureMap;

namespace DigitalDias.DependenyInversion
{
    public class RuntimeRegistry : Registry
    {
        public RuntimeRegistry()
        {
            Scan(x =>
            {
                x.AssembliesAndExecutablesFromApplicationBaseDirectory();
                x.WithDefaultConventions();
            });

            For<ILogger>().Singleton().Use<ConsoleLogger>();
        }
    }
}
