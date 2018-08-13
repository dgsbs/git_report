using GitCounter;
using Xunit;

namespace GitReport.Tests
{
    public class ArgumentsValidationTests 
    {
        GitDiffArguments gitArgument = new GitDiffArguments();
        IDirectoryValidation directoryValidation = 
            new ArgumentsValidationTestsHelper();
        [Fact]
        public void AreDatesPathValid_ArgumentsInOrder_ValidResult()
        {
            ArgumentsValidation validation = 
                new ArgumentsValidation(gitArgument, directoryValidation);    

            var isValid = validation.AreDatesPathValid(new[]
            {
                "01/01/2018",
                "01/15/2018",
                @"C:\git"                
            });

            Assert.True(isValid);
            Assert.Equal(15, gitArgument.DateBefore.Day);
            Assert.Equal(1, gitArgument.DateBefore.Month);
            Assert.Equal(2018, gitArgument.DateBefore.Year);
            Assert.Equal(1, gitArgument.DateSince.Day);
            Assert.Equal(1, gitArgument.DateSince.Month);
            Assert.Equal(2018, gitArgument.DateSince.Year);
            Assert.Contains("git", gitArgument.GitPath);
        }

        [Fact]
        public void AreDatesPathValid_SinceDateWrongFormat_InvalidResult()
        {
            ArgumentsValidation validation = 
                new ArgumentsValidation(gitArgument, directoryValidation);

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
                new ArgumentsValidation(gitArgument, directoryValidation);

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
                new ArgumentsValidation(gitArgument, directoryValidation);

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
                new ArgumentsValidation(gitArgument, directoryValidation);

            var isValid = validation.AreDatesPathValid(new[]
            {
                "01/01/2018",
                "01/15/2018",
                @"C:\asdasd"
            });

            Assert.False(isValid);
        }
    }
}
