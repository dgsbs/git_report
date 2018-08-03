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

            ModificationCounters modArg = new ModificationCounters();
            modArg.AdditionCounter = CheckIfStringIsNumber(separatedGitDiffOutput[0]);
            modArg.DeletionCounter = CheckIfStringIsNumber(separatedGitDiffOutput[1]);

            if (jsonManager.TryMatchPaths(separatedGitDiffOutput[2], out componentNewId))
            {
                if (CheckIfKeyExists(componentNewId))
                {
                    dictionaryManager[componentNewId].AdditionCounter += modArg.AdditionCounter;
                    dictionaryManager[componentNewId].DeletionCounter += modArg.DeletionCounter;
                }
                else
                {
                    dictionaryManager.Add(componentNewId, modArg);
                }
            }
        }
        public Dictionary<string, ModificationCounters> CreateWholeReport(string wholeStdOut)
        {
            string[] outputLineByLine = wholeStdOut.Split('\n');
            for (int i = 0; i < outputLineByLine.Length - 1; i ++)                                                            
            {
                CreateReportForComponent(outputLineByLine[i]);
            }
            return dictionaryManager;
        }
        private bool CheckIfKeyExists(string temporaryKey)
        {
            if (dictionaryManager.ContainsKey(temporaryKey))
            {
                return true;
            }
            else
            {
                return false;
            }
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
