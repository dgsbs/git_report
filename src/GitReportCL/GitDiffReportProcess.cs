using System;
using System.Diagnostics;
using System.IO;
namespace GitReport.CLI
{
    class GitDiffReportProcess
    {
        public DateAndPathValidation ValidateArgsForGitDiff
        {
            get;
            set;
        } = new DateAndPathValidation();
        private string CreateFileForGitDiffReport()
        {
            Console.WriteLine("Choose directory and file name for your Git report.");
            return Console.ReadLine(); ;                                                                        
        }
        private string RunProcessWithGitCommands(string arg)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("git")
            {
                WorkingDirectory = ValidateArgsForGitDiff.GetArgsForGitDiff.GitArguments[2],
                UseShellExecute = false,
                RedirectStandardOutput = true,
                Arguments = arg
            };

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            string processTemp = process.StandardOutput.ReadToEnd();
            process.CloseMainWindow();
            process.Dispose();

            return processTemp;
        }
        private string CutDate(string arg)
        {
            if (arg.Length == 20)
            {
                return arg.Substring(0, 8);
            }
            else if (arg.Length == 21)
            {
                return arg.Substring(0, 9);
            }
            else
            {
                return arg.Substring(0, 10);
            }
        }
        private string BuildBeforeArgument()
        {
            return "log --pretty=\"%H\" --before=\"24:00 " +
                CutDate(ValidateArgsForGitDiff.GetArgsForGitDiff.GitArguments[1]) + "\" -1";
        }
        private string BuildSinceArgument()
        {
            return "log --pretty=\"%H\" --before=\"00:00 " + 
                CutDate(ValidateArgsForGitDiff.GetArgsForGitDiff.GitArguments[0]) + "\" -1";
        }
        private string BuildGitDiffArgument(string commitSince, string commitBefore)
        {
            return "diff --numstat " + commitSince.Trim() + ".." + commitBefore.Trim();
        }
        private string RunGitDiff()
        {
            string sinceArgument = BuildSinceArgument();
            string beforeArgument = BuildBeforeArgument();
            string commitFromSinceDate = RunProcessWithGitCommands(sinceArgument);
            string commitFromBeforeDate = RunProcessWithGitCommands(beforeArgument);

            if (commitFromBeforeDate == commitFromSinceDate)
            {
                Console.WriteLine("Dates you did choose, have produced only one commit." +
                    " Run this again with different dates.");
            }
            string argForGitDiff = BuildGitDiffArgument(commitFromSinceDate, 
                commitFromBeforeDate);

            return RunProcessWithGitCommands(argForGitDiff);
        }
        public void HandleGitDiffProcess()
        {
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;

            if (encoding.GetMaxByteCount((RunGitDiff()).ToCharArray().Length) < int.MaxValue)
            {
                string dataFromProcessDiff = RunGitDiff();
                byte[] byteArray = encoding.GetBytes(dataFromProcessDiff);

                using (MemoryStream memStream = new MemoryStream(byteArray))
                {
                    using (FileStream fileStream = 
                        new FileStream(CreateFileForGitDiffReport(), FileMode.Create))      
                    {
                        memStream.CopyTo(fileStream);
                    }
                }
                string gitReport = new string(encoding.GetChars(byteArray));               
            }
        }
    }
}
