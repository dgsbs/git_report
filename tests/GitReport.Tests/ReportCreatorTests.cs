using System.Collections.Generic;
using Xunit;
using GitCounter;

namespace GitReport.Tests
{
    public class ReportCreatorTests
    {
        Dictionary<string, CommitData> commitManager = 
            new Dictionary<string, CommitData>();
        Dictionary<string, ComponentData> componentManager = 
            new Dictionary<string, ComponentData>();
        IJsonConfig jsonManager = new ReportCreatorTestsHelper();

        [Fact]
        public void CreateReport_ManyCommits_NumberOfDictionaryObjectsEqualsNumberIntended()
        {
            ReportCreator reportCreator = new ReportCreator(jsonManager, componentManager,
                commitManager);

            string testOutput = "divideLine\n" +
                "asdasdasd234234sedfdsf2323aewas23\n" +
                "Bruce Lee\n" +
                "05/02/2018\n" +
                "Changes needed\n" +
                "smallLine\n" +
                "33      3       Star/Wars//Ilike.txt\n" +
                "1       2       Common/Knowledge/Start/WhenReady.txt\n" +
                "1       1       Common/Knowledge/Start/Abort.txt\n" +
                "62      5       Computer/IsSlow/Why/Google.txt\n" +
                "divideLine\n" +
                "asdasdasd234234sasdedfdsf2323aewas23\n" +
                "Bruce Leess\n" +
                "05/02/2019\n" +
                "Changes neasdeded\n" +
                "smallLine\n" +
                "33      3       Star/Wars//Ilike.txt\n" +
                "1       2       Common/Knowledge/Start/WhenReady.txt\n" +
                "1       1       Common/Knowledge/Start/Abort.txt\n" +
                "62      5       Computer/IsSlow/Why/Google.txt\n";

            reportCreator.CreateFullReport(testOutput);

            Assert.Equal(2, commitManager.Count);
            Assert.Equal(2, componentManager.Count);
        }
        [Fact]
        public void CreateReport_ManyCommits_NumberOfDictionaryObjectsEqualsEmptyInput()
        {
            ReportCreator reportCreator = new ReportCreator(jsonManager, componentManager,
                commitManager);

            string testOutput = "";

            reportCreator.CreateFullReport(testOutput);

            Assert.Empty(commitManager);
            Assert.Empty(componentManager);
        }

        [Fact]
        public void CreateReport_OneCommit_CommitDictionaryDataEqualsDataFromCommit()
        {
            ReportCreator reportCreator = new ReportCreator(jsonManager, componentManager,
                commitManager);

            string testOutput = "divideLine\n" +
                "asdasdasd234234sedfdsf2323aewas23\n" +
                "Bruce Lee\n" +
                "05/02/2018\n" +
                "Changes needed\n" +
                "smallLine\n" +
                "33      3       Star/Wars//Ilike.txt\n" +
                "1       2       Common/Knowledge/Start/WhenReady.txt\n" +
                "1       1       Common/Knowledge/Start/Abort.txt\n" +
                "62      5       Computer/IsSlow/Why/Google.txt\n";

            reportCreator.CreateFullReport(testOutput);

            Assert.Equal("Bruce Lee", commitManager
                ["asdasdasd234234sedfdsf2323aewas23"].CommiterName);
            Assert.Equal("05/02/2018", commitManager
                ["asdasdasd234234sedfdsf2323aewas23"].CommitDate);
            Assert.Equal("Changes needed", commitManager
                ["asdasdasd234234sedfdsf2323aewas23"].CommitMessage);
            Assert.True(commitManager.ContainsKey("asdasdasd234234sedfdsf2323aewas23"));
        }

        [Fact]
        public void CreateReport_ManyCommits_ComponentDictionaryDataEqualsDataFromCommit()
        {
            ReportCreator reportCreator = new ReportCreator(jsonManager, componentManager,
                commitManager);

            string testOutput = "divideLine\n" +
                "asdasdasd234234sedfdsf2323aewas23\n" +
                "Bruce Lee\n" +
                "05/02/2018\n" +
                "Changes needed\n" +
                "smallLine\n" +
                "33      3       Star/Wars//Ilike.txt\n" +
                "1       2       Common/Knowledge/Start/WhenReady.txt\n" +
                "1       1       Common/Knowledge/Start/Abort.txt\n" +
                "62      5       Computer/IsSlow/Why/Google.txt\n";

            reportCreator.CreateFullReport(testOutput);

            Assert.Equal(2, componentManager
                ["asdasdasd234234sedfdsf2323aewas23Common.Knowledge"].InsertionCounter);
            Assert.Equal(3, componentManager
                ["asdasdasd234234sedfdsf2323aewas23Common.Knowledge"].DeletionCounter);
            Assert.True(componentManager.ContainsKey
                ("asdasdasd234234sedfdsf2323aewas23Common.Knowledge"));
        }
    }
}
