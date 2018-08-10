using System;

namespace GitCounter
{
    public class ArgumentsValidation 
    {
        GitDiffArguments gitArgument = new GitDiffArguments();
        IDirectoryValidation directoryValidation;
        public ArgumentsValidation(GitDiffArguments gitArgument, IDirectoryValidation directoryValidation)
        {
            this.gitArgument = gitArgument;
            this.directoryValidation = directoryValidation;
        }
        public bool AreDatesPathValid(string[] arguments)         
        {
            bool sinceDateValidator = DateTime.TryParse(arguments[0], out var SinceDate);
            gitArgument.DateSince = SinceDate;

            bool beforeDateValidator = DateTime.TryParse(arguments[1], out var BeforeDate);
            gitArgument.DateBefore = BeforeDate;

            bool pathExistenceValidator = directoryValidation.CheckIfDirectoryExist(arguments[2]);

            if (pathExistenceValidator)
            {
                gitArgument.GitPath = arguments[2];
            }

            if (!sinceDateValidator || !beforeDateValidator || !pathExistenceValidator ||
                SinceDate > BeforeDate)
            {
                return false;
            }
            return true;
        }
    }
}