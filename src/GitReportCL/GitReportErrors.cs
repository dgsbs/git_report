using System;
using GitCounter;

namespace GitReport.CLI
{
    class GitDiffErrors
    {
        public void FixDatePathError(string[] currentArguments, 
            GitDiffArguments gitArgument, out string[] newArguments)
        {
            if (gitArgument.DateSince == DateTime.MinValue)
            {
                currentArguments[0] = EnterDate("start-date", true);
            }

            if (gitArgument.DateBefore == DateTime.MinValue)
            {
                currentArguments[1] = EnterDate("end-date", true);
            }

            if (gitArgument.DateSince > gitArgument.DateBefore &&
                gitArgument.DateSince != DateTime.MinValue &&
                gitArgument.DateBefore != DateTime.MinValue)
            {
                Console.WriteLine("The end-date was more previous then start - date.");
                currentArguments[0] = EnterDate("start-date", false);
                currentArguments[1] = EnterDate("end-date", false);
            }

            if (gitArgument.GitPath == string.Empty)
            {
                currentArguments[2] = EnterPath(true);
            }
            newArguments = currentArguments;
        }
        private string EnterDate(string whichDate, bool flag)
        {
            if (flag)
            {
                Console.WriteLine("Format that you used while entering the " + whichDate +
                " was wrong.");
            }
            Console.WriteLine("Please enter new " + whichDate + ":");

            return Console.ReadLine();
        }
        private string EnterPath(bool flag)
        {
            if (flag)
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
        public string[] CreateArgsForGitDiffReport()
        {
            string[] tempGitArguments = new string[3];
            Console.WriteLine("You have to pass 3 arguments for GitReport to work. ");
            tempGitArguments[0] = EnterDate("start-date", false);
            tempGitArguments[1] = EnterDate("end-date", false);
            tempGitArguments[2] = EnterPath(false);

            return tempGitArguments;
        }
    }
}
