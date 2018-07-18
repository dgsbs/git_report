

namespace GitReportCL
{
    using System;
    using System.Globalization;
    using System.Threading;

    class SetGitReport
    {
        public string[] gitArguments;
        public DateTime dateSince, dateBefore;
        public CultureInfo cultureInfo;
        public SetGitReport()
        {
            this.gitArguments = new string[3];               //co z tymi thisami?
            this.Initialization();
        }
        public SetGitReport(string[] args)
        {
            this.gitArguments = args;
            this.Initialization();
        }
        private void Initialization()
        {
            dateSince = new DateTime();
            dateBefore = new DateTime();
            cultureInfo = new CultureInfo("en-Us");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
        }
    }
}
