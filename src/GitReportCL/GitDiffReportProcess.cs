using System.Diagnostics;
namespace GitReport.CLI
{
    class GitDiffProcess
    {
        private static string partialGitLogCommand = "log --pretty=\"%H\" --before=\"";
        private string RunProcessWithGitCommands(string arg, GitDiffArguments gitArgument)            
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("git")
            {
                WorkingDirectory = gitArgument.GitPath,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                Arguments = arg
            };

            var wholeStdOut = string.Empty;
            var stdOneLine = string.Empty;
            using (Process process = new Process())
            {
                process.StartInfo = startInfo;
                process.Start();
                
                if (arg.Contains("diff"))
                {
                    while ((stdOneLine = process.StandardOutput.ReadLine()) != null)
                    {
                        wholeStdOut += stdOneLine;
                        wholeStdOut += "\n";
                        if (wholeStdOut.Length >= int.MaxValue)
                        {
                            break;
                        }
                    }
                    return wholeStdOut;
                }
                else
                {
                    stdOneLine = process.StandardOutput.ReadLine();
                    return stdOneLine;
                }
            }
        }
        private string BuildGitLogBeforeCommand(GitDiffArguments gitArgument) 
        {
            return $"{partialGitLogCommand}24:00 {gitArgument.DateBefore.ToShortDateString()}\" -1";          
        }
        private string BuildGitLogSinceCommand(GitDiffArguments gitArgument)
        {
            return $"{partialGitLogCommand}00:00 {gitArgument.DateSince.ToShortDateString()}\" -1";
        }
        private string BuildGitDiffCommand(string commitSince, string commitBefore)
        {
            return $"diff --numstat {commitSince.Trim()}..{commitBefore.Trim()}";
        }
        public string RunGitDiffProcess(GitDiffArguments gitArgument)
        {
            var sinceArgument = BuildGitLogSinceCommand(gitArgument);
            var beforeArgument = BuildGitLogBeforeCommand(gitArgument);
            var commitFromSinceDate = RunProcessWithGitCommands(sinceArgument, gitArgument);
            var commitFromBeforeDate = RunProcessWithGitCommands(beforeArgument, gitArgument);
            var argForGitDiff = BuildGitDiffCommand(commitFromSinceDate, 
                commitFromBeforeDate);

            return RunProcessWithGitCommands(argForGitDiff,gitArgument);
        }
    }
}