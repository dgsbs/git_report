using System;
namespace GitReport.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            GitDiffArguments gitArgument = new GitDiffArguments();
            GitDiffArgumentsValidation gitArgsValidator =  new GitDiffArgumentsValidation();
            GitDiffProcess processRunner = new GitDiffProcess();
            GitDiffErrors errorManager = new GitDiffErrors();
            
            if (args.Length == 3)
            {
                RunGitDiff(args, gitArgument);
            }
            else
            {
                string[] newArgs = errorManager.CreateArgsForGitDiffReport();
                RunGitDiff(newArgs, gitArgument);
            }

            void RunGitDiff (string[] arguments, GitDiffArguments gitArg)
            {
                while (!gitArgsValidator.AreDatesAndPathValid(arguments, gitArg))
                {
                    string[] editedArgs = new string[3];
                    errorManager.FixDatePathError(arguments, gitArg, out editedArgs);
                    arguments = editedArgs;
                }
                Console.WriteLine(processRunner.RunGitDiffProcess(gitArg));
            }
        }
    }
}