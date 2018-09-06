﻿using System;
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
        public string[] FixDatePathError(string[] currentArguments)
        {
            if (this.gitArgument.DateSince == DateTime.MinValue)
            {
                currentArguments[0] = 
                    EnterDate("start-date", DateFormatMessage.WrongFormat);
            }

            if (this.gitArgument.DateBefore == DateTime.MinValue)
            {
                currentArguments[1] = 
                    EnterDate("end-date", DateFormatMessage.WrongFormat);
            }

            if (this.gitArgument.DateSince > this.gitArgument.DateBefore &&
                this.gitArgument.DateSince != DateTime.MinValue &&
                this.gitArgument.DateBefore != DateTime.MinValue)
            {
                Console.WriteLine("The end-date was more previous then start-date.");
                currentArguments[0] = 
                    EnterDate("start-date", DateFormatMessage.EndDateMorePrevious);
                currentArguments[1] = 
                    EnterDate("end-date", DateFormatMessage.EndDateMorePrevious);
            }

            if (this.gitArgument.GitPath == string.Empty)
            {
                currentArguments[2] = 
                    EnterPath(PathCompatibilityMessage.WrongPath);
            }

            return currentArguments;
        }
        private string EnterDate(string whichDate, DateFormatMessage message)
        {
            switch (message)
            {
                case DateFormatMessage.WrongFormat :
                {
                    Console.WriteLine("Format that you used while entering the " +
                            $"{whichDate} was wrong.");
                    break;
                }
            }
            Console.WriteLine($"Please enter new {whichDate}:");

            return Console.ReadLine();
        }
        private string EnterPath(PathCompatibilityMessage message)
        {
            switch (message)
            {
                case PathCompatibilityMessage.WrongPath :
                {
                    Console.WriteLine("There is something wrong with the path you have entered." +
                    " Be sure to use folder connected to GitHub. Please try again.");
                    break;
                }
            }
            return Console.ReadLine();
        }
    }
    public enum DateFormatMessage
    {
        EndDateMorePrevious,
        WrongFormat
    }
    public enum PathCompatibilityMessage
    {
        WrongPath
    }
}
