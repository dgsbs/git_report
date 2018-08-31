using System.Collections.Generic;
using Xunit;
using ReportCreator;

namespace GitReport.Tests
{
    public class ReportCreatorTests
    {
        [Fact]
        public void CreateReport_ManyCommits_NumberOfDictionaryObjectsEqualsNumberIntended()
        {
            GitReportCreator reportCreator = 
                new GitReportCreator(new ReportCreatorTestsHelper(),
                new Dictionary<string, ComponentData>(), new Dictionary<string, CommitData>());

            string testOutput = @"divideLine
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

            Assert.Equal(2, reportCreator.CommitDictionary.Count);
            Assert.Equal(2, reportCreator.ComponentDictionary.Count);
        }
       
        [Fact]
        public void CreateReport_ManyCommits_NumberOfDictionaryObjectsEqualsEmptyInput()
        {
            GitReportCreator reportCreator = 
                new GitReportCreator(new ReportCreatorTestsHelper(),
                new Dictionary<string, ComponentData>(), new Dictionary<string, CommitData>());

            string testOutput = "";

            reportCreator.CreateFullReport(testOutput);

            Assert.Empty(reportCreator.CommitDictionary);
            Assert.Empty(reportCreator.ComponentDictionary);
        }

        [Fact]
        public void CreateReport_OneCommit_CommitDictionaryDataEqualsDataFromCommit()
        {
            GitReportCreator reportCreator = 
                new GitReportCreator(new ReportCreatorTestsHelper(),
                new Dictionary<string, ComponentData>(), new Dictionary<string, CommitData>());

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

            Assert.Equal("Bruce Lee", reportCreator.CommitDictionary
                ["asdasdasd234234sedfdsf2323aewas23"].CommiterName);
            Assert.Equal("05/02/2018", reportCreator.CommitDictionary
                ["asdasdasd234234sedfdsf2323aewas23"].CommitDate);
            Assert.Equal("Changes needed", reportCreator.CommitDictionary
                ["asdasdasd234234sedfdsf2323aewas23"].CommitMessage);
            Assert.True(reportCreator.CommitDictionary.ContainsKey
                ("asdasdasd234234sedfdsf2323aewas23"));
        }

        [Fact]
        public void CreateReport_ManyCommits_ComponentDictionaryDataEqualsDataFromCommit()
        {
            GitReportCreator reportCreator = 
                new GitReportCreator(new ReportCreatorTestsHelper(),
                new Dictionary<string, ComponentData>(), new Dictionary<string, CommitData>());

            string testOutput = @"divideLine
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

            Assert.Equal(2, reportCreator.ComponentDictionary
                ["asdasdasd234234sedfdsf2323aewas23Common.Knowledge"].InsertionCounter);
            Assert.Equal(3, reportCreator.ComponentDictionary
                ["asdasdasd234234sedfdsf2323aewas23Common.Knowledge"].DeletionCounter);
            Assert.True(reportCreator.ComponentDictionary.ContainsKey
                ("asdasdasd234234sedfdsf2323aewas23Common.Knowledge"));
        }
    }
}
