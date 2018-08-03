using System;
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

                while (!gitArgsValidator.AreDatesAndPathValid(arguments, gitArg))
                {
                    string[] editedArgs = new string[3];
                    errorManager.FixDatePathError(arguments, gitArg, out editedArgs);
                    arguments = editedArgs;
                }
                GitDiffProcess processRunner = new GitDiffProcess();
                string processOutput = processRunner.RunGitDiffProcess(gitArg);

                ReportCreator reportManager = new ReportCreator();
                ShowReport(reportManager.CreateWholeReport(processOutput));
            }

            void ShowReport (Dictionary<string, ModificationCounters> dictionaryManager)
            {
                foreach (var dictionaryItem in dictionaryManager)
                {
                    Console.WriteLine("Component id: {0}\nCode added in component: " +
                        "{1}\nCode removed in component: {2}\n", dictionaryItem.Key,
                        dictionaryItem.Value.AdditionCounter, dictionaryItem.Value.DeletionCounter);
                }
            }

        }
    }
}