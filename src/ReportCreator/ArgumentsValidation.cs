using System;

namespace ReportCreator
{
    public class ArgumentsValidation 
    {
        public GitArguments GitArgument { get; private set; } 
        IDirectoryValidation directoryValidation;
        public ArgumentsValidation(GitArguments GitArgument,
            IDirectoryValidation directoryValidation)
        {
            this.GitArgument = GitArgument;
            this.directoryValidation = directoryValidation;
        }
        public bool AreDatesPathValid(string[] arguments)
        {
            bool sinceDateValidator = DateTime.TryParse(arguments[0], out var SinceDate);
            GitArgument.DateSince = SinceDate;

            bool beforeDateValidator = DateTime.TryParse(arguments[1], out var BeforeDate);
            GitArgument.DateBefore = BeforeDate;

            bool pathExistenceValidator =
            directoryValidation.CheckIfDirectoryIsValid(arguments[2]);

            if (pathExistenceValidator)
            {
                GitArgument.GitPath = arguments[2];
            }
            else
            {
                GitArgument.GitPath = "";
            }

            if (!sinceDateValidator || !beforeDateValidator ||
                !pathExistenceValidator || SinceDate > BeforeDate)
            {
                return false;
            }
            return true;
        }
    }
}