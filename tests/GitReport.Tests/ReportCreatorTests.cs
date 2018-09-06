using Xunit;
using ReportCreator;

namespace GitReport.Tests
{
    public class ReportCreatorTests
    {
        [Fact]
        public void CreateReport_MultipleCommits_CreatedDictionaryContainsCorrectKeys()
        {
            var reportCreator = new GitReportCreator(new JsonConfigStub());

            const string testOutput = @"divideLine
                                        123
                                        Bruce Lee
                                        05/02/2018
                                        Changes needed
                                        smallLine
                                        33      3       Star/Wars/Ilike.txt
                                        1       2       Common/Knowledge/Start/WhenReady.txt
                                        1       1       Common/Knowledge/Start/Abort.txt
                                        62      5       Computer/IsSlow/Why/Google.txt
                                        divideLine
                                        456
                                        Bruce Leess
                                        05/02/2019
                                        Changes neasdeded
                                        smallLine
                                        33      3       Star/Wars//Ilike.txt
                                        1       2       Common/Knowledge/Start/WhenReady.txt
                                        1       1       Common/Knowledge/Start/Abort.txt
                                        62      5       Computer/IsSlow/Why/Google.txt";

            var reportHandler = reportCreator.GetReportDictionary(testOutput);
            Assert.True(reportHandler.ContainsKey
                (new HashId { CommitHash = "123", ComponentId = "Common.Knowledge" }));
            Assert.True(reportHandler.ContainsKey
                (new HashId { CommitHash = "123", ComponentId = "Computer.Slow" }));
            Assert.True(reportHandler.ContainsKey
                (new HashId { CommitHash = "456", ComponentId = "Common.Knowledge" }));
            Assert.True(reportHandler.ContainsKey
                (new HashId { CommitHash = "456", ComponentId = "Computer.Slow" }));
        }

        [Fact]
        public void CreateReport_MultipleCommits_CreatedDictionaryContainsCorrectValuesUnderConcreteKeys()
        {
            var reportCreator = new GitReportCreator(new JsonConfigStub());

            const string testOutput = @"divideLine
                                        123
                                        Bruce Lee
                                        05/02/2018
                                        Changes needed
                                        smallLine
                                        33      3       Star/Wars/Ilike.txt
                                        1       2       Common/Knowledge/Start/WhenReady.txt
                                        1       1       Common/Knowledge/Start/Abort.txt
                                        62      5       Computer/IsSlow/Why/Google.txt
                                        divideLine
                                        456
                                        Bruce Leess
                                        05/02/2019
                                        Changes neasdeded
                                        smallLine
                                        33      3       Star/Wars//Ilike.txt
                                        1       2       Common/Knowledge/Start/WhenReady.txt
                                        1       1       Common/Knowledge/Start/Abort.txt
                                        62      5       Computer/IsSlow/Why/Google.txt";

            var reportHandler = reportCreator.GetReportDictionary(testOutput);
            Assert.True(reportHandler.TryGetValue (new HashId { CommitHash = "123", ComponentId = "Common.Knowledge" }, out CommitComponentData testOne));
            Assert.Equal("05/02/2018", testOne.CommitDate);
            Assert.Equal("Bruce Lee", testOne.CommiterName);
            Assert.Equal("Changes needed", testOne.CommitMessage);
            Assert.Equal(2, testOne.ComponentInsertions);
            Assert.Equal(3, testOne.ComponentDeletions);

            Assert.True(reportHandler.TryGetValue(new HashId { CommitHash = "456", ComponentId = "Computer.Slow" }, out CommitComponentData testTwo));
            Assert.Equal("05/02/2019", testTwo.CommitDate);
            Assert.Equal("Bruce Leess", testTwo.CommiterName);
            Assert.Equal("Changes neasdeded", testTwo.CommitMessage);
            Assert.Equal(62, testTwo.ComponentInsertions);
            Assert.Equal(5, testTwo.ComponentDeletions);
        }
        [Fact]
        public void CreateReport_ZeroCommits_CreatedDictioanaryIsEmpty()
        {
            var reportCreator = new GitReportCreator(new JsonConfigStub());

            const string testOutput = "";

            var reportHandler = reportCreator.GetReportDictionary(testOutput);

            Assert.Empty(reportHandler);
        }

        [Fact]
        public void CreateReport_MultipleCommits_CreatedDictionaryContainsCorrectValuesUnderConcreteMultipleKeys()
        {
            var reportCreator = new GitReportCreator(new JsonConfigStub());

            const string testOutput = @"divideLine
                                        123
                                        Ce Eee
                                        05/02/2018
                                        Need
                                        smallLine
                                        332     3       Star/Wars1/Ilike.txt
                                        1       2       Common/Knowledge/Start/WhenReady.txt
                                        121     1       Common/Knowledge/Start/Abort.txt
                                        62      5       Computer/IsSlow/Why/Google.txt
                                        divideLine
                                        456
                                        Bruce Leess
                                        05/02/2019
                                        Changes 
                                        smallLine
                                        33      3       Star/Wars/Ilike.txt
                                        1       2       Common/Knowledge/Start/WhenReady.txt
                                        1       1       Common/Knowledge/Start/Abort.txt
                                        62      5       Computer/IsSlow/Why/Google.txt
                                        divideLine
                                        789
                                        Bru Leess
                                        05/13/2019
                                        Changes 
                                        smallLine
                                        332     31     Star/Wars/Ilike.txt
                                        1       2      Common/Knowledge/Start/WhenReady.txt
                                        11      11     Common/Knowledge/Start/Abort.txt
                                        6212    5      Computer/IsSlow/Why/Google.txt";

            var reportHandler = reportCreator.GetReportDictionary(testOutput);

            Assert.True(reportHandler.TryGetValue(new HashId { CommitHash = "123", ComponentId = "Common.Knowledge" }, out CommitComponentData testOne));
            Assert.Equal("05/02/2018", testOne.CommitDate);
            Assert.Equal("Ce Eee", testOne.CommiterName);
            Assert.Equal("Need", testOne.CommitMessage);
            Assert.Equal(122, testOne.ComponentInsertions);
            Assert.Equal(3, testOne.ComponentDeletions);

            Assert.True(reportHandler.TryGetValue(new HashId { CommitHash = "456", ComponentId = "Star.Wars" }, out CommitComponentData testTwo));
            Assert.Equal("05/02/2019", testTwo.CommitDate);
            Assert.Equal("Bruce Leess", testTwo.CommiterName);
            Assert.Equal("Changes", testTwo.CommitMessage);
            Assert.Equal(33, testTwo.ComponentInsertions);
            Assert.Equal(3, testTwo.ComponentDeletions);

            Assert.True(reportHandler.TryGetValue(new HashId { CommitHash = "789", ComponentId = "Computer.Slow" }, out CommitComponentData testThree));
            Assert.Equal("05/13/2019", testThree.CommitDate);
            Assert.Equal("Bru Leess", testThree.CommiterName);
            Assert.Equal("Changes", testThree.CommitMessage);
            Assert.Equal(6212, testThree.ComponentInsertions);
            Assert.Equal(5, testThree.ComponentDeletions);
        }
    }
}
