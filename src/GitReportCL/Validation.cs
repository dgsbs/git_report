

namespace GitReportCL
{
    using System;
    using System.Globalization;
    using System.IO;
    class Validation
    {
            public SetGitReport setGitRep = new SetGitReport();
            private string DateValidation(string arg)
            {
                string[] formats = new string[] { "dd/MM/yyyy", "d/M/yyyy", "MM/dd/yyyy", "M/d/yyyy",
                "dd.MM.yyyy", "d.M.yyyy", "MM-dd-yyyy", "M-d-yyyy", "dd-MM-yyyy", "d-M-yyyy", "MM.dd.yyyy", "M.d.yyyy" };

                while (DateTime.TryParseExact(arg, formats, setGitRep.cultureInfo, DateTimeStyles.None, out setGitRep.dateSince) == false)        //AdjustToUniversal
                {
                    Console.WriteLine("Please enter new date:");
                    arg = Console.ReadLine();
                }
                arg = setGitRep.dateSince.ToString();
                return arg;
            }
            public string PathValidation(string arg)
            {
                if (arg == null)
                {
                    return PathValidationLoop(arg);
                }
                if (arg.Contains("Phoenix"))                     //test           Phoenix
                {
                    return PathValidationLoop(arg);
                }
                else
                {
                    // Console.WriteLine("Your path was not the path to Phoenix repository. Set a new path:");
                    return PathValidationLoop(Console.ReadLine());
                }
            }
            public string PathValidationLoop(string arg)
            {
                while (Directory.Exists(arg) == false)
                    arg = Console.ReadLine();
                return arg;
            }
            private void IncorrectOrFew()
            {
                Console.WriteLine("Incorrect or to few arguments.");                   // Use correct format -> 'dd/MM/yyyy'.\n");
                Console.WriteLine("Set start-date:");
                setGitRep.gitArguments[0] = DateValidation(setGitRep.gitArguments[0]);
                Console.WriteLine("Set end-date:");
                setGitRep.gitArguments[1] = DateValidation(setGitRep.gitArguments[1]);
                Console.WriteLine("Set path:");
                setGitRep.gitArguments[2] = PathValidation(setGitRep.gitArguments[2]);

                if (DateTime.TryParse(setGitRep.gitArguments[0], out setGitRep.dateSince) && DateTime.TryParse(setGitRep.gitArguments[1], out setGitRep.dateBefore) && setGitRep.dateSince > setGitRep.dateBefore)
                {
                    Console.WriteLine("Start-date has to be more previous then end-date. Try again");
                    IncorrectOrFew();
                }
            }
            public void Validate()
            {
                if (DateTime.TryParse(setGitRep.gitArguments[0], out setGitRep.dateSince) && DateTime.TryParse(setGitRep.gitArguments[1], out setGitRep.dateBefore) && setGitRep.dateSince <= setGitRep.dateBefore)
                {
                    for (int i = 0; i < setGitRep.gitArguments.Length; i++)
                    {
                        if (i == setGitRep.gitArguments.Length - 1)
                        {
                            setGitRep.gitArguments[i] = PathValidation(setGitRep.gitArguments[i]);
                        }
                        else
                        setGitRep.gitArguments[i] = DateValidation(setGitRep.gitArguments[i]);
                    }
                }
                else
                    IncorrectOrFew();
            }
        }
}

