using System.IO;

namespace ReportCreator
{
    public class DirectoryValidation : IDirectoryValidation
    {
        public bool IsDirectoryValid(string arg)
        {
            return Directory.Exists(arg);
        }
    }
}
