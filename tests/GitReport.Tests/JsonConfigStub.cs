using ReportCreator;

namespace GitReport.Tests
{
    class JsonConfigStub : IJsonConfig
    {
        public bool TryMatchPath(string pathFromProcess, out string finalId)
        {
            if (pathFromProcess.Contains("Common/Knowledge/Start/"))
            {
                finalId = "Common.Knowledge";
                return true;
            }
            if (pathFromProcess.Contains("Computer/IsSlow/"))
            {
                finalId = "Computer.Slow";
                return true;
            }
            if (pathFromProcess.Contains("Star/Wars/"))
            {
                finalId = "Star.Wars";
                return true;
            }

            finalId = string.Empty;
            return false;
        }
        public string GetSeparator(Separator separator)
        {
            switch (separator)
            {
                case Separator.Commit:
                {
                    return "smallLine";
                }
                case Separator.Output:
                {
                    return "divideLine";
                }
            }
            return string.Empty;
        }
    }
}
