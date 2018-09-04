using ReportCreator;
using System.Collections.Generic;

namespace GitReport.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var gitArgument = new GitArguments();
            string[] newArgs = gitArgument.ManageGitArguments(args);

            var errorHandler = new GitLogErrors(gitArgument);
            var gitArgsValidator =
                new ArgumentsValidation(gitArgument, new DirectoryValidation());

            while (!gitArgsValidator.AreDatesPathValid(newArgs))
            {
                newArgs = errorHandler.FixDatePathError(newArgs);
            }

            IJsonConfig jsonConfig = new JsonConfig();
            var processRunner = new GitProcess(gitArgument, jsonConfig);
            var processOutput = processRunner.RunGitLogProcess();

            var reportHandler = new GitReportCreator(jsonConfig); 
            reportHandler.CreateFullReport(processOutput, 
                out List<CommitData> CommitList,
                out Dictionary<ComponentKey, ComponentData> ComponentDictionary);

            var reportPresentation = new GitLogPresentation();
            reportPresentation.PresentReport(in CommitList, in ComponentDictionary);
        }
    }
}