using System;
namespace GitReport.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            GitReportArguments getArg = new GitReportArguments();
            DateAndPathValidation startValidation =  new DateAndPathValidation();
            GitDiffReportProcess runProcess = new GitDiffReportProcess();
            GitReportErrorsHandler operate = new GitReportErrorsHandler();
            
            if (args.Length == 3)
            {
                while (!startValidation.ValidateGitDiffReportArgs(args, getArg))
                {
                    string[] editedArgs = new string[3];
                    operate.HandleDateFormatOrPathError(args, getArg, out editedArgs);
                    args = editedArgs;
                }
                Console.WriteLine(runProcess.RunGitDiff(getArg)); 
            }
            else
            {
                string[] newArgs = operate.CreateArgsForGitDiffReport();
                while (!startValidation.ValidateGitDiffReportArgs(newArgs, getArg))
                {
                    string[] editedArgs = new string[3];
                    operate.HandleDateFormatOrPathError(newArgs, getArg, out editedArgs);
                    newArgs = editedArgs;
                }
                Console.WriteLine(runProcess.RunGitDiff(getArg));
            }
        }
    }
}