using GitCounter;

namespace GitReport.Tests
{
    class DirectoryValidationTestsHelper : IDirectoryValidation 
    {
        public bool CheckIfDirectoryIsValid(string arg)
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
