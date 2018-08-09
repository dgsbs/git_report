using GitReport.CLI;
using System.Collections.Generic;
using Xunit;
using GitCounter;
using System;
using System.IO;
namespace GitReport.Tests
{
    public class GitReportTests
    {
        GitDiffArguments gitargument = new GitDiffArguments();
        Dictionary<string, ModificationCounters> dictionaryManager = new Dictionary<string, ModificationCounters>();
        IJsonConfig jsonManager = new ReportCreatorForTests();
        [Fact]
        public void DatesPathAllValid()
        {
            IGitValidation validation = new ValidationForTests(gitargument);

            var isValid = validation.AreDatesAndPathValid(new[]
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
        public void DateSinceNotValid()
        {
            GitDiffArgumentsValidation validation = new GitDiffArgumentsValidation(gitargument);

            var isValid = validation.AreDatesAndPathValid(new[]
            {
                "01/012/2018",
                "11/15/2018",
                @"C:\git"
            });
            Assert.False(isValid);
        }
        [Fact]
        public void DateBeforeNotValid()
        {
            GitDiffArgumentsValidation validation = new GitDiffArgumentsValidation(gitargument);

            var isValid = validation.AreDatesAndPathValid(new[]
            {
                "01/01/2018",
                "11/15123/2018",
                @"C:\git"
            });
            Assert.False(isValid);
        }
        [Fact]
        public void DateBeforeMorePrevious()
        {
            GitDiffArgumentsValidation validation = new GitDiffArgumentsValidation(gitargument);

            var isValid = validation.AreDatesAndPathValid(new[]
            {
                "01/01/2018",
                "11/15/2017",
                @"C:\git"
            });
            Assert.False(isValid);
        }
        [Fact]
        public void PathNotValid()
        {
            GitDiffArgumentsValidation validation = new GitDiffArgumentsValidation(gitargument);

            var isValid = validation.AreDatesAndPathValid(new[]
            {
                "01/01/2018",
                "01/15/2018",
                @"C:\asdasd"
            });
            Assert.False(isValid);
        }
        [Fact]
        public void CreateReport_TestManyLines()
        {
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
        [Fact]
        public void CreateReport_TestSingleLine()
        {
            ReportCreator reportCreator = new ReportCreator(jsonManager);
            string testOutput = "1       1       Common/Knowledge/Start/Abort.txt\n";
            dictionaryManager = reportCreator.CreateReport(testOutput);

            Assert.Equal(1, dictionaryManager["Common.Knowledge"].InsertionCounter);
            Assert.Equal(1, dictionaryManager["Common.Knowledge"].DeletionCounter);
            Assert.Single(dictionaryManager);
        }
        [Fact]
        public void CreateReport_TestEmptyLine()
        {
            ReportCreator reportCreator = new ReportCreator(jsonManager);
            string testOutput = "";
            dictionaryManager = reportCreator.CreateReport(testOutput);

            Assert.Empty(dictionaryManager);
        }
        [Fact]
        public void CreateReport_TestWrongInput()
        {
            ReportCreator reportCreator = new ReportCreator(jsonManager);
            string testOutput = "62      5       Computer/IsSlow/Why/Google.txt\n";
            dictionaryManager = reportCreator.CreateReport(testOutput);

            Assert.Empty(dictionaryManager);
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
    class ValidationForTests : IGitValidation
    {
        GitDiffArguments gitArgument = new GitDiffArguments();
        public ValidationForTests(GitDiffArguments gitArgument)
        {
            this.gitArgument = gitArgument;
        }
        public bool AreDatesAndPathValid(string[] arguments)
        {
            bool sinceDateValidator = DateTime.TryParse(arguments[0], out var SinceDate);
            gitArgument.DateSince = SinceDate;

            bool beforeDateValidator = DateTime.TryParse(arguments[1], out var BeforeDate);
            gitArgument.DateBefore = BeforeDate;

            string localPath = @"C:\git";
            bool pathExistenceValidator = false;

            if (arguments[2].Contains(localPath))
            {
                gitArgument.GitPath = arguments[2];
            }

            if (!sinceDateValidator || !beforeDateValidator || !pathExistenceValidator ||
                SinceDate > BeforeDate)
            {
                return false;
            }
            return true;
        }
    }
}
