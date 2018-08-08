﻿using System;
using System.Collections.Generic;
namespace GitCounter
{
    public class ReportCreator
    {
        Dictionary<string, ModificationCounters> dictionaryManager = new Dictionary<string, ModificationCounters>();

        IJsonConfig jsonManager;
        public ReportCreator(IJsonConfig jsonConfig)
        {
            this.jsonManager = jsonConfig;
        }
        private void CreateReportForComponent(string gitOutputline)
        {
            var componentNewId = string.Empty;
            string[] separatedGitDiffOutput = gitOutputline.Split(new Char[] { '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (jsonManager.TryMatchPath(separatedGitDiffOutput[2], out componentNewId))
            {
                ModificationCounters counterHandler = new ModificationCounters
                {
                    InsertionCounter = CheckIfStringIsNumber(separatedGitDiffOutput[0]),
                    DeletionCounter = CheckIfStringIsNumber(separatedGitDiffOutput[1])
                };

                if (dictionaryManager.ContainsKey(componentNewId))
                {
                    dictionaryManager[componentNewId].InsertionCounter += counterHandler.InsertionCounter;
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
            string[] LineByLine = gitOutput.Split('\n');

            foreach (var line in LineByLine)
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
            if (Int32.TryParse(numberOfChanges, out int FormatCheck))
            {
                return FormatCheck;
            }
            else
            {
                return 0;
            }
        }
    }
}