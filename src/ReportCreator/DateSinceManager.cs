using System;

namespace ReportCreator
{
    public class DateSinceManager 
    {
        public string GetDateString(FromToday daysNumber)
        {
            var numberOfDays = GetNumberOfDays(daysNumber);
            DateTime dateSince;
            bool isMonday = false;
            var hour = DateTime.Now.Hour;

            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday
                && hour < 10 )
            {
                numberOfDays += 2;
                isMonday = true;
            }

            dateSince = (hour < 10 && daysNumber == FromToday.OneDay && isMonday == false)
                ? DateTime.Today.AddDays(-(numberOfDays + 1)) :
                  DateTime.Today.AddDays(-numberOfDays);

            return dateSince.ToString();
        }
        private int GetNumberOfDays(FromToday daysNumber)
        {
            switch (daysNumber)
            {
                case FromToday.OneDay:
                    {
                        return 1;
                    }
                case FromToday.OneWeek:
                    {
                        return 7;
                    }
                case FromToday.FourWeeks:
                    {
                        return 28;
                    }
            }
            return 0;
        }
    }
    public enum FromToday
    {
        OneDay,
        OneWeek,
        FourWeeks
    }
}
