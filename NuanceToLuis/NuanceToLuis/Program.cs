using DigitalDias.DependenyInversion;
using DigitalDias.Domain.Contracts;
using DigitalDias.Domain.Entities;
using StructureMap;
using System;

namespace DigitalDias
{
    class Program
    {
        private static Container container = new Container(new RuntimeRegistry());

        static void Main(string[] args)
        {
            Console.WriteLine("NuanceToLuis Tool v1.0α");
            Console.WriteLine("----------------------------");

            ConfigureLogger();
            container.GetInstance<INuanceToLuDownService>().Process(args);
        }

        private static void ConfigureLogger()
        {
            var logger       = container.GetInstance<ILogger>();
            logger.Level     = LogLevel.Information;
            logger.LogAction = (s) => Console.WriteLine(s);
        }
    }
}
