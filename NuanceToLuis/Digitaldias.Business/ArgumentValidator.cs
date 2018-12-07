using DigitalDias.Domain.Contracts;
using System.Linq;

namespace DigitalDias.Business
{
    public class ArgumentValidator : IArgumentValidator
    {
        private readonly ILogger _log;

        public ArgumentValidator(ILogger logger)
        {
            _log = logger;
        }

        public bool IsValid(string[] args)
        {
            _log.Enter(this, args: args);

            if (!args.Any())
            {
                _log.Warning("No arguments given");
                return false;
            }

            if(!args.Contains("-i") && !args.Contains("--inputPattern"))
            {
                _log.Warning("No input pattern parameter detected");
                return false;
            }

            if(!args.Contains("-o") && !args.Contains("--outputFolder"))
            {
                _log.Warning("No outputFolder detected");
                return false;
            }

            return true;
        }
    }
}
