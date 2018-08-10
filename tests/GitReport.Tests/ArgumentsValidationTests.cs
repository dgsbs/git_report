using GitCounter;
using Xunit;

namespace GitReport.Tests
{
    public class ArgumentsValidationTests 
    {
        GitDiffArguments gitargument = new GitDiffArguments();
        IDirectoryValidation directoryValidation = 
            new ArgumentsValidationTestsHelper();
        [Fact]
        public void AreDatesPathValid_ArgumentsInOrder_ValidResult()
        {
            ArgumentsValidation validation = 
                new ArgumentsValidation(gitargument, directoryValidation);    

            var isValid = validation.AreDatesPathValid(new[]
            {
                "01/01/2018",
                "01/15/2018",
                @"C:\git"                
            });

            Assert.True(isValid);
            Assert.Equal(15, gitargument.DateBefore.Day);
            Assert.Equal(1, gitargument.DateBefore.Month);
            Assert.Equal(2018, gitargument.DateBefore.Year);
            Assert.Equal(1, gitargument.DateSince.Day);
            Assert.Equal(1, gitargument.DateSince.Month);
            Assert.Equal(2018, gitargument.DateSince.Year);
            Assert.Contains("git", gitargument.GitPath);
        }

        [Fact]
        public void AreDatesPathValid_SinceDateWrongFormat_InvalidResult()
        {
            ArgumentsValidation validation = 
                new ArgumentsValidation(gitargument, directoryValidation);

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
                new ArgumentsValidation(gitargument, directoryValidation);

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
                new ArgumentsValidation(gitargument, directoryValidation);

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
                new ArgumentsValidation(gitargument, directoryValidation);

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
