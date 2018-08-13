using System.Collections.Generic;
using Xunit;
using GitCounter;

namespace GitReport.Tests
{
    public class ReportCreatorTests
    {
        Dictionary<string, ModificationCounters> dictionaryManager =
           new Dictionary<string, ModificationCounters>();
        IJsonConfig jsonManager = new ReportCreatorTestsHelper();

        [Fact]
        public void CreateReport_ManyLinesFromProcess_ComponentEqualsToValuesFromDictionary()
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
        public void CreateReport_ManyLinesFromProcess_ManyComponentsEqualsToValuesFromDictionary()
        {
            ReportCreator reportCreator = new ReportCreator(jsonManager);
            string testOutput = "33      3       Star/Wars//Ilike.txt\n" +
                "1       2       Common/Knowledge/Start/WhenReady.txt\n" +
                "1       1       Common/Knowledge/Start/Abort.txt\n" +
                "62      5       Computer/IsSlow/Why/Google.txt\n " +
                "15      12      Computer/IsSlow/AskGoogle/yahoo.com";

            dictionaryManager = reportCreator.CreateReport(testOutput);

            Assert.Equal(2, dictionaryManager["Common.Knowledge"].InsertionCounter);
            Assert.Equal(3, dictionaryManager["Common.Knowledge"].DeletionCounter);
            Assert.Equal(15, dictionaryManager["Computer.Slow"].InsertionCounter);
            Assert.Equal(12, dictionaryManager["Computer.Slow"].DeletionCounter);
            Assert.Equal(2,dictionaryManager.Count);
        }

        [Fact]
        public void CreateReport_SingleLineFromProcess_ComponentEqualsToValuesFromDictionary()
        {
            ReportCreator reportCreator = new ReportCreator(jsonManager);
            string testOutput = "1       1       Common/Knowledge/Start/Abort.txt\n";

            dictionaryManager = reportCreator.CreateReport(testOutput);

            Assert.Equal(1, dictionaryManager["Common.Knowledge"].InsertionCounter);
            Assert.Equal(1, dictionaryManager["Common.Knowledge"].DeletionCounter);
            Assert.Single(dictionaryManager);
        }

        [Fact]
        public void CreateReport_EmptyLine_EqualsToEmptyDictionary()
        {
            ReportCreator reportCreator = new ReportCreator(jsonManager);
            string testOutput = "";

            dictionaryManager = reportCreator.CreateReport(testOutput);

            Assert.Empty(dictionaryManager);
        }

        [Fact]
        public void CreateReport_WrongInput_EqualsToEmptyDictionary()
        {
            ReportCreator reportCreator = new ReportCreator(jsonManager);
            string testOutput = "62      5       Computer/IsSlow/Why/Google.txt\n";

            dictionaryManager = reportCreator.CreateReport(testOutput);

            Assert.Empty(dictionaryManager);
        }
    }
}
