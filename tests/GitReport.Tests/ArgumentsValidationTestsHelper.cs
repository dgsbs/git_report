using ReportCreator;

namespace GitReport.Tests
{
    class ArgumentsValidationTestsHelper : IDirectoryValidation 
    {
        public bool IsDirectoryValid(string arg)
        {
            string localPath = @"C:\git";
            bool pathExistenceValidator = arg.Contains(localPath);
            if (pathExistenceValidator)
            {
                return true;
            }
            return false;
        }
    }
}
