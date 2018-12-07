using DigitalDias.Domain.Contracts;
using System.Xml.Linq;
using FI = System.IO;

namespace DigitalDias.Data.File
{
    public class TrsxLoader : ITrsxLoader
    {
        private readonly ILogger _logger;

        public TrsxLoader(ILogger logger)
        {
            _logger = logger;
        }

        public XElement Load(string fileName)
        {
            _logger.Enter(this, args: fileName);

            var fileContent = FI.File.ReadAllText(fileName);
            return XElement.Parse(fileContent);
        }
    }
}
