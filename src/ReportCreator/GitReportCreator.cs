using System;
using System.Collections.Generic;

namespace ReportCreator
{
    public class GitReportCreator
    {
        private Dictionary<HashIdKey, AllDataFromCommit> completeDictionary;
        private Dictionary<HashIdKey, ComponentData> componentDictionary;
        private Dictionary<string, CommitData> commitDictionary;
        private IJsonConfig jsonConfig;
        public GitReportCreator(IJsonConfig jsonConfig)
        {
            this.completeDictionary = new Dictionary<HashIdKey, AllDataFromCommit>();
            this.componentDictionary = new Dictionary<HashIdKey, ComponentData>();
            this.commitDictionary = new Dictionary<string,CommitData>();
            this.jsonConfig = jsonConfig;
        }
        public Dictionary<HashIdKey, AllDataFromCommit> CreateCompleteDictionary(string gitOutput)
        {
            CollectCommitComponentData(gitOutput);

            foreach (var dictioanryItem in this.componentDictionary)
            {
                var allCommponents = new AllDataFromCommit()
                {
                    InsertionCounter = dictioanryItem.Value.InsertionCounter,
                    DeletionCounter = dictioanryItem.Value.DeletionCounter,
                    CommiterName = this.commitDictionary[dictioanryItem.Key.CommitHash].CommiterName,
                    CommitDate = this.commitDictionary[dictioanryItem.Key.CommitHash].CommitDate,
                    CommitMessage = this.commitDictionary[dictioanryItem.Key.CommitHash].CommitMessage
                };
                this.completeDictionary.Add(dictioanryItem.Key, allCommponents);
            }
            return this.completeDictionary;
        }
        private void CollectCommitComponentData(string gitOutput)
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

                commitHash = AddCommitDataToDictionary(commitDivided[0]);
                AddCommitsComponentData(commitDivided[1], commitHash);
            }
        }
        private string AddCommitDataToDictionary(string commitData)
        {
            var lineByLine = commitData.Split('\n');
            var commitHash = lineByLine[1].Trim();
            var data = new CommitData
            {
                CommiterName = lineByLine[2].Trim(),
                CommitDate = lineByLine[3].Trim(),
                CommitMessage = lineByLine[4].Trim()
            };
            this.commitDictionary.Add(commitHash,data);
            return commitHash;
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
            var existingKey = new HashIdKey();
            var oneLineSeparated = oneLineData.Split
                (new Char[] { '\t', ' ' },StringSplitOptions.RemoveEmptyEntries);

            if (this.jsonConfig.TryMatchPath(oneLineSeparated[2], out componentNewId))
            {
                var componentKey = new HashIdKey
                {
                    CommitHash = commitHash,
                    ComponentId = componentNewId
                };
                if (TryMatchComponent(componentKey,out existingKey))
                {
                    this.componentDictionary[existingKey].InsertionCounter += 
                        GetNumberFromString(oneLineSeparated[0]);
                    this.componentDictionary[existingKey].DeletionCounter += 
                        GetNumberFromString(oneLineSeparated[1]);
                }
                else
                { 
                    var componentCounter = new ComponentData
                    {
                        InsertionCounter = GetNumberFromString(oneLineSeparated[0]),
                        DeletionCounter = GetNumberFromString(oneLineSeparated[1])
                    };
                    this.componentDictionary.Add(componentKey, componentCounter);
                }
            }
        }
        private bool TryMatchComponent(HashIdKey data, out HashIdKey existingKey)
        {
            existingKey = new HashIdKey();

            foreach (var dictionaryItem in this.componentDictionary)
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