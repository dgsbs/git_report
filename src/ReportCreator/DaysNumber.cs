namespace ReportCreator
{
    public class DaysNumber : IDaysNumber
    {
        public int GetNumberOfDays(FromToday numberOfDays)
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
        public enum FromToday
        {
            OneDay,
            OneWeek,
            FourWeeks
        }
    }
}
