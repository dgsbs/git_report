namespace GitReport.CLI
{
    class GitReport
    {
        public static GitReportHandler RunGitReport
        {
            get;
        } = new GitReportHandler();
        static void Main(string[] args)
        {
            if (args.Length == 3)
            {
                RunGitReport.RunGitDiffProcess.ValidateArgsForGitDiff.GetArgsForGitDiff
                    = new GitReportArguments(args);
                RunGitReport.RunGitReportHandler();
            }
            else
            {
                RunGitReport.RunGitDiffProcess.ValidateArgsForGitDiff.GetArgsForGitDiff
                    = new GitReportArguments();
                RunGitReport.RunGitReportHandler();
            }
        }
    }
}
