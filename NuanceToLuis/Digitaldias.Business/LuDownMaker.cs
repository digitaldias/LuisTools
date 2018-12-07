using DigitalDias.Domain.Contracts;
using DigitalDias.Domain.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace DigitalDias.Business
{
    public class LuDownMaker : ILuDownMaker
    {
        private readonly ILogger              _log;
        private readonly ILuDownWriter        _luDownWriter;
        private readonly IExceptionHandler    _exceptionHandler;
        private readonly IFileFinder          _fileFinder;
        private readonly IStatisticsGenerator _statisticsGenerator;

        public LuDownMaker(ILogger logger, ILuDownWriter luDownWriter, IExceptionHandler exceptionHandler, IFileFinder fileFinder, IStatisticsGenerator statisticsGenerator)
        {
            _log                 = logger;
            _luDownWriter        = luDownWriter;
            _exceptionHandler    = exceptionHandler;
            _fileFinder          = fileFinder;
            _statisticsGenerator = statisticsGenerator;
        }

        public string MakeFromTrsx(string fileName, XElement trsxElement, ProgramParameters parameters)
        {
            _log.Enter(this);

            var intents           = new Dictionary<string, HashSet<EntityBasedUtterance>>();
            var allUtterances     = trsxElement.Descendants("sample");  // Utterances are named "samples" in Nuance
            var lines             = new List<string>();
            var reducedUtterances = allUtterances;
            var stats             = new FileStatistic { FileName = new FileInfo(fileName).Name, IgnoredIntents = new List<string>() };
            stats.UtteranceCount  = trsxElement.Descendants("sample").Count();
            
            reducedUtterances = RemoveUtterancesForIgnoredIntents(parameters, reducedUtterances, stats);

            foreach (var utterance in reducedUtterances)
            {
                stats.ProcessedUtteranceCount += ExtractEntityBasedUtteranceFromXElement(intents, utterance);
            }
            stats.IntentCount = intents.Keys.Count();

            foreach (var key in intents.Keys)
            {
                stats.ResultingUtteranceCount += intents[key].Count();
            }
            stats.LinesWritten = _exceptionHandler.Get(() => WriteIntentsToLudownFile(parameters.DestinationFolderName, fileName, intents));

            return _statisticsGenerator.Generate(stats);
        }

        private static IEnumerable<XElement> RemoveUtterancesForIgnoredIntents(ProgramParameters parameters, IEnumerable<XElement> reducedUtterances, FileStatistic stats)
        {
            if (parameters.IgnoredIntents != null && parameters.IgnoredIntents.Any())
            {
                foreach (var ignoredIntent in parameters.IgnoredIntents)
                {
                    stats.IgnoredIntentCount++;
                    stats.IgnoredUtteranceCount += reducedUtterances.Count(s => !s.Attribute("intentref").Value.StartsWith(ignoredIntent));
                    stats.IgnoredIntents.Add(ignoredIntent);

                    reducedUtterances = reducedUtterances.Where(s => !s.Attribute("intentref").Value.StartsWith(ignoredIntent));
                }
            }

            return reducedUtterances;
        }

        private static int ExtractEntityBasedUtteranceFromXElement(Dictionary<string, HashSet<EntityBasedUtterance>> intents, XElement utteranceXElement)
        {
            var intentName          = string.Empty;
            var searchableTextParts = new List<string>();
            var entityStrippedParts = new List<string>();
            var entityMarkedParts   = new List<string>();

            if (utteranceXElement.HasElements)
            {
                // "intentref" is the intent name in Nuance
                if (!utteranceXElement.HasAttributes || utteranceXElement.Attribute("intentref") == null)
                    return 0;

                intentName               = utteranceXElement.Attribute("intentref").Value;
                var entityBasedUtterance = new EntityBasedUtterance();
                var currentNode          = utteranceXElement.FirstNode;
                do
                {
                    if (currentNode.NodeType == System.Xml.XmlNodeType.Text)
                    {
                        searchableTextParts.Add(currentNode.ToString());
                        entityStrippedParts.Add(currentNode.ToString());
                        entityMarkedParts.Add(currentNode.ToString());
                    }
                    else if (currentNode.NodeType == System.Xml.XmlNodeType.Element)
                    {
                        var element = currentNode as XElement;
                        if (element.Name == "annotation")
                        {
                            // "conceptref" -->  "EntityName"
                            var entityName = element.Attribute("conceptref").Value;
                            var entityValue = element.Value;

                            searchableTextParts.Add(entityValue);
                            entityMarkedParts.Add($"{{{entityName}={entityValue}}}");
                        }
                    }
                    currentNode = currentNode.NextNode;
                } while (currentNode != null);

                entityBasedUtterance.SearchableText     = string.Join(' ', searchableTextParts).Replace(" ?", "?");
                entityBasedUtterance.EntityMarkedText   = "- " + string.Join(' ', entityMarkedParts).Replace(" ?", "?");
                entityBasedUtterance.EntityStrippedText = string.Join(' ', entityStrippedParts).Replace(" ?", "?");

                if(!intents.ContainsKey(intentName))
                    intents[intentName] = new HashSet<EntityBasedUtterance>(new EntityBasedUtteranceComparer());

                intents[intentName].Add(entityBasedUtterance);
                return 1;
            }
            return 0;
        }

        private int WriteIntentsToLudownFile(string folderName, string sourceFileName, Dictionary<string, HashSet<EntityBasedUtterance>> intents)
        {
            var linesToWrite = new List<string>();

            foreach (var intentName in intents.Keys)
            {
                linesToWrite.Add($"# {intentName}");
                linesToWrite.AddRange(intents[intentName].Select(v => v.EntityMarkedText));
                linesToWrite.Add("");
            }
            
            return _luDownWriter.Write(folderName, sourceFileName, linesToWrite);
        }
    }
}
