using System;
using ReportCreator;
using System.Collections.Generic;

namespace GitReport.CLI
{
    class GitLogPresentation
    {
        public void PresentReport(in List<CommitData> commitList, 
            in Dictionary<ComponentKey, ComponentData> componentDictionary)
        {
            if (commitList.Count == 0 && componentDictionary.Count == 0)
            {
                Console.WriteLine("Json configuration file was not found. " +
                    "Check if it is in application folder.");
            }
            var writer = Console.Out;

            writer.WriteLine("CommitHash, CommiterName, CommitDate, CommitInfo");
            foreach (var commit in commitList)
            {
                var date = DateTime.Parse(commit.CommitDate).ToShortDateString();
               
                writer.WriteLine($"{commit.CommitHash},\t{commit.CommiterName}," +
                    $"\t{date},\t{commit.CommitMessage}");
            }

            writer.WriteLine("\n\n\nCommitHash, ComponentId, Insertion, Deletion");
            foreach (var component in componentDictionary)
            {
                writer.WriteLine($"{component.Key.CommitHash},\t{component.Key.ComponentId}," +
                    $"\t{component.Value.InsertionCounter},\t{component.Value.DeletionCounter}");
            }
        }
    }
}
