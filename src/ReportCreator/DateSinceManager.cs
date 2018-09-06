using System;

namespace ReportCreator
{
    public class DateSinceManager 
    {
        public string GetDateString(FromToday daysNumber)
        {
            var numberOfDays = GetNumberOfDays(daysNumber);
            DateTime dateSince;
           
            var hour = DateTime.Now.Hour;
            dateSince = (hour < 10 && daysNumber == FromToday.OneDay)
                ? DateTime.Today.AddDays(-(numberOfDays + 1)) :
                  DateTime.Today.AddDays(-numberOfDays);

            return dateSince.ToString();
        }
        private int GetNumberOfDays(FromToday numberOfDays)
        {
            switch (numberOfDays)
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
