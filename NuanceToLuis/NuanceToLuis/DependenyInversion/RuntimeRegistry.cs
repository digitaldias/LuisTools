using LuisTools.Domain.Contracts;
using LuisTools.Domain;
using StructureMap;

namespace LuisTools.DependenyInversion
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
