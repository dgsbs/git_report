using System;
using System.Diagnostics;

namespace ReportCreator
{
    public class GitProcess
    {
        private const string renameLimit = "config diff.renameLimit 999999";
        private String GitPath { get; set; }
        private DateTime DateSince { get; set; }
        private DateTime DateBefore { get; set; }
        IJsonConfig jsonConfig;
        public GitProcess (GitArguments gitArgument, IJsonConfig JsonConfig)
        {
            this.GitPath = gitArgument.GitPath;
            this.DateSince = gitArgument.DateSince;
            this.DateBefore = gitArgument.DateBefore;
            this.jsonConfig = JsonConfig;
        }
        private string RunGitProcess(string processArguments)            
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("git")
            {
                WorkingDirectory = GitPath,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                Arguments = processArguments
            };

            var wholeStdOut = string.Empty;
            var stdOneLine = string.Empty;
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
                return wholeStdOut;
            }
        }
        private string BuildGitLogCommand()
        {
            return $"log --pretty=\"{jsonConfig.FetchSepatator(true)}%n%H%n%cn%n%ci%n%s%n" +
                $"{jsonConfig.FetchSepatator(false)}\" --numstat " +
                $"--since=\"{DateSince.ToShortDateString()} 24:00\"" +
                $" --before=\"{DateBefore.ToShortDateString()} 24:00\"";
        }
        public string RunGitLogProcess()
        {
            RunGitProcess(renameLimit);

            string gitLogArgument = BuildGitLogCommand();

            return RunGitProcess(gitLogArgument);
        }
    }
}