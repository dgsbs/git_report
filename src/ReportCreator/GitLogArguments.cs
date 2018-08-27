using System;

namespace GitCounter
{
    public class GitLogArguments
    {
        public string GitPath { get; set;}
        public DateTime DateSince { get; set;}
        public DateTime DateBefore { get; set;}
        public int ReportType { get; set;}

        public string[] ManageGitLogArguments(string[] args)
        {
            string[] reportArguments = new string[4];
            DateTime today = DateTime.Today;

            if (args.Length == 3)
            {
                reportArguments[0] = FetchDateSince(args[1]);
                reportArguments[1] = today.ToString();
                reportArguments[2] = args[0];
                reportArguments[3] = args[2];
            }
            else
            {
                reportArguments[0] = FetchDateSince("");
                reportArguments[1] = today.ToString();
                reportArguments[2] = string.Empty;
                reportArguments[3] = string.Empty;
            }

            string FetchDateSince(string timePeriod)                                       
            {
                int numberOfDays = FetchTimePeriod(timePeriod);
                DateTime dateSince  = DateTime.Today.AddDays(-numberOfDays);

                return dateSince.ToString();
            }
            int FetchTimePeriod(string period)
            {
                if (period == "oneWeek")
                {
                    return 7;
                }
                if (period == "fourWeeks")
                {
                    return 28;
                }
                return 1;
            }

            return reportArguments;
        }
    }
}
