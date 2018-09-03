using System;
using ReportCreator;

namespace GitReport.CLI
{
    class GitLogPresentation
    {
        GitReportCreator reportHandler;
        public GitLogPresentation(GitReportCreator reportHandler)
        {
            this.reportHandler = reportHandler;
        }
        public void PresentReport()
        {
            if (this.reportHandler.CommitList.Count == 0 &&
                this.reportHandler.ComponentDictionary.Count == 0)
            {
                Console.WriteLine("Json configuration file was not found. " +
                    "Check if it is in application folder.");
            }
            var writer = Console.Out;

            writer.WriteLine("CommitHash, CommiterName, CommitDate, CommitInfo");
            foreach (var commit in this.reportHandler.CommitList)
            {
                string date = DateTime.Parse(commit.CommitDate).ToShortDateString();
               
                writer.WriteLine($"{commit.CommitHash},\t{commit.CommiterName}," +
                    $"\t{date},\t{commit.CommitMessage}");
            }

            writer.WriteLine("\n\n\nCommitHash, ComponentId, Insertion, Deletion");
            foreach (var component in this.reportHandler.ComponentDictionary)
            {
                writer.WriteLine($"{component.Key.CommitHash},\t{component.Key.ComponentId}," +
                    $"\t{component.Value.InsertionCounter},\t{component.Value.DeletionCounter}");
            }
        }
    }
}
