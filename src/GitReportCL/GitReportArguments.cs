using System;
using System.Globalization;
using System.Threading;
namespace GitReport.CLI
{
    class GitReportArguments
    {
        public string[] GitArguments
        {
            get;
            set;
        }
        public DateTime dateSince;
        public DateTime dateBefore;
        public CultureInfo CultureInfo
        {
            get;
            set;
        }
        public GitReportArguments()
        {
            this.GitArguments = new string[3];               
            this.Initialization();
        }
        public GitReportArguments(string[] args)
        {
            this.GitArguments = args;
            this.Initialization();
        }
        private void Initialization()
        {
            dateSince = new DateTime();
            dateBefore = new DateTime();
            CultureInfo = new CultureInfo("en-Us");
            Thread.CurrentThread.CurrentCulture = CultureInfo;
        }
    }
}
