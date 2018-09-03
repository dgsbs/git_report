using Xunit;
using ReportCreator;

namespace GitReport.Tests
{
    public class ReportCreatorTests
    {
        [Fact]
        public void CreateReport_ManyCommits_NumberOfObjectsCreatedEqualsNumberIntended()
        {
            GitReportCreator reportCreator = 
                new GitReportCreator(new ReportCreatorTestsHelper());

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

            reportCreator.CreateFullReport(testOutput);

            Assert.Equal(2, reportCreator.CommitList.Count);
            Assert.Equal(2, reportCreator.ComponentDictionary.Count);
        }
       
        [Fact]
        public void CreateReport_ManyCommits_NumberOfObjectsEqualsEmptyInput()
        {
            GitReportCreator reportCreator =
                new GitReportCreator(new ReportCreatorTestsHelper());

            string testOutput = "";

            reportCreator.CreateFullReport(testOutput);

            Assert.Empty(reportCreator.CommitList);
            Assert.Empty(reportCreator.ComponentDictionary);
        }

        [Fact]
        public void CreateReport_OneCommit_CommitDictionaryDataEqualsDataFromCommit()
        {
            GitReportCreator reportCreator =
                new GitReportCreator(new ReportCreatorTestsHelper());

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

            reportCreator.CreateFullReport(testOutput);

            Assert.Equal("Bruce Lee", reportCreator.CommitList[0].CommiterName);
            Assert.Equal("05/02/2018", reportCreator.CommitList[0].CommitDate);
            Assert.Equal("Changes needed", reportCreator.CommitList[0].CommitMessage);
            Assert.Equal("asdasdasd234234sedfdsf2323aewas23", 
                reportCreator.CommitList[0].CommitHash);
        }

        [Fact]
        public void CreateReport_ManyCommits_ComponentDictionaryDataEqualsDataFromCommit()
        {
            GitReportCreator reportCreator =
                new GitReportCreator(new ReportCreatorTestsHelper());
            
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

            reportCreator.CreateFullReport(testOutput);

            var dictionaryEnumerator = reportCreator.ComponentDictionary.GetEnumerator();
            dictionaryEnumerator.MoveNext();

            Assert.Single(reportCreator.ComponentDictionary);
            Assert.Equal(2, reportCreator.ComponentDictionary
                [dictionaryEnumerator.Current.Key].InsertionCounter);
            Assert.Equal(3, reportCreator.ComponentDictionary
                [dictionaryEnumerator.Current.Key].DeletionCounter);
            Assert.True(reportCreator.ComponentDictionary.ContainsKey
                (dictionaryEnumerator.Current.Key));
        }
    }
}
