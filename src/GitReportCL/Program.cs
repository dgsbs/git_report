using GitCounter;
using System.Collections.Generic;

namespace GitReport.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            GitLogArguments gitArgument = new GitLogArguments();
            string[] newArgs = gitArgument.ManageGitLogArguments(args);

            IDirectoryValidation directoryValidation = new DirectoryValidation();
            GitLogErrors errorManager = new GitLogErrors(gitArgument);      
            ArgumentsValidation gitArgsValidator =
                new ArgumentsValidation(gitArgument, directoryValidation);

            while (!gitArgsValidator.AreDatesPathValid(newArgs))
            {
                string[] editedArgs = new string[3];
                errorManager.FixDatePathError(newArgs, out editedArgs);
                newArgs = editedArgs;
            }

            Dictionary<string, ComponentData> componentManager =
                new Dictionary<string, ComponentData>();

            Dictionary<string, CommitData> commitManager =
                new Dictionary<string, CommitData>();

            GitProcess processRunner = new GitProcess();
            string processOutput = processRunner.RunGitLogProcess(gitArgument);

            ReportCreator reportManager = new ReportCreator(new JsonConfig(),
                componentManager, commitManager);
            reportManager.CreateFullReport(processOutput);

            GitLogPresentation reportPresentation =
                new GitLogPresentation(componentManager, commitManager, gitArgument, errorManager);
            reportPresentation.PresentReport();
        }
    }
}