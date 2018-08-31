using System;
using ReportCreator;

namespace GitReport.CLI
{
    class GitLogErrors
    {
        GitArguments gitArgument;
        public GitLogErrors(GitArguments gitArgument)
        {
            this.gitArgument = gitArgument;
        }
        public void FixDatePathError(string[] currentArguments, 
             out string[] newArguments)
        {
            if (this.gitArgument.DateSince == DateTime.MinValue)
            {
                currentArguments[0] = EnterDate("start-date", true);
            }

            if (this.gitArgument.DateBefore == DateTime.MinValue)
            {
                currentArguments[1] = EnterDate("end-date", true);
            }

            if (this.gitArgument.DateSince > this.gitArgument.DateBefore &&
                this.gitArgument.DateSince != DateTime.MinValue &&
                this.gitArgument.DateBefore != DateTime.MinValue)
            {
                Console.WriteLine("The end-date was more previous then start-date.");
                currentArguments[0] = EnterDate("start-date", false);
                currentArguments[1] = EnterDate("end-date", false);
            }

            if (this.gitArgument.GitPath == string.Empty)
            {
                currentArguments[2] = EnterPath(true);
            }
            newArguments = currentArguments;
        }
        private string EnterDate(string whichDate, bool messageTypeFlag)
        {
            if (messageTypeFlag)
            {
                Console.WriteLine("Format that you used while entering the " + whichDate +
                " was wrong.");
            }
            Console.WriteLine("Please enter new " + whichDate + ":");

            return Console.ReadLine();
        }
        private string EnterPath(bool messageTypeFlag)
        {
            if (messageTypeFlag)
            {
                Console.WriteLine("There is something wrong with the path you have entered." +
                    " Be sure to use folder connected to GitHub. Please try again.");
            }
            else
            {
                Console.WriteLine("To run Git Report you need to a path to your folder " +
                    "connected to GitHub. Please enter your the path.");
            }
            return Console.ReadLine();
        }
    }
}
