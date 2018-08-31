using ReportCreator;
using System.Collections.Generic;

namespace GitReport.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            GitArguments gitArgument = new GitArguments();
            string[] newArgs = gitArgument.ManageGitArguments(args);

            GitLogErrors errorHandler = new GitLogErrors(gitArgument);      
            ArgumentsValidation gitArgsValidator =
                new ArgumentsValidation(gitArgument, new DirectoryValidation());

            while (!gitArgsValidator.AreDatesPathValid(newArgs))
            {
                string[] editedArgs = new string[3];
                errorHandler.FixDatePathError(newArgs, out editedArgs);
                newArgs = editedArgs;
            }

            IJsonConfig jsonConfig = new JsonConfig();
            GitProcess processRunner = new GitProcess(gitArgument, jsonConfig);
            string processOutput = processRunner.RunGitLogProcess();

            GitReportCreator reportHandler =
                new GitReportCreator(jsonConfig, new Dictionary<string,
                ComponentData>(), new Dictionary<string, CommitData>()); 
            reportHandler.CreateFullReport(processOutput);

            GitLogPresentation reportPresentation =
                new GitLogPresentation(reportHandler);
            reportPresentation.PresentReport();
        }
    }
}