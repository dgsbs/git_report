using System;
using System.Collections.Generic;

namespace ReportCreator
{
    public class GitReportCreator
    {
        private Dictionary<HashId, ComponentData> componentDictionary;
        private Dictionary<string, CommitData> commitDictionary;
        private IJsonConfig jsonConfig;
        public GitReportCreator(IJsonConfig jsonConfig)
        {
            this.componentDictionary = new Dictionary<HashId, ComponentData>();
            this.commitDictionary = new Dictionary<string,CommitData>();
            this.jsonConfig = jsonConfig;
        }
        public Dictionary<HashId, CommitComponentData> GetReportDictionary(string gitOutput)
        {
            CollectCommitComponentData(gitOutput);

            var completeDictionary = new Dictionary<HashId, CommitComponentData>();

            foreach (var dictionaryItem in this.componentDictionary)
            {
                var allCommponents = new CommitComponentData()
                {
                    ComponentInsertions = dictionaryItem.Value.ComponentInsertions,
                    ComponentDeletions = dictionaryItem.Value.ComponentDeletions,
                    CommiterName = 
                        this.commitDictionary[dictionaryItem.Key.CommitHash].CommiterName,
                    CommitDate = 
                        this.commitDictionary[dictionaryItem.Key.CommitHash].CommitDate,
                    CommitMessage = 
                        this.commitDictionary[dictionaryItem.Key.CommitHash].CommitMessage
                };
                completeDictionary.Add(dictionaryItem.Key, allCommponents);
            }
            return completeDictionary;
        }
        private void CollectCommitComponentData(string gitOutput)
        {
            var outputSeparator =
                new[] { this.jsonConfig.GetSeparator(Separator.Output) };

            var commitByCommit =
                gitOutput.Split(outputSeparator, StringSplitOptions.RemoveEmptyEntries);

            var commitSeparator =
                new[] { this.jsonConfig.GetSeparator(Separator.Commit) };
            var commitHash = string.Empty;

            foreach (var commit in commitByCommit)
            {
                var commitDivided =
                    commit.Split(commitSeparator, StringSplitOptions.RemoveEmptyEntries);

                commitHash = AddCommitData(commitDivided[0]);
                AddCommitsComponents(commitDivided[1], commitHash);
            }
        }
        private string AddCommitData(string commitData)
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
        private void AddCommitsComponents(string oneCommitData, string commitHash)
        {
            var lineByLine = oneCommitData.Split('\n');

            foreach (var line in lineByLine)
            {
                if (!String.IsNullOrWhiteSpace(line))
                {
                    AddEditComponent(line.Trim(), commitHash);
                }
            }
        }
        private void AddEditComponent(string oneLineData, string commitHash)
        {
            var componentNewId = string.Empty;
            var existingKey = new HashId();
            var oneLineSeparated = oneLineData.Split
                (new Char[] { '\t', ' ' },StringSplitOptions.RemoveEmptyEntries);

            if (this.jsonConfig.TryMatchPath(oneLineSeparated[2], out componentNewId))
            {
                var componentKey = new HashId
                {
                    CommitHash = commitHash,
                    ComponentId = componentNewId
                };
                if (TryMatchComponent(componentKey,out existingKey))
                {
                    this.componentDictionary[existingKey].ComponentInsertions += 
                        GetNumberFromString(oneLineSeparated[0]);
                    this.componentDictionary[existingKey].ComponentDeletions += 
                        GetNumberFromString(oneLineSeparated[1]);
                }
                else
                { 
                    var component = new ComponentData
                    {
                        ComponentInsertions = GetNumberFromString(oneLineSeparated[0]),
                        ComponentDeletions = GetNumberFromString(oneLineSeparated[1])
                    };
                    this.componentDictionary.Add(componentKey, component);
                }
            }
        }
        private bool TryMatchComponent(HashId data, out HashId existingKey)
        {
            existingKey = new HashId();

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