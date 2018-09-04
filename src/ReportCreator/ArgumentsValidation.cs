using System;

namespace ReportCreator
{
    public class ArgumentsValidation 
    {
        public GitArguments GitArgument { get; private set; } 
        private IDirectoryValidation directoryValidation;
        public ArgumentsValidation(GitArguments gitArgument,
            IDirectoryValidation directoryValidation)
        {
            this.GitArgument = gitArgument;
            this.directoryValidation = directoryValidation;
        }
        public bool AreDatesPathValid(string[] arguments)
        {
            var sinceDateValidator = DateTime.TryParse(arguments[0], out var SinceDate);
            GitArgument.DateSince = SinceDate;

            var beforeDateValidator = DateTime.TryParse(arguments[1], out var BeforeDate);
            GitArgument.DateBefore = BeforeDate;

            var pathValidator = this.directoryValidation.IsDirectoryValid(arguments[2]);

            if (pathValidator)
            {
                GitArgument.GitPath = arguments[2];
            }
            else
            {
                GitArgument.GitPath = "";
            }

            if (!sinceDateValidator || !beforeDateValidator ||
                !pathValidator || SinceDate > BeforeDate)
            {
                return false;
            }
            return true;
        }
    }
}