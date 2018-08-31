using System.IO;

namespace ReportCreator
{
    public class DirectoryValidation : IDirectoryValidation
    {
        public bool CheckIfDirectoryIsValid(string arg)
        {
            return Directory.Exists(arg);
        }
    }
}
