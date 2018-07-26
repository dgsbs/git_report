using System.Diagnostics;
namespace GitReport.CLI
{
    class GitDiffReportProcess
    {
        private static string gitLogCommand = "log --pretty=\"%H\" --before=\"";
        private string RunProcessWithGitCommands(string arg, GitReportArguments getArg)            
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("git")
            {
                WorkingDirectory = getArg.GitPath,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                Arguments = arg
            };

            string wholeStdOut = string.Empty;
            string stdOneLine;
            using (Process process = new Process())
            {
                process.StartInfo = startInfo;
                process.Start();
                while ((stdOneLine = process.StandardOutput.ReadLine()) != null)
                {
                    wholeStdOut += stdOneLine;
                    wholeStdOut += "\n";
                    if (wholeStdOut.Length >= int.MaxValue)
                    {
                        break;
                    }
                }
            }
            return wholeStdOut;
        }
        private string BuildBeforeArgument(GitReportArguments getArg) 
        {
            return $"{gitLogCommand}24:00 {getArg.DateBefore.ToShortDateString()}\" -1";          
        }
        private string BuildSinceArgument(GitReportArguments getArg)
        {
            return $"{gitLogCommand}00:00 {getArg.DateSince.ToShortDateString()}\" -1";
        }
        private string BuildGitDiffArgument(string commitSince, string commitBefore)
        {
            return $"diff --numstat {commitSince.Trim()}..{commitBefore.Trim()}";
        }
        public string RunGitDiff(GitReportArguments getArg)
        {
            string sinceArgument = BuildSinceArgument(getArg);
            string beforeArgument = BuildBeforeArgument(getArg);
            string commitFromSinceDate = RunProcessWithGitCommands(sinceArgument, getArg);
            string commitFromBeforeDate = RunProcessWithGitCommands(beforeArgument, getArg);
            string argForGitDiff = BuildGitDiffArgument(commitFromSinceDate, 
                commitFromBeforeDate);

            return RunProcessWithGitCommands(argForGitDiff,getArg);
        }
    }
}