using GitCounter;
using System;
using System.Collections.Generic;

namespace GitReport.CLI
{
    class GitLogPresentation
    {
        Dictionary<string, ComponentData> componentManager;
        Dictionary<string, CommitData> commitManager;
        GitLogArguments gitArgument;
        GitLogErrors errorManager; 

        public GitLogPresentation(Dictionary<string, ComponentData> componentManager,
            Dictionary<string, CommitData> commitManager, 
            GitLogArguments gitArgument,
            GitLogErrors errorManager)
        {
            this.commitManager = commitManager;
            this.componentManager = componentManager;
            this.gitArgument = gitArgument;
            this.errorManager = errorManager;            
        }
        public void PresentReport()
        {
            if (componentManager.Count == 0)
            {
                errorManager.ShowDictionaryEmpty();
            }
            var writer = Console.Out;

            writer.WriteLine("CommitHash, CommiterName, CommitDate, CommitInfo");
            foreach (var commit in commitManager)
            {
                string date = DateTime.Parse(commit.Value.CommitDate).ToShortDateString();
                writer.WriteLine("{0},\t{1},\t{2},\t{3}", commit.Key, commit.Value.CommiterName,
                    date, commit.Value.CommitMessage);
            }

            writer.WriteLine("\n\n\nCommitHash, ComponentId, Insertion, Deletion");
            foreach (var component in componentManager)
            {
                writer.WriteLine("{0},\t{1},\t{2},\t{3}", component.Value.ComponentHash,
                    component.Value.ComponentId, component.Value.InsertionCounter,
                    component.Value.DeletionCounter);
            }
        }
    }
}
