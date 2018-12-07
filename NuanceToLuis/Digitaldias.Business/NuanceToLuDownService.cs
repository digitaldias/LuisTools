using DigitalDias.Domain.Contracts;
using DigitalDias.Domain.Entities;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalDias.Business
{
    public class NuanceToLuDownService : INuanceToLuDownService
    {
        private readonly ILogger              _log;
        private readonly ILuDownMaker         _luDownMaker;
        private readonly ITrsxLoader          _trsxLoader;
        private readonly IParametersExtractor _parametersExtractor;
        private readonly IFileFinder          _fileFinder;
        private Stopwatch                     _stopWatch;

        public NuanceToLuDownService(ILogger logger, ILuDownMaker luDownMaker, ITrsxLoader trsxLoader, IParametersExtractor parametersExtractor, IFileFinder fileFinder)
        {
            _log                 = logger;
            _luDownMaker         = luDownMaker;
            _trsxLoader          = trsxLoader;
            _parametersExtractor = parametersExtractor;
            _fileFinder          = fileFinder;
        }

        public void Process(string[] args)
        {
            _stopWatch = Stopwatch.StartNew();
            _log.Enter(this, args: args);

            ProgramParameters parameters = ValidateArgsAndExtractProgramParameters(args);
            if (parameters == null)
                return;

            var filePaths = _fileFinder.Find(parameters.InputFilePattern);
            if (!filePaths.Any())
                return;

            var finalStatistics = ProcessEachFileInParallel(parameters, filePaths);

            foreach (var statistic in finalStatistics)
            {
                _log.Info(statistic);
            }
            _log.Info($"Program completed in {_stopWatch.ElapsedMilliseconds}ms.");
        }

        private ConcurrentBag<string> ProcessEachFileInParallel(ProgramParameters parameters, System.Collections.Generic.IEnumerable<string> filePaths)
        {
            var _statisticsBag = new ConcurrentBag<string>();

            Parallel.ForEach(filePaths, filePath =>
            {
                switch (parameters.FileType.ToLower())
                {
                    case "trsx": _statisticsBag.Add(ProcessTrsxModel(filePath, parameters)); break;
                    default: _log.Warning($"Unrecognized file format: '{filePath}'"); break;
                };
            });
            return _statisticsBag;
        }

        private ProgramParameters ValidateArgsAndExtractProgramParameters(string[] args)
        {
            var parameters = _parametersExtractor.Extract(args);
            if (parameters == null || parameters.DisplayHelp)
            {
                DisplayHelp();
                return null;
            }
            return parameters;
        }

        private string ProcessTrsxModel(string sourceFileName, ProgramParameters parameters)
        {
            var project = _trsxLoader.Load(sourceFileName);

            return _luDownMaker.MakeFromTrsx(sourceFileName, project, parameters);
        }

        private void DisplayHelp()
        {
            _log.Info("");
            _log.Info("Program Arguments:");
            _log.Info("");
            _log.Info("\t-i | --inputPattern <*.trsx>        - Specify File input pattern, absolute or relative");
            _log.Info("\t-o | --outputFolder <folderSpec>    - Destination folder for the generated *.lu files ");
            _log.Info("\t-f | --filter \"[INTENT1,INTENT2]\"   - List of intents to ignore                       ");
            _log.Info("");
            _log.Info("Example:\n");
            _log.Info("\tdotnet NuanceToLuis.dll -i *.trsx -o C:\\temp\\luDownFiles -f \"NO_INTENT, GLOBAL_*\" \n");
            _log.Info("The above example takes all .trsx files in the current folder and produces corresponding");
            _log.Info(".lu files in the folder 'C:\\temp\\luDownFiles'. Intents named 'NO_INTENT' or intents ");
            _log.Info("starting with 'GLOBAL_' will be ignored.");
        }
    }
}
