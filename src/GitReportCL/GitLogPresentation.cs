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
            GitLogArguments gitArgument)
        {
            this.commitManager = commitManager;
            this.componentManager = componentManager;
            this.gitArgument = gitArgument;
            errorManager = new GitLogErrors(gitArgument);
        }
        public void PresentReport()
        {
            genarateReport((PresentationMethod)gitArgument.ReportType);

            void genarateReport(PresentationMethod handler)
            {
                switch (handler)
                {
                    case PresentationMethod.ConsoleDisplay:
                    {
                        ShowReport();
                        break;
                    }
                    case PresentationMethod.CsvFile:
                    {
                        CreateCsvFile();
                        break;
                    }
                }
            }
        }
        private void ShowReport()
        {
            if (componentManager.Count == 0)
            {
                errorManager.ShowDictionaryEmpty();
            }
            var writer = Console.Out;

            PresentReport(writer);
        }
        private void CreateCsvFile()
        {
            if (componentManager.Count == 0)
            {
                errorManager.ShowDictionaryEmpty();
            }
            var writer = Console.Out;

            PresentReport(writer);
        }
        private void PresentReport(System.IO.TextWriter writer)
        {
            writer.WriteLine("CommitHash, CommiterName, CommitDate, CommitInfo");
            foreach (var commit in commitManager)
            {
                string date = DateTime.Parse(commit.Value.CommitDate).ToShortDateString();
                writer.WriteLine("{0},{1},{2},{3}", commit.Key, commit.Value.CommiterName,
                    date, commit.Value.CommitMessage);
            }

            writer.WriteLine("\n\n\nCommitHash, ComponentId, Insertion, Deletion");
            foreach (var component in componentManager)
            {
                writer.WriteLine("{0},{1},{2},{3}", component.Value.ComponentHash,
                    component.Value.ComponentId, component.Value.InsertionCounter,
                    component.Value.DeletionCounter);
            }
        }
        private enum PresentationMethod
        {
            ConsoleDisplay,
            CsvFile
        }
    }
}
