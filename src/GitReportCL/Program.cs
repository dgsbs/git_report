using ReportCreator;
using System;

namespace GitReport.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var newArgs = ManageConsoleArguments(args);

            var gitArgument = new GitArguments();
            var argumentValidator =
                new ArgumentsValidation(gitArgument, new DirectoryValidation());
            var errorHandler = new GitLogErrors(gitArgument);

            while (!argumentValidator.AreDatesPathValid(newArgs))
            {
                newArgs = errorHandler.FixDatePathError(newArgs);
            }

            IJsonConfig jsonConfig = new JsonConfig();
            var processRunner = new GitProcess(gitArgument, jsonConfig);
            var processOutput = processRunner.RunGitLogProcess();

            var reportHandler = new GitReportCreator(jsonConfig);
            var report = reportHandler.GetReportDictionary(processOutput);

            var reportPresentation = new GitLogPresentation();
            reportPresentation.PresentReport(report);

            string[] ManageConsoleArguments(string[] arguments)
            {
                var dateHandler = new DateSinceManager();
                var reportArguments = new string[3];
                var today = DateTime.Today;

                if (arguments.Length == 2)
                {
                    if (Int32.TryParse(arguments[1], out int parsedString))
                    {
                        var enumFromInt = (FromToday)parsedString;
                        reportArguments[0] = dateHandler.GetDateString(enumFromInt);
                    }
                    reportArguments[1] = today.ToString();
                    reportArguments[2] = arguments[0];
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
}