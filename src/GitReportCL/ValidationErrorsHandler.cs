using System;
namespace GitReport.CLI
{
    class ValidationErrorsHandler
    {
        public string HandleDateFormatError(int whichError)
        {
            if (whichError == 0)
            {
                EnterNewDate("start-date");
            }
            else if (whichError == 1)
            {
                EnterNewDate("end-date");
            }
            else
            {
                Console.WriteLine("Start-date has to be more previous than end-date. " +
                    "Please enter new date:");
            }
            return Console.ReadLine();
        }
        private void EnterNewDate(string whichDate)
        {
            Console.WriteLine("Format that you used while entering the " + whichDate + 
                " was wrong. Check correct formats shown below and try again.");

            Console.WriteLine("Correct formats:\ndd/MM/yyyy, d/M/yyyy, MM/dd/yyyy,\n" +
                "M/d/yyyy, dd.MM.yyyy, d.M.yyyy,\nMM-dd-yyyy, M-d-yyyy,dd-MM-yyyy,\n" +
                "d-M-yyyy, MM.dd.yyyy, M.d.yyyy");

            Console.WriteLine("Please enter new " + whichDate + ":");
        }
        public string EnterPath()
        {
            Console.WriteLine("There is something wrong with the path you have entered." +
                " Be sure to use folder connected to GitHub. Please try again.");

            return Console.ReadLine();
        }
    }
}
