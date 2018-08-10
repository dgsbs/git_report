using System.IO;

namespace GitCounter
{
    public class DirectoryValidation : IDirectoryValidation
    {
        public bool CheckIfDirectoryExist(string arg)
        {
            return Directory.Exists(arg);
        }
    }
}
