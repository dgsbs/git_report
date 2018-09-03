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
            string[] reportArguments = new string[3];
            DateTime today = DateTime.Today;

            if (args.Length == 2)
            {
                reportArguments[0] = GetDateSince(args[1]);
                reportArguments[1] = today.ToString();
                reportArguments[2] = args[0];
            }
            else
            {
                reportArguments[0] = GetDateSince("oneDay");
                reportArguments[1] = today.ToString();
                reportArguments[2] = "";
            }
            return reportArguments;
        }
        private string GetDateSince(string timePeriod)
        {
            int numberOfDays = GetNumberOfDays(timePeriod);
            DateTime dateSince;

            if (DateTime.Now.Hour < 10 && timePeriod == "oneDay")
            {
                dateSince = DateTime.Today.AddDays(-(numberOfDays + 1));
            }
            else
            {
                dateSince = DateTime.Today.AddDays(-numberOfDays);
            }
            return dateSince.ToString();
        }
        private int GetNumberOfDays(string timePeriod)
        {
            if (timePeriod == "oneWeek")
            {
                return 7;
            }
            if (timePeriod == "fourWeeks")
            {
                return 28;
            }
            return 1;
        }
    }
}
