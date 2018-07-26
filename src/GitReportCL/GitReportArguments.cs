using System;
namespace GitReport.CLI
{
    class GitReportArguments
    {
        public string GitPath { get; set;}
        public DateTime DateSince { get; set;}
        public DateTime DateBefore { get; set;}
    }
}
