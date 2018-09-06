using ReportCreator;

namespace GitReport.Tests
{
    class DirectoryValidationStub : IDirectoryValidation 
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
