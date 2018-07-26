using System;
using System.IO;
namespace GitReport.CLI
{
    class DateAndPathValidation                        
    {
        public bool ValidateGitDiffReportArgs(string[] arguments, GitReportArguments getArg)         
        {
            DateTime.TryParse(arguments[0], out var SinceDate);
            DateTime.TryParse(arguments[1], out var BeforeDate);
            getArg.DateSince = SinceDate;
            getArg.DateBefore = BeforeDate;

            if (Directory.Exists(arguments[2]))
            {
                getArg.GitPath = arguments[2];
            }
            else
            {
                getArg.GitPath = "no path found";
            }

            if (getArg.DateSince == DateTime.MinValue ||
                getArg.DateBefore == DateTime.MinValue ||
                getArg.GitPath == "no path found" ||
                SinceDate > BeforeDate)
            {
                return false;
            }

            return true;
        }
    }
}