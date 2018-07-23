namespace GitReport.CLI
{
    class GitReportHandler
    {
        public GitDiffReportProcess RunGitDiffProcess
        {
            get;
        } = new GitDiffReportProcess();
        public void RunGitReportHandler()
        {
            RunGitDiffProcess.ValidateArgsForGitDiff.ValidatePathAndDate();
            RunGitDiffProcess.HandleGitDiffProcess();
        }
    }
}
