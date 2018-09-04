using System;
using System.Collections.Generic;

namespace ReportCreator
{
    public class GitReportCreator
    {
        public Dictionary<ComponentKey, ComponentData> 
            ComponentDictionary { get; private set; }
        public List<CommitData> CommitList { get; private set; }
        private IJsonConfig jsonConfig;
        public GitReportCreator(IJsonConfig jsonConfig)
        {
            this.ComponentDictionary = new Dictionary<ComponentKey, ComponentData>();
            this.CommitList = new List<CommitData>();
            this.jsonConfig = jsonConfig;
        }
        public void CreateFullReport(string gitOutput, out List<CommitData> outCommitList, 
            out Dictionary<ComponentKey, ComponentData> outComponentDictionary)
        {
            var outputSeparator = 
                new[] { this.jsonConfig.GetSeparator(JsonConfig.Separator.Output) };

            var commitByCommit =
                gitOutput.Split(outputSeparator, StringSplitOptions.RemoveEmptyEntries);

            var commitSeparator =
                new[] { this.jsonConfig.GetSeparator(JsonConfig.Separator.Commit) };
            var commitHash = string.Empty;

            foreach (var commit in commitByCommit)
            {
                var commitDivided =
                    commit.Split(commitSeparator, StringSplitOptions.RemoveEmptyEntries);

                commitHash = AddCommitDataToList(commitDivided[0]);
                AddCommitsComponentData(commitDivided[1], commitHash);
            }
            outCommitList = CommitList;
            outComponentDictionary = ComponentDictionary;
        }
        private string AddCommitDataToList(string commitData)
        {
            var lineByLine = commitData.Split('\n');

            var data = new CommitData
            {
                CommitHash = lineByLine[1].Trim(),
                CommiterName = lineByLine[2].Trim(),
                CommitDate = lineByLine[3].Trim(),
                CommitMessage = lineByLine[4].Trim()
            };
            CommitList.Add(data);
            return data.CommitHash;
        }
        private void AddCommitsComponentData(string oneCommitData, string commitHash)
        {
            var lineByLine = oneCommitData.Split('\n');

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
            var existingKey = new ComponentKey();
            var oneLineSeparated = oneLineData.Split
                (new Char[] { '\t', ' ' },StringSplitOptions.RemoveEmptyEntries);

            if (this.jsonConfig.TryMatchPath(oneLineSeparated[2], out componentNewId))
            {
                var componentKey = new ComponentKey
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
                    var componentCounter = new ComponentData
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
                if (dictionaryItem.Key.Equals(data))
                {
                    existingKey = dictionaryItem.Key;
                    return true;
                }
            }
            return false;
        }
        private int GetNumberFromString(string numberOfChanges)
        {
            if (Int32.TryParse(numberOfChanges, out int parsedString))
            {
                return parsedString;
            }
            else
            {
                return 0;
            }
        }
    }
}