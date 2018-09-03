using System;
using System.Collections.Generic;

namespace ReportCreator
{
    public class GitReportCreator
    {
        public Dictionary<ComponentKey, ComponentData> 
            ComponentDictionary { get; private set; }
        public List<CommitData> CommitList { get; private set; }
        IJsonConfig jsonConfig;
        public GitReportCreator(IJsonConfig jsonConfig)
        {
            this.ComponentDictionary = new Dictionary<ComponentKey, ComponentData>();
            this.CommitList = new List<CommitData>();
            this.jsonConfig = jsonConfig;
        }
        public void CreateFullReport(string gitOutput)
        {
            string[] outputSeparator = 
                new[] { this.jsonConfig.GetSeparator(JsonConfig.Separator.Output) };

            string[] commitByCommit =
                gitOutput.Split(outputSeparator, StringSplitOptions.RemoveEmptyEntries);

            string[] commitSeparator =
                new[] { this.jsonConfig.GetSeparator(JsonConfig.Separator.Commit) };
            string commitHash = string.Empty;

            foreach (var commit in commitByCommit)
            {
                string[] commitDivided =
                    commit.Split(commitSeparator, StringSplitOptions.RemoveEmptyEntries);

                AddCommitDataToList(commitDivided[0], out commitHash);
                AddCommitsComponentData(commitDivided[1], commitHash);
            }
        }
        private void AddCommitDataToList(string commitData, out string commitHash)
        {
            string[] lineByLine = commitData.Split('\n');

            CommitData data = new CommitData
            {
                CommitHash = lineByLine[1].Trim(),
                CommiterName = lineByLine[2].Trim(),
                CommitDate = lineByLine[3].Trim(),
                CommitMessage = lineByLine[4].Trim()
            };
            commitHash = data.CommitHash;
            CommitList.Add(data);
        }
        private void AddCommitsComponentData(string oneCommitData, string commitHash)
        {
            string[] lineByLine = oneCommitData.Split('\n');

            foreach (var line in lineByLine)
            {
                if (!String.IsNullOrWhiteSpace(line))
                {
                    AddEditComponentData(line.Trim(), commitHash);
                }
            }
        }
        private void AddEditComponentData(string oneLineData, string commitHash)
        {
            var componentNewId = string.Empty;
            ComponentKey existingKey = new ComponentKey();
            string[] oneLineSeparated = oneLineData.Split
                (new Char[] { '\t', ' ' },StringSplitOptions.RemoveEmptyEntries);

            if (this.jsonConfig.TryMatchPath(oneLineSeparated[2], out componentNewId))
            {
                ComponentKey componentKey = new ComponentKey
                {
                    CommitHash = commitHash,
                    ComponentId = componentNewId
                };
                if (TryMatchComponent(componentKey,out existingKey))
                {
                    ComponentDictionary[existingKey].InsertionCounter += 
                        GetNumberFromString(oneLineSeparated[0]);
                    ComponentDictionary[existingKey].DeletionCounter += 
                        GetNumberFromString(oneLineSeparated[1]);
                }
                else
                { 
                    ComponentData componentCounter = new ComponentData
                    {
                        InsertionCounter = GetNumberFromString(oneLineSeparated[0]),
                        DeletionCounter = GetNumberFromString(oneLineSeparated[1])
                    };
                    ComponentDictionary.Add(componentKey, componentCounter);
                }
            }
        }
        private bool TryMatchComponent(ComponentKey data, out ComponentKey existingKey)
        {
            existingKey = new ComponentKey();

            foreach (var dictionaryItem in ComponentDictionary)
            {
                if (dictionaryItem.Key.CommitHash == data.CommitHash &&
                    dictionaryItem.Key.ComponentId == data.ComponentId)
                {
                    existingKey = dictionaryItem.Key;
                    return true;
                }
            }
            return false;
        }
        private int GetNumberFromString(string numberOfChanges)
        {
            if (Int32.TryParse(numberOfChanges, out int parsedNumber))
            {
                return parsedNumber;
            }
            else
            {
                return 0;
            }
        }
    }
}