using System.IO;

namespace GitCounter
{
    public class DirectoryValidation : IDirectoryValidation
    {
        public bool CheckIfDirectoryIsValid(string arg)
        {
            return Directory.Exists(arg);
        }
    }
}
