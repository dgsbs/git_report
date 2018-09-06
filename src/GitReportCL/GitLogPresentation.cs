using System;
using ReportCreator;
using System.Collections.Generic;

namespace GitReport.CLI
{
    class GitLogPresentation
    {
        public void PresentReport(Dictionary<HashId, CommitComponentData> dictionary)
        {
            if (dictionary.Count == 0)
            {
                Console.WriteLine("Json configuration file was not found. " +
                    "Check if it is in application folder.");
            }
            var writer = Console.Out;
            writer.WriteLine("CommitHash, ComponentId, CommiterName, CommitDate, CommitInfo, " +
                "Insertions, Deletions");
            foreach (var dictionaryItem in dictionary)
            {
                var date = DateTime.Parse(dictionaryItem.Value.CommitDate).ToShortDateString();

                writer.WriteLine($"{dictionaryItem.Key.CommitHash}," +
                    $"{dictionaryItem.Key.ComponentId}," +
                    $"{dictionaryItem.Value.CommiterName}," +
                    $"{date}," +
                    $"{dictionaryItem.Value.CommitMessage}," +
                    $"{dictionaryItem.Value.ComponentInsertions}," +
                    $"{dictionaryItem.Value.ComponentDeletions}");
            }
        }
    }
}
