using Xunit;
using ReportCreator;
using System.Collections.Generic;

namespace GitReport.Tests
{
    public class ReportCreatorTests
    {
        [Fact]
        public void CreateReport_ManyCommits_NumberOfObjectsCreatedEqualsNumberIntended()
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

            reportCreator.CreateFullReport(testOutput, out List < CommitData > commitList,
                out Dictionary<ComponentKey, ComponentData> componentDictionary);

            Assert.Equal(2, commitList.Count);
            Assert.Equal(2, componentDictionary.Count);
        }
       
        [Fact]
        public void CreateReport_ManyCommits_NumberOfObjectsEqualsEmptyInput()
        {
            var reportCreator = new GitReportCreator(new ReportCreatorTestsHelper());

            const string testOutput = "";

            reportCreator.CreateFullReport(testOutput, out List<CommitData> commitList,
                out Dictionary<ComponentKey, ComponentData> componentDictionary);

            Assert.Empty(commitList);
            Assert.Empty(componentDictionary);
        }

        [Fact]
        public void CreateReport_OneCommit_CommitDictionaryDataEqualsDataFromCommit()
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

            reportCreator.CreateFullReport(testOutput, out List<CommitData> commitList,
                out Dictionary<ComponentKey, ComponentData> componentDictionary);

            Assert.Equal("Bruce Lee", commitList[0].CommiterName);
            Assert.Equal("05/02/2018", commitList[0].CommitDate);
            Assert.Equal("Changes needed", commitList[0].CommitMessage);
            Assert.Equal("asdasdasd234234sedfdsf2323aewas23", 
                commitList[0].CommitHash);
        }

        [Fact]
        public void CreateReport_ManyCommits_ComponentDictionaryDataEqualsDataFromCommit()
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

            reportCreator.CreateFullReport(testOutput, out List<CommitData> commitList,
                out Dictionary<ComponentKey, ComponentData> componentDictionary); ;

            var dictionaryEnumerator = componentDictionary.GetEnumerator();
            dictionaryEnumerator.MoveNext();

            Assert.Single(componentDictionary);
            Assert.Equal(2, componentDictionary
                [dictionaryEnumerator.Current.Key].InsertionCounter);
            Assert.Equal(3, componentDictionary
                [dictionaryEnumerator.Current.Key].DeletionCounter);
            Assert.True(componentDictionary.ContainsKey
                (dictionaryEnumerator.Current.Key));
        }
    }
}
