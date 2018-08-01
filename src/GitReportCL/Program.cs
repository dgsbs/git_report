using System.Collections.Generic;
namespace GitReport.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            GitDiffArguments gitArgument = new GitDiffArguments();
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
                GitDiffArgumentsValidation gitArgsValidator = new GitDiffArgumentsValidation();
                Dictionary<string, DictionaryArgsForGitDiff> dictionaryManager = 
                    new Dictionary<string, DictionaryArgsForGitDiff>();

                while (!gitArgsValidator.AreDatesAndPathValid(arguments, gitArg))
                {
                    string[] editedArgs = new string[3];
                    errorManager.FixDatePathError(arguments, gitArg, out editedArgs);
                    arguments = editedArgs;
                }
                GitDiffProcess processRunner = new GitDiffProcess(dictionaryManager);
                processRunner.RunGitDiffProcess(gitArg);
            }
        }
    }
}