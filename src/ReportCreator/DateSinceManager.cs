using System;

namespace ReportCreator
{
    public class DateSinceManager 
    {
        private IDaysNumber daysHandler;
        public DateSinceManager(IDaysNumber numberHandler)
        {
            this.daysHandler = numberHandler;
        }
        public string GetDateSince(DaysNumber.FromToday daysNumber)
        {
            var numberOfDays = this.daysHandler.GetNumberOfDays(daysNumber);
            DateTime dateSince;
           
            var hour = DateTime.Now.Hour;
            dateSince = (hour < 10 && daysNumber == DaysNumber.FromToday.OneDay)
                ? DateTime.Today.AddDays(-(numberOfDays + 1)) :
                  DateTime.Today.AddDays(-numberOfDays);

            return dateSince.ToString();
        }
        
    }
}
