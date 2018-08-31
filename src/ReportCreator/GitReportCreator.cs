using System;
using System.Collections.Generic;

namespace ReportCreator
{
    public class GitReportCreator
    {
        public Dictionary<string, CommitData> CommitDictionary { get; private set; }
        public Dictionary<string, ComponentData> ComponentDictionary { get; private set; }
        private Dictionary<int, ComponentData> TemporaryContainer { get; set; }
        IJsonConfig jsonConfig;
        private int IdNumber { get; set; }
        public GitReportCreator(IJsonConfig jsonConfig, 
            Dictionary<string, ComponentData> componentDictionary,
            Dictionary<string, CommitData> commitDictionary)
        {
            this.ComponentDictionary = componentDictionary;
            this.CommitDictionary = commitDictionary;
            this.TemporaryContainer = new Dictionary<int, ComponentData>();
            this.jsonConfig = jsonConfig;
            this.IdNumber = 0;
        }
        public void CreateFullReport(string gitOutput)
        {
            string[] outputSeparator = new[]
                { this.jsonConfig.FetchSepatator(true).Trim() };
            string[] commitByCommit = gitOutput.Split(outputSeparator,
                StringSplitOptions.RemoveEmptyEntries);

            foreach (var commit in commitByCommit)
            {
                DivideCommit(commit);
            }
            
            CreateFinalComponentList();
        }
        private void DivideCommit(string fullCommit)
        {
            string[] commitSeparator = new[]
                { this.jsonConfig.FetchSepatator(false) };

            string[] commitDivided = 
                fullCommit.Split(commitSeparator,StringSplitOptions.RemoveEmptyEntries);

            CreateCommitComponentData(commitDivided[0], commitDivided[1]);
        }
        private void CreateCommitComponentData(string commitData, string componentData)
        {
            string[] lineByLine = commitData.Split('\n');
            string commitHash = lineByLine[1].Trim();

            CommitData data = new CommitData
            {
                CommiterName = lineByLine[2].Trim(),
                CommitDate = lineByLine[3].Trim(),
                CommitMessage = lineByLine[4].Trim()
            };
            CreateReportAllComponents(commitHash, componentData);
            this.CommitDictionary.Add(commitHash, data);
        }
        private void CreateReportAllComponents(string hash, string componentInfo)
        {
            string[] lineByLine = componentInfo.Split('\n');

            foreach (var line in lineByLine)
            {
                if (!String.IsNullOrWhiteSpace(line))
                {
                    CreateReportOneComponent(line.Trim(), hash);
                }
            }
        }
        private void CreateReportOneComponent(string gitOutputLine, string hash)
        {
            var componentNewId = string.Empty;
            string[] separatedGitDiffOutput = 
                gitOutputLine.Split(new Char[] {'\t',' '}, 
                StringSplitOptions.RemoveEmptyEntries);
                        
            if (this.jsonConfig.TryMatchPath(separatedGitDiffOutput[2], out componentNewId))
            {
                ComponentData componentHandler = new ComponentData
                {
                    ComponentHash = hash,
                    ComponentId = componentNewId,
                    InsertionCounter = CheckIfStringIsNumber(separatedGitDiffOutput[0]),
                    DeletionCounter = CheckIfStringIsNumber(separatedGitDiffOutput[1])
                };
                this.TemporaryContainer.Add(this.IdNumber, componentHandler);
            }
            this.IdNumber++;
        }
        private void CreateFinalComponentList()
        {
            string newKey = string.Empty;

            foreach (var temporaryComponent in this.TemporaryContainer)
            {
                if (TryMatchComponent(temporaryComponent, out newKey))
                {
                    ComponentDictionary[newKey].InsertionCounter +=
                        temporaryComponent.Value.InsertionCounter;
                    ComponentDictionary[newKey].DeletionCounter +=
                        temporaryComponent.Value.DeletionCounter;
                }
                else
                {
                    newKey = temporaryComponent.Value.ComponentHash + 
                        temporaryComponent.Value.ComponentId;
                    ComponentDictionary.Add(newKey, temporaryComponent.Value);
                }
            }
        }
        private bool TryMatchComponent(KeyValuePair<int, ComponentData> tempItem,
            out string newKey)
        {
            foreach (var dictionaryItem in ComponentDictionary)
            {
                if (dictionaryItem.Value.ComponentHash == tempItem.Value.ComponentHash &&
                    dictionaryItem.Value.ComponentId == tempItem.Value.ComponentId)
                {
                    newKey = tempItem.Value.ComponentHash + tempItem.Value.ComponentId;
                    return true;
                }
            }
            newKey = string.Empty;
            return false;
        }
        private int CheckIfStringIsNumber(string numberOfChanges)
        {
            if (Int32.TryParse(numberOfChanges, out int formatCheck))
            {
                return formatCheck;
            }
            else
            {
                return 0;
            }
        }
    }
}