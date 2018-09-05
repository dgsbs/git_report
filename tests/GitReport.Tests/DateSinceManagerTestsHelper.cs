using ReportCreator;

namespace GitReport.Tests
{
    class DateSinceManagerTestsHelper : IDaysNumber
    {
        public int GetNumberOfDays(DaysNumber.FromToday numberOfDays)
        {
            switch (numberOfDays)
            {
                case DaysNumber.FromToday.OneDay:
                {
                    return 12;
                }
                case DaysNumber.FromToday.OneWeek:
                {
                    return 1221;
                }
                case DaysNumber.FromToday.FourWeeks:
                {
                    return 128;
                }
            }
            return 0;
        }
    }
}
