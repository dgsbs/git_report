using Xunit;
using ReportCreator;
using System;

namespace GitReport.Tests
{
    public class DateSinceManagerTests
    {
        [Fact]
        public void GetNumberOfDays_OneDay_ReturnValueCorrect()
        {
            var dateHandler = new DateSinceManager();
            var numberOfDays = (DateTime.Now.Hour < 10) ? 2 : 1; 
            DateTime dateSince = DateTime.Today.AddDays(-numberOfDays);
            Assert.Equal(dateSince.ToString(), dateHandler.GetDateString(FromToday.OneDay));

        }
        [Fact]
        public void GetNumberOfDays_OneWeek_ReturnValueCorrect()
        {
            var dateHandler = new DateSinceManager();
            DateTime dateSince = DateTime.Today.AddDays(-7);
            Assert.Equal(dateSince.ToString(), dateHandler.GetDateString(FromToday.OneWeek));

        }
        [Fact]
        public void GetNumberOfDays_FourWeeks_ReturnValueCorrect()
        {
            var dateHandler = new DateSinceManager();
            DateTime dateSince = DateTime.Today.AddDays(-28);
            Assert.Equal(dateSince.ToString(), dateHandler.GetDateString(FromToday.FourWeeks));

        }
        [Fact]
        public void GetNumberOfDays_FourWeeks_ReturnValueIncorrect()
        {
            var dateHandler = new DateSinceManager();
            DateTime dateSince = DateTime.Today.AddDays(-11298);
            Assert.NotEqual(dateSince.ToString(), dateHandler.GetDateString(FromToday.FourWeeks));

        }
    }
}
