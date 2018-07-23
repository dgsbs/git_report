using System;
using System.Globalization;
using System.IO;
namespace GitReport.CLI
{
    class DateAndPathValidation                        
    {
        public GitReportArguments GetArgsForGitDiff
        {
            get;
            set;
        } = new GitReportArguments();
        public ValidationErrorsHandler GetErrorsHandled
        {
            get;
        } = new ValidationErrorsHandler();
        private string DateValidation(string arg, int whichDate)
        {
            string[] formats = new string[] { "dd/MM/yyyy", "d/M/yyyy", "MM/dd/yyyy",
                "M/d/yyyy","dd.MM.yyyy", "d.M.yyyy", "MM-dd-yyyy", "M-d-yyyy",
                "dd-MM-yyyy", "d-M-yyyy", "MM.dd.yyyy", "M.d.yyyy" };

            while (!DateTime.TryParseExact(arg, formats, GetArgsForGitDiff.CultureInfo, 
                    DateTimeStyles.None, out GetArgsForGitDiff.dateSince))        
            {
                arg = GetErrorsHandled.HandleDateFormatError(whichDate);
            }
            arg = GetArgsForGitDiff.dateSince.ToString();
            return arg;
        }
        public string PathValidation(string arg)
        {
            if (arg == null)
            {
                return PathValidationLoop(arg);
            }
            if (arg.Contains("Phoenix"))                       
            {
                return PathValidationLoop(arg);
            }
            else
            {
                return PathValidationLoop(GetErrorsHandled.EnterPath());
            }
        }
        public string PathValidationLoop(string arg)
        {
            while (!Directory.Exists(arg))
            {
                arg = GetErrorsHandled.EnterPath();
            }
            return arg;
        }
        public void SettingWrongArgsRight(int wrongDate )
        {
            GetArgsForGitDiff.GitArguments[0] = DateValidation(GetArgsForGitDiff.
                GitArguments[0], wrongDate);
            GetArgsForGitDiff.GitArguments[1] = DateValidation(GetArgsForGitDiff.
                GitArguments[1], wrongDate);


            DateTime.TryParse(GetArgsForGitDiff.GitArguments[0],
                out GetArgsForGitDiff.dateSince);
            DateTime.TryParse(GetArgsForGitDiff.GitArguments[1], 
                out GetArgsForGitDiff.dateBefore);

            if (GetArgsForGitDiff.dateSince > GetArgsForGitDiff.dateBefore)
            {
                SettingWrongArgsRight(wrongDate);
            }
        }
        public void ValidatePathAndDate()
        {
            for (int i = 0; i < GetArgsForGitDiff.GitArguments.Length; i++)
            {
                if (i == GetArgsForGitDiff.GitArguments.Length - 1)
                {
                    GetArgsForGitDiff.GitArguments[i] = PathValidation
                        (GetArgsForGitDiff.GitArguments[i]);
                }
                else
                {
                    GetArgsForGitDiff.GitArguments[i] = DateValidation(
                        GetArgsForGitDiff.GitArguments[i],i);
                }
            }

            DateTime.TryParse(GetArgsForGitDiff.GitArguments[0], 
                out GetArgsForGitDiff.dateSince);
            DateTime.TryParse(GetArgsForGitDiff.GitArguments[1], out 
                GetArgsForGitDiff.dateBefore);

            if (GetArgsForGitDiff.dateSince > GetArgsForGitDiff.dateBefore)
            {
                SettingWrongArgsRight(2);
            }
        }
    }
}

