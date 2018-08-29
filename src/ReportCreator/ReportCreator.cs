using System;
using System.Collections.Generic;

namespace GitCounter
{
    public class ReportCreator
    {
        Dictionary<int, ComponentData> temporaryComponentManager;
        Dictionary<string, CommitData> commitManager;
        Dictionary<string, ComponentData> componentManager;
        IJsonConfig jsonConfig;
        public int IdNumber { get; set; }

        public ReportCreator(IJsonConfig jsonConfig, 
            Dictionary<string, ComponentData> componentManager,
            Dictionary<string, CommitData> commitManager)
        {
            this.componentManager = componentManager;
            this.commitManager = commitManager;
            this.temporaryComponentManager = new Dictionary<int, ComponentData>();
            this.jsonConfig = jsonConfig;
            this.IdNumber = 0;
        }
        public void CreateFullReport(string gitOutput)
        {
            string[] outputSeparator = new[] { "divideLine" };
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
            string[] commitSeparator = new[] { "smallLine" };
            string[] commitDivided = 
                fullCommit.Split(commitSeparator,StringSplitOptions.RemoveEmptyEntries);

            CreateCommitComponentData(commitDivided[0], commitDivided[1]);
        }
        private void CreateCommitComponentData(string commitData, string componentData)
        {
            string[] lineByLine = commitData.Split('\n');
            string commitHash = lineByLine[1];

            CommitData data = new CommitData
            {
                CommiterName = lineByLine[2],
                CommitDate = lineByLine[3],
                CommitMessage = lineByLine[4]
            };
            CreateReportAllComoponents(commitHash, componentData);
            commitManager.Add(commitHash, data);
        }
        private void CreateReportAllComoponents(string hash, string componentInfo)
        {
            string[] lineByLine = componentInfo.Split('\n');

            foreach (var line in lineByLine)
            {
                if (!String.IsNullOrWhiteSpace(line))
                {
                    CreateReportOneComponent(line, hash);
                }
            }
        }
        private void CreateReportOneComponent(string gitOutputLine, string hash)
        {
            var componentNewId = string.Empty;
            string[] separatedGitDiffOutput = 
                gitOutputLine.Split(new Char[] {'\t',' '}, StringSplitOptions.RemoveEmptyEntries);
                        
            if (jsonConfig.TryMatchPath(separatedGitDiffOutput[2], out componentNewId))
            {
                ComponentData componentHandler = new ComponentData
                {
                    ComponentHash = hash,
                    ComponentId = componentNewId,
                    InsertionCounter = CheckIfStringIsNumber(separatedGitDiffOutput[0]),
                    DeletionCounter = CheckIfStringIsNumber(separatedGitDiffOutput[1])
                };
                temporaryComponentManager.Add(IdNumber, componentHandler);
            }
            IdNumber++;
        }
        private void CreateFinalComponentList()
        {
            string newKey = string.Empty;

            foreach (var temporaryComponent in temporaryComponentManager)
            {
                if (TryMatchComponent(temporaryComponent, out newKey))
                {
                    componentManager[newKey].InsertionCounter +=
                        temporaryComponent.Value.InsertionCounter;
                    componentManager[newKey].DeletionCounter +=
                        temporaryComponent.Value.DeletionCounter;
                }
                else
                {
                    newKey = temporaryComponent.Value.ComponentHash + 
                        temporaryComponent.Value.ComponentId;
                    componentManager.Add(newKey, temporaryComponent.Value);
                }
            }
        }
        private bool TryMatchComponent(KeyValuePair<int, ComponentData> tempItem,
            out string newKey)
        {
            foreach (var dictionaryItem in componentManager)
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