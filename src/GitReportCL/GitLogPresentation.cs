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
            if (this.reportHandler.ComponentDictionary.Count == 0)
            {
                Console.WriteLine("Json configuration file was not found. " +
                    "Check if it is in application folder.");
            }
            var writer = Console.Out;

            writer.WriteLine("CommitHash, CommiterName, CommitDate, CommitInfo");
            foreach (var commit in this.reportHandler.CommitDictionary)
            {
                string date = DateTime.Parse(commit.Value.CommitDate).ToShortDateString();
                writer.WriteLine("{0},\t{1},\t{2},\t{3}", commit.Key, commit.Value.CommiterName,
                    date, commit.Value.CommitMessage);
            }

            writer.WriteLine("\n\n\nCommitHash, ComponentId, Insertion, Deletion");
            foreach (var component in this.reportHandler.ComponentDictionary)
            {
                writer.WriteLine("{0},\t{1},\t{2},\t{3}", component.Value.ComponentHash,
                    component.Value.ComponentId, component.Value.InsertionCounter,
                    component.Value.DeletionCounter);
            }
        }
    }
}
