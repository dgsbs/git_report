using System;
using System.Diagnostics;

namespace ReportCreator
{
    public class GitProcess
    {
        private const string renameLimit = "config diff.renameLimit 999999";
        private string gitPath;
        private DateTime dateSince;
        private DateTime dateBefore;
        private IJsonConfig jsonConfig;
        public GitProcess (GitArguments gitArgument, IJsonConfig jsonConfig)
        {
            this.gitPath = gitArgument.GitPath;
            this.dateSince = gitArgument.DateSince;
            this.dateBefore = gitArgument.DateBefore;
            this.jsonConfig = jsonConfig;
        }
        private string RunGitProcess(string processArguments)            
        {
            var startInfo = new ProcessStartInfo("git")
            {
                WorkingDirectory = this.gitPath,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                Arguments = processArguments
            };
            
            using (var process = new Process())
            {
                process.StartInfo = startInfo;
                process.Start();

                var wholeStdOut = string.Empty;
                var stdOneLine = string.Empty;

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
            return $"log --pretty=\"" +
                $"{this.jsonConfig.GetSeparator(JsonConfig.Separator.Output)}%n%H%n%cn%n%ci%n%s%n" +
                $"{this.jsonConfig.GetSeparator(JsonConfig.Separator.Commit)}\" --numstat " +
                $"--since=\"{this.dateSince.ToShortDateString()} 24:00\"" +
                $" --before=\"{this.dateBefore.ToShortDateString()} 24:00\"";
        }
        public string RunGitLogProcess()
        {
            RunGitProcess(renameLimit);

            var gitLogArgument = BuildGitLogCommand();

            return RunGitProcess(gitLogArgument);
        }
    }
}