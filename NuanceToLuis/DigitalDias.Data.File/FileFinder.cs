using DigitalDias.Domain.Contracts;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DigitalDias.Data.File
{
    public class FileFinder : IFileFinder
    {
        private readonly ILogger _log;

        public FileFinder(ILogger log)
        {
            _log = log;
        }

        public IEnumerable<string> Find(string pattern)
        {
            _log.Enter(this, args: pattern);

            var folder      = Path.GetDirectoryName(pattern);
            var filePattern = Path.GetFileName(pattern);

            var foundFiles = System.IO.Directory.GetFiles(folder, filePattern);
            _log.Info($"Found {foundFiles.Count()} file(s) matching the specified pattern.");
            return foundFiles;
        }


        public string GetFolderName(string outputFolderName)
        {
            return Path.GetDirectoryName(outputFolderName);
        }
    }
}
