using System;
using GitCounter;

namespace GitReport.CLI
{
    class GitLogErrors
    {
        GitLogArguments gitArgument;
        public GitLogErrors(GitLogArguments gitArgument)
        {
            this.gitArgument = gitArgument;
        }
        public void FixDatePathError(string[] currentArguments, 
             out string[] newArguments)
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
        public void ShowDictionaryEmpty()
        {
            Console.WriteLine("Json configuration file was not found. " +
                    "Check if it is in application folder.");
        }
        private string EnterPresentationMethod()
        {
            Console.WriteLine("Enter \"csv\" for csv format output or press enter for" +
                " default output in console.");
            return Console.ReadLine();
        }
    }
}
