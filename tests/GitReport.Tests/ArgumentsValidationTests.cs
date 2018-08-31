using ReportCreator;
using Xunit;

namespace GitReport.Tests
{
    public class ArgumentsValidationTests 
    {
        [Fact]
        public void AreDatesPathValid_ArgumentsInOrder_ValidResult()
        {
            ArgumentsValidation validation = 
                new ArgumentsValidation(new GitArguments(), 
                new ArgumentsValidationTestsHelper());    

            var isValid = validation.AreDatesPathValid(new[]
            {
                "01/01/2018",
                "01/15/2018",
                @"C:\git"
            });

            Assert.True(isValid);
            Assert.Equal(15, validation.GitArgument.DateBefore.Day);
            Assert.Equal(1, validation.GitArgument.DateBefore.Month);
            Assert.Equal(2018, validation.GitArgument.DateBefore.Year);
            Assert.Equal(1, validation.GitArgument.DateSince.Day);
            Assert.Equal(1, validation.GitArgument.DateSince.Month);
            Assert.Equal(2018, validation.GitArgument.DateSince.Year);
            Assert.Contains("git", validation.GitArgument.GitPath);
        }

        [Fact]
        public void AreDatesPathValid_SinceDateWrongFormat_InvalidResult()
        {
            ArgumentsValidation validation =
                new ArgumentsValidation(new GitArguments(), 
                new ArgumentsValidationTestsHelper());

            var isValid = validation.AreDatesPathValid(new[]
            {
                "01/012/2018",
                "11/15/2018",
                @"C:\git"
            });

            Assert.False(isValid);
        }

        [Fact]
        public void AreDatesPathValid_BeforeDateWrongFormat_InvalidResult()
        {
            ArgumentsValidation validation =
                new ArgumentsValidation(new GitArguments(), 
                new ArgumentsValidationTestsHelper());

            var isValid = validation.AreDatesPathValid(new[]
            {
                "01/01/2018",
                "11/15123/2018",
                @"C:\git"
            });

            Assert.False(isValid);
        }

        [Fact]
        public void AreDatesPathValid_BeforeDateMorePrevious_InvalidResult()
        {
            ArgumentsValidation validation =
                new ArgumentsValidation(new GitArguments(), 
                new ArgumentsValidationTestsHelper());

            var isValid = validation.AreDatesPathValid(new[]
            {
                "01/01/2018",
                "11/15/2017",
                @"C:\git"
            });

            Assert.False(isValid);
        }

        [Fact]
        public void AreDatesPathValid_WrongPathFromInput_InvalidResult()
        {
            ArgumentsValidation validation =
                new ArgumentsValidation(new GitArguments(), 
                new ArgumentsValidationTestsHelper());

            var isValid = validation.AreDatesPathValid(new[]
            {
                "01/01/2018",
                "01/15/2018",
                @"C:\asdasdasd"
            });

            Assert.False(isValid);
        }
    }
}
