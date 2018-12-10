using LuisTools.Domain.Contracts;
using LuisTools.Domain.Entities;
using System.IO;
using System.Linq;
using System.Text;

namespace LuisTools.Business
{
    public class StatisticsGenerator : IStatisticsGenerator
    {
        public string Generate(FileStatistic stats)
        {
            var stringBuilder    = new StringBuilder("");
            var displayFileName  = new FileInfo(stats.FileName).Name.ToUpper()  +":";
            const int PAD_AMOUNT = 25;

            double reduction = ((double) stats.ResultingUtteranceCount / stats.ProcessedUtteranceCount);
            stringBuilder.Append("\n");
            stringBuilder.Append($"\n{displayFileName}");
            stringBuilder.Append($"\n{new string('-', displayFileName.Length)}");
            stringBuilder.Append($"\nUtterances found       : {stats.UtteranceCount.ToString("### ###").PadLeft(PAD_AMOUNT)}");
            stringBuilder.Append($"\nUtterances ignored     : {stats.IgnoredUtteranceCount.ToString("### ###").PadLeft(PAD_AMOUNT)}");
            stringBuilder.Append($"\nUtterances processed   : {stats.ProcessedUtteranceCount.ToString("### ###").PadLeft(PAD_AMOUNT)}");
            stringBuilder.Append($"\nUtterances reduced to  : {stats.ResultingUtteranceCount.ToString("### ###").PadLeft(PAD_AMOUNT)}");
            stringBuilder.Append($"\nReduction factor       : {reduction.ToString("### ##0.###").PadLeft(PAD_AMOUNT)}");
            stringBuilder.Append("\n");
            stringBuilder.Append($"\nIntents found          : {(stats.IgnoredIntentCount + stats.IntentCount).ToString("### ###").PadLeft(PAD_AMOUNT)}");
            stringBuilder.Append($"\nIntents ignored        : {stats.IgnoredIntentCount.ToString("### ###").PadLeft(PAD_AMOUNT)}");
            stringBuilder.Append($"\nTotal intents processed: {stats.IntentCount.ToString("### ###").PadLeft(PAD_AMOUNT)}");
            stringBuilder.Append($"\nIgnored intents        : {string.Join(',', stats.IgnoredIntents.Select(s => $"'{s}'")).PadLeft(PAD_AMOUNT)}");
            stringBuilder.Append("\n");
            stringBuilder.Append($"\nLines Written          : {stats.LinesWritten.ToString("### ###").PadLeft(PAD_AMOUNT)}");
            stringBuilder.Append("\n" + new string('-', PAD_AMOUNT*2));

            return stringBuilder.ToString();
        }
    }
}
