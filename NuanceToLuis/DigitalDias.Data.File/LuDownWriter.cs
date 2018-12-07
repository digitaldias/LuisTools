using DigitalDias.Domain.Contracts;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FI = System.IO;

namespace DigitalDias.Data.File
{
    public class LuDownWriter : ILuDownWriter
    {
        private readonly ILogger _log;

        public LuDownWriter(ILogger logger)
        {
            _log = logger;
        }

        public int Write(string outputFolder, string originalFileName, List<string> linesToWrite)
        {
            _log.Enter(this);

            var fileInfo            = new FI.FileInfo(originalFileName);
            var destinationFileName = fileInfo.Name.Split('.')[0] + ".lu";
            var finalFilePath       = Path.Combine(outputFolder, destinationFileName);

            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }
            FI.File.WriteAllLines(finalFilePath, linesToWrite);

            return linesToWrite.Count();
        }
    }
}
