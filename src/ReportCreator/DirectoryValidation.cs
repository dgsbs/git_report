using System.IO;

namespace ReportCreator
{
    public class DirectoryValidation : IDirectoryValidation
    {
        public bool IsDirectoryValid(string path)
        {
            return Directory.Exists(path);
        }
    }
}
