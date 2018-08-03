using System;
using System.IO;
namespace GitReport.CLI
{
    class GitDiffArgumentsValidation                        
    {
        public bool AreDatesAndPathValid(string[] arguments, GitDiffArguments gitArgument)         
        {
            bool sinceDateValidator = DateTime.TryParse(arguments[0], out var SinceDate);
            gitArgument.DateSince = SinceDate;

            bool beforeDateValidator = DateTime.TryParse(arguments[1], out var BeforeDate);
            gitArgument.DateBefore = BeforeDate;

            bool pathExistenceValidator = Directory.Exists(arguments[2]);

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