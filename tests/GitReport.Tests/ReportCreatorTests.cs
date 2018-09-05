using Xunit;
using ReportCreator;

namespace GitReport.Tests
{
    public class ReportCreatorTests
    {
        [Fact]
        public void CreateReport_ManyCommits_DictionaryKeysAreEqualToThoseIntededFromCommits()
        {
            var reportCreator = new GitReportCreator(new ReportCreatorTestsHelper());

            const string testOutput = @"divideLine
                                        asdasdasd234234sedfdsf2323aewas23
                                        Bruce Lee
                                        05/02/2018
                                        Changes needed
                                        smallLine
                                        33      3       Star/Wars/Ilike.txt
                                        1       2       Common/Knowledge/Start/WhenReady.txt
                                        1       1       Common/Knowledge/Start/Abort.txt
                                        62      5       Computer/IsSlow/Why/Google.txt
                                        divideLine
                                        asdasdasd234234sasdedfdsf2323aewas23
                                        Bruce Leess
                                        05/02/2019
                                        Changes neasdeded
                                        smallLine
                                        33      3       Star/Wars//Ilike.txt
                                        1       2       Common/Knowledge/Start/WhenReady.txt
                                        1       1       Common/Knowledge/Start/Abort.txt
                                        62      5       Computer/IsSlow/Why/Google.txt";

            var reportHandler = reportCreator.CreateCompleteDictionary(testOutput);
            var dictionaryEnumerator = reportHandler.GetEnumerator();

            dictionaryEnumerator.MoveNext();
            Assert.Equal("asdasdasd234234sedfdsf2323aewas23", dictionaryEnumerator.Current.Key.CommitHash);
            Assert.Equal("Common.Knowledge", dictionaryEnumerator.Current.Key.ComponentId);

            dictionaryEnumerator.MoveNext();
            Assert.Equal("asdasdasd234234sasdedfdsf2323aewas23", dictionaryEnumerator.Current.Key.CommitHash);
            Assert.Equal("Common.Knowledge", dictionaryEnumerator.Current.Key.ComponentId);
        }

        [Fact]
        public void CreateReport_ZeroCommits_NumberOfObjectsInDictionaryEqualsEmptyInput()
        {
            var reportCreator = new GitReportCreator(new ReportCreatorTestsHelper());

            const string testOutput = "";

            var reportHandler = reportCreator.CreateCompleteDictionary(testOutput);

            Assert.Empty(reportHandler);
        }

        [Fact]
        public void CreateReport_OneCommit_DataFromCreatedDictioanryEqualsToDataFromCommit()
        {
            var reportCreator = new GitReportCreator(new ReportCreatorTestsHelper());

            const string testOutput = @"divideLine
                                        asdasdasd234234sedfdsf2323aewas23
                                        Bruce Lee
                                        05/02/2018
                                        Changes needed
                                        smallLine
                                        33      3       Star/Wars/Ilike.txt
                                        1       2       Common/Knowledge/Start/WhenReady.txt
                                        1       1       Common/Knowledge/Start/Abort.txt
                                        62      5       Computer/IsSlow/Why/Google.txt";

            var reportHandler = reportCreator.CreateCompleteDictionary(testOutput);
            var dictionaryEnumerator = reportHandler.GetEnumerator();

            dictionaryEnumerator.MoveNext();
            Assert.Equal("Bruce Lee", dictionaryEnumerator.Current.Value.CommiterName);
            Assert.Equal("05/02/2018", dictionaryEnumerator.Current.Value.CommitDate);
            Assert.Equal("Changes needed", dictionaryEnumerator.Current.Value.CommitMessage);
            Assert.Equal(2, dictionaryEnumerator.Current.Value.InsertionCounter);
            Assert.Equal(3, dictionaryEnumerator.Current.Value.DeletionCounter);
        }
    }
}
