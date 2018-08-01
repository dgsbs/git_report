using System;
using System.IO;
namespace GitReport.CLI
{
    class GitDiffArgumentsValidation                        
    {
        public bool AreDatesAndPathValid(string[] arguments, GitDiffArguments gitArgument)         
        {
            bool sinceDateFlag = DateTime.TryParse(arguments[0], out var SinceDate);
            bool beforeDateFlag = DateTime.TryParse(arguments[1], out var BeforeDate);
            bool pathFlag = false;
            gitArgument.DateSince = SinceDate;
            gitArgument.DateBefore = BeforeDate;

            if (Directory.Exists(arguments[2]))
            {
                gitArgument.GitPath = arguments[2];
                pathFlag = true;
            }
            else
            {
                gitArgument.GitPath = string.Empty;
            }

            if (!sinceDateFlag || !beforeDateFlag || !pathFlag ||
                SinceDate > BeforeDate)
            {
                return false;
            }

            return true;
        }
    }
}