using DigitalDias.Domain.Contracts;
using DigitalDias.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DigitalDias.Business
{
    public class ParametersExtractor : IParametersExtractor
    {
        private readonly ILogger _logger;
        private readonly IArgumentValidator _argumentValidator;
        private readonly IFileFinder _filePatternHelper;

        public ParametersExtractor(ILogger logger, IArgumentValidator argumentValidator, IFileFinder filePatternHelper)
        {
            _logger            = logger;
            _argumentValidator = argumentValidator;
            _filePatternHelper = filePatternHelper;
        }

        public ProgramParameters Extract(params string[] args)
        {
            if (!_argumentValidator.IsValid(args))
                return null;

            var result = new ProgramParameters();

            for(int i = 0; i < args.Length; i++)
            {
                if(args[i].Equals("-h", StringComparison.InvariantCultureIgnoreCase) || args[i].Equals("--help", StringComparison.InvariantCultureIgnoreCase))
                {
                    result.DisplayHelp = true;
                    return result;
                }

                if(args[i].Equals("-i", StringComparison.InvariantCultureIgnoreCase) || args[i].Equals("--inputPattern", StringComparison.InvariantCultureIgnoreCase))
                {
                    result.FileType = Path.GetFileName(args[i+1]).Split('.')[1];
                    result.InputFilePattern = args[++i];
                    continue;
                }

                if(args[i].Equals("-o", StringComparison.InvariantCultureIgnoreCase) || args[i].Equals("--outputFolder", StringComparison.InvariantCultureIgnoreCase))
                {
                    result.DestinationFolderName = args[++i];
                    continue;
                }

                if(args[i].Equals("-f", StringComparison.InvariantCulture) || args[i].Equals("--filter", StringComparison.InvariantCultureIgnoreCase))
                {
                    result.IgnoredIntents = new List<string>(args[++i].Split(',').Select(s => s.Trim()));
                    continue;
                }
            }
            return result;
        }
    }
}
