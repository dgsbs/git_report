using Moq;
using Xunit;
using ReportCreator;

namespace GitReport.Tests
{
    public class ArgumentsValidationTestsUsingMoq
    {
        IDirectoryValidation validationMock = Mock.Of<IDirectoryValidation>(directoryValidation =>
            directoryValidation.IsDirectoryValid(It.Is<string>(path => path == @"C:\git")) == true);
        [Fact]
        public void AreDatesPathValid_ArgumentsInOrder_ValidResult()
        {
            var validation = new ArgumentsValidation(new GitArguments(), validationMock);
            var validator = validation.AreDatesPathValid(new[]
            {
                "01/01/2018",
                "01/15/2018",
                @"C:\git"
            });

            Assert.True(validator);
            Assert.Equal(15, validation.GitArgument.DateBefore.Day);
            Assert.Equal(1, validation.GitArgument.DateBefore.Month);
            Assert.Equal(2018, validation.GitArgument.DateBefore.Year);
            Assert.Equal(1, validation.GitArgument.DateSince.Day);
            Assert.Equal(1, validation.GitArgument.DateSince.Month);
            Assert.Equal(2018, validation.GitArgument.DateSince.Year);

        }
        [Fact]
        public void AreDatesPathValid_SinceDateWrongFormat_InvalidResult()
        {
            var validation = new ArgumentsValidation(new GitArguments(), validationMock);
            var validator = validation.AreDatesPathValid(new[]
            {
                "01/012/2018",
                "11/15/2018",
                @"C:\git"
            });

            Assert.False(validator);
        }

        [Fact]
        public void AreDatesPathValid_BeforeDateWrongFormat_InvalidResult()
        {
            var validation = new ArgumentsValidation(new GitArguments(), validationMock);
            var validator = validation.AreDatesPathValid(new[]
            {
                "01/01/2018",
                "11/15123/2018",
                @"C:\git"
            });

            Assert.False(validator);
        }

        [Fact]
        public void AreDatesPathValid_BeforeDateMorePrevious_InvalidResult()
        {
            var validation = new ArgumentsValidation(new GitArguments(), validationMock);
            var validator = validation.AreDatesPathValid(new[]
            {
                "01/01/2018",
                "11/15/2017",
                @"C:\git"
            });

            Assert.False(validator);
        }

        [Fact]
        public void AreDatesPathValid_WrongPathFromInput_InvalidResult()
        {
            var validation = new ArgumentsValidation(new GitArguments(), validationMock);
            var validator = validation.AreDatesPathValid(new[]
            {
                "01/01/2018",
                "01/15/2018",
                @"C:\asdasdasd"
            });

            Assert.False(validator);
        }
    }
}
