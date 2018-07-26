using System;
namespace GitReport.CLI
{
    class GitReportErrorsHandler
    {
        public void HandleDateFormatOrPathError(string[] currentArguments, 
            GitReportArguments getArg, out string[] newArguments)
        {
            if (getArg.DateSince == DateTime.MinValue)
            {
                currentArguments[0] = EnterDate("start-date", true);
            }

            if (getArg.DateBefore == DateTime.MinValue)
            {
                currentArguments[1] = EnterDate("end-date", true);
            }

            if (getArg.DateSince > getArg.DateBefore &&
                getArg.DateSince != DateTime.MinValue &&
                getArg.DateBefore != DateTime.MinValue)
            {
                Console.WriteLine("The end-date was more previous then start - date.");
                currentArguments[0] = EnterDate("start-date", false);
                currentArguments[1] = EnterDate("end-date", false);
            }

            if (getArg.GitPath == "no path found")
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
            string[] array = new string[3];
            Console.WriteLine("You have to pass 3 arguments for GitReport to work. ");
            array[0] = EnterDate("start-date", false);
            array[1] = EnterDate("end-date", false);
            array[2] = EnterPath(false);

            return array;
        }
    }
}
