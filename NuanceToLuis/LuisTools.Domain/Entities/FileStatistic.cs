using System.Collections.Generic;

namespace LuisTools.Domain.Entities
{
    public class FileStatistic
    {
        public string FileName { get; set; }

        public int LinesWritten { get; set; }

        public int IntentCount { get; set; }

        public int IgnoredIntentCount { get; set; }

        public int UtteranceCount { get; set; }

        public int IgnoredUtteranceCount { get; set; }

        public int ProcessedUtteranceCount { get; set; }

        public List<string> IgnoredIntents { get; set; }
        public int ResultingUtteranceCount { get; set; }
    }
}
