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
            RunGitLog(newArgs, gitArgument);

            void RunGitLog (string[] arguments, GitLogArguments gitArg)
            {
                IDirectoryValidation directoryValidation = new DirectoryValidation();
                GitLogErrors errorManager = new GitLogErrors(gitArgument);
                ArgumentsValidation gitArgsValidator = 
                    new ArgumentsValidation(gitArgument, directoryValidation);

                while (!gitArgsValidator.AreDatesPathValid(arguments))
                {
                    string[] editedArgs = new string[3];
                    errorManager.FixDatePathError(arguments, out editedArgs);
                    arguments = editedArgs;
                }

                Dictionary<string, ComponentData> componentManager =
                    new Dictionary<string, ComponentData>();

                Dictionary<string, CommitData> commitManager = 
                    new Dictionary<string, CommitData>();

                GitProcess processRunner = new GitProcess();
                string processOutput = processRunner.RunGitLogProcess(gitArg);
                
                ReportCreator reportManager = new ReportCreator(new JsonConfig(),
                    componentManager, commitManager);
                reportManager.CreateFullReport(processOutput);
                
                GitLogPresentation reportPresentation = 
                    new GitLogPresentation(componentManager,commitManager, gitArg);
                reportPresentation.PresentReport();
            }
        }
    }
}