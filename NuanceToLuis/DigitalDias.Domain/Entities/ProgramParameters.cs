using System.Collections.Generic;

namespace DigitalDias.Domain.Entities
{
    public class ProgramParameters
    {
        public string InputFilePattern { get; set; }

        public string DestinationFolderName { get; set; }

        public List<string> IgnoredIntents { get; set; }
        public bool DisplayHelp { get; set; }
        public string FileType { get; set; }
    }
}
