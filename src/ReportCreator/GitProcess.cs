using System.Diagnostics;

namespace GitCounter
{
    public class GitProcess
    {
        private static string renameLimit = "config diff.renameLimit 999999";
        private string RunGitProcess(string arg, GitLogArguments gitArgument)            
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
                
                if (arg.Contains("log"))
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
                return string.Empty;
            }
        }
        private string BuildGitLogCommand(GitLogArguments gitArgument)
        {
            return $"log --pretty=\"divideLine%n%H%n%cn%n%ci%n%s%nsmallLine\" --numstat " +
                $"--since=\"{ gitArgument.DateSince.ToShortDateString()} 24:00\"" +
                $" --before=\"{gitArgument.DateBefore.ToShortDateString()} 24:00\"";
        }
        public string RunGitLogProcess(GitLogArguments gitArgument)
        {
            RunGitProcess(renameLimit,gitArgument);

            string gitLogArgument = BuildGitLogCommand(gitArgument);

            return RunGitProcess(gitLogArgument,gitArgument);
        }
    }
}