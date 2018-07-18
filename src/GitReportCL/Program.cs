namespace GitReportCL
{
    class Program
    {
        static void Main(string[] args)
        {
            RunGitReport report = new RunGitReport();
            if (args.Length == 3)
            {
                report.run.validate.setGitRep = new SetGitReport(args);
                report.Report();
            }
            else
            {
                report.run.validate.setGitRep = new SetGitReport();
                report.Report();
            }
        }
    }
}
