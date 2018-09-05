using ReportCreator;

namespace GitReport.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var gitArgument = new GitArguments();
            var newArgs = gitArgument.ManageGitArguments(args);
            
            var gitArgsValidator =
                new ArgumentsValidation(gitArgument, new DirectoryValidation());
            var errorHandler = new GitLogErrors(gitArgument);

            while (!gitArgsValidator.AreDatesPathValid(newArgs))
            {
                newArgs = errorHandler.FixDatePathError(newArgs);
            }

            IJsonConfig jsonConfig = new JsonConfig();
            var processRunner = new GitProcess(gitArgument, jsonConfig);
            var processOutput = processRunner.RunGitLogProcess();

            var reportHandler = new GitReportCreator(jsonConfig);
            var report = reportHandler.CreateCompleteDictionary(processOutput);

            var reportPresentation = new GitLogPresentation();
            reportPresentation.PresentReport(report);
        }
    }
}