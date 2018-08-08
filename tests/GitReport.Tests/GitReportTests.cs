using GitReport.CLI;
using System.Collections.Generic;
using Xunit;
namespace GitReport.Tests
{
    public class GitReportTests
    {
        [Fact]
        public void DatesPathAllValid()
        {
            var gitarguments = new GitDiffArguments();
            GitDiffArgumentsValidation validation = new GitDiffArgumentsValidation(gitarguments);

            var isValid = validation.AreDatesAndPathValid(new[]
            {
                "01/01/2018",
                "01/15/2018",
                @"C:\git"
            });

            Assert.True(isValid);
            Assert.Equal(15, gitarguments.DateBefore.Day);
            Assert.Equal(1, gitarguments.DateBefore.Month);
            Assert.Equal(2018, gitarguments.DateBefore.Year);
            Assert.Equal(1, gitarguments.DateSince.Day);
            Assert.Equal(1, gitarguments.DateSince.Month);
            Assert.Equal(2018, gitarguments.DateSince.Year);
            Assert.Contains("git", gitarguments.GitPath);
        }
        [Fact]
        public void DateSinceNotValid()
        {
            var gitarguments = new GitDiffArguments();
            GitDiffArgumentsValidation validation = new GitDiffArgumentsValidation(gitarguments);

            var isValid = validation.AreDatesAndPathValid(new[]
            {
                "01/012/2018",
                "11/15/2018",
                @"C:\git"
            });
            Assert.True(isValid);
        }
        [Fact]
        public void DateBeforeNotValid()
        {
            var gitarguments = new GitDiffArguments();
            GitDiffArgumentsValidation validation = new GitDiffArgumentsValidation(gitarguments);

            var isValid = validation.AreDatesAndPathValid(new[]
            {
                "01/01/2018",
                "11/15123/2018",
                @"C:\git"
            });
            Assert.True(isValid);
        }
        [Fact]
        public void DateBeforeMorePrevious()
        {
            var gitarguments = new GitDiffArguments();
            GitDiffArgumentsValidation validation = new GitDiffArgumentsValidation(gitarguments);

            var isValid = validation.AreDatesAndPathValid(new[]
            {
                "01/01/2018",
                "11/15/2017",
                @"C:\git"
            });
            Assert.True(isValid);
        }
        [Fact]
        public void PathNotValid()
        {
            var gitarguments = new GitDiffArguments();
            GitDiffArgumentsValidation validation = new GitDiffArgumentsValidation(gitarguments);

            var isValid = validation.AreDatesAndPathValid(new[]
            {
                "01/01/2018",
                "01/15/2018",
                @"C:\asdasd"
            });
            Assert.True(isValid);
        }
        [Fact]
        public void CreateReport_Test()
        {
            Dictionary<string, ModificationCounters> dictionaryManager = new Dictionary<string, ModificationCounters>();
            IJsonConfig jsonManager = new ReportCreatorForTests();
            ReportCreator reportCreator = new ReportCreator(jsonManager);
            string testOutput = "33      3       Star/Wars//Ilike.txt\n" +
                "1       2       Common/Knowledge/Start/WhenReady.txt\n" +
                "1       1       Common/Knowledge/Start/Abort.txt\n" +
                "62      5       Computer/IsSlow/Why/Google.txt\n";
            dictionaryManager = reportCreator.CreateReport(testOutput);

            Assert.Equal(2, dictionaryManager["Common.Knowledge"].InsertionCounter);
            Assert.Equal(3, dictionaryManager["Common.Knowledge"].DeletionCounter);
            Assert.Single(dictionaryManager);
        }
    }
    class ReportCreatorForTests : IJsonConfig
    {
        public bool TryMatchPath(string pathFromProcess, out string finalId)
        {
            if (pathFromProcess.Contains("Common/Knowledge/Start/"))
            {
                finalId = "Common.Knowledge"; 
                return true;
            }
            finalId = string.Empty;
            return false;
        }
    }
}
