using System;
using System.Collections.Generic;
namespace GitReport.CLI
{
    class ReportCreator
    {
        Dictionary<string, ModificationCounters> dictionaryManager = 
            new Dictionary<string, ModificationCounters>();

        JsonConfig jsonManager = new JsonConfig();
        private void CreateReportForComponent(string oneLineFromStdOut)
        {
            var componentNewId = string.Empty;
            string[] separatedGitDiffOutput = oneLineFromStdOut.Split('\t');

            ModificationCounters counterHandler = new ModificationCounters
            {
                AdditionCounter = CheckIfStringIsNumber(separatedGitDiffOutput[0]),
                DeletionCounter = CheckIfStringIsNumber(separatedGitDiffOutput[1])
            };

            if (jsonManager.TryMatchPaths(separatedGitDiffOutput[2], out componentNewId))
            {
                if (dictionaryManager.ContainsKey(componentNewId)) 
                {
                    dictionaryManager[componentNewId].AdditionCounter += counterHandler.AdditionCounter;
                    dictionaryManager[componentNewId].DeletionCounter += counterHandler.DeletionCounter;
                }
                else
                {
                    dictionaryManager.Add(componentNewId, counterHandler);
                }
            }
        }
        public Dictionary<string, ModificationCounters> CreateReport(string gitOutput)
        {
            string[] outputLineByLine = gitOutput.Split('\n');
            foreach (var line in outputLineByLine)
            {
                if (!String.IsNullOrWhiteSpace(line))
                {
                    CreateReportForComponent(line);
                }
            }
            return dictionaryManager;
        }
        private int CheckIfStringIsNumber(string numberOfChanges)
        {
            if (Int32.TryParse(numberOfChanges, out int tempForFormatCheck))
            {
                return tempForFormatCheck;
            }
            else
            {
                return 0;
            }
        }
    }
}
