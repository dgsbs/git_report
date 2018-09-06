using System;

namespace ReportCreator
{
    public class GitArguments
    {
        public string GitPath { get; set; }
        public DateTime DateSince { get; set; }
        public DateTime DateBefore { get; set; }
        public string[] ManageGitArguments(string[] args)
        {
            var dateHandler = new DateSinceManager();
            var reportArguments = new string[3];
            var today = DateTime.Today;

            if (args.Length == 2)
            {
                if (Int32.TryParse(args[1], out int parsedString))
                {
                    var enumFromInt = (FromToday)parsedString;
                    reportArguments[0] = dateHandler.GetDateString(enumFromInt);
                }
                reportArguments[1] = today.ToString();
                reportArguments[2] = args[0];
            }
            else
            {
                reportArguments[0] = 
                    dateHandler.GetDateString(FromToday.OneDay);
                reportArguments[1] = today.ToString();
                reportArguments[2] = "";
            }
            return reportArguments;
        }
    }
}
