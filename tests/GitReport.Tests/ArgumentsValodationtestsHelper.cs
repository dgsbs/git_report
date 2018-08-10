using GitCounter;

namespace GitReport.Tests
{
    class ArgumentsValidationTestsHelper : IDirectoryValidation
    {
        public bool CheckIfDirectoryExist(string arg)
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
