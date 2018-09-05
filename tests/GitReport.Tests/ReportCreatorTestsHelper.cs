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
        public string GetSeparator(JsonConfig.Separator separator)
        {
            switch (separator)
            {
                case JsonConfig.Separator.Commit:
                {
                    return "smallLine";
                }
                case JsonConfig.Separator.Output:
                {
                    return "divideLine";
                }
            }
            return string.Empty;
        }
    }
}
