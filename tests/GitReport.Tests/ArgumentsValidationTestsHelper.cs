using ReportCreator;

namespace GitReport.Tests
{
    class ArgumentsValidationTestsHelper : IDirectoryValidation 
    {
        public bool IsDirectoryValid(string arg)
        {
            var localPath = @"C:\git";
            var pathExistenceValidator = arg.Contains(localPath);
            if (pathExistenceValidator)
            {
                return true;
            }
            return false;
        }
    }
}
