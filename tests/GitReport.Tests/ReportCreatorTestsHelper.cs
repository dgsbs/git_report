using ReportCreator;

namespace GitReport.Tests
{
    class ReportCreatorTestsHelper : IJsonConfig
    {
        public bool TryMatchPath(string pathFromProcess, out string finalId)
        {
            if (pathFromProcess.Contains("Common/Knowledge/Start/"))
            {
                finalId = "Common.Knowledge";
                return true;
            }
            if (pathFromProcess.Contains("Computer/IsSlow/AskGoogle/"))
            {
                finalId = "Computer.Slow";
                return true;
            }

            finalId = string.Empty;
            return false;
        }
        public string FetchSepatator(bool whichSepatator)
        {
            if (whichSepatator)
            {
                return "divideLine";
            }
            return "smallLine";
        }
    }
}
