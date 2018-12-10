using LuisTools.Domain.Contracts;
using System.IO;
using System.Xml.Linq;

namespace LuisTools.Data.Disk
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

            var fileContent = File.ReadAllText(fileName);
            return XElement.Parse(fileContent);
        }
    }
}
