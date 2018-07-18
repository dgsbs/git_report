namespace GitReportCL
{
    class RunGitReport
    {
        public RunProcess run = new RunProcess();
        public void Report()
        {
            run.validate.Validate();
            run.Process();
        }
    }
}
