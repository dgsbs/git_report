using System;
using System.Collections.Generic;
namespace GitReport.CLI
{
    class GitDiffFinalOutputBuilder
    {
        Dictionary<string, DictionaryArgsForGitDiff> dictionaryManager = 
            new Dictionary<string, DictionaryArgsForGitDiff>();
        public GitDiffFinalOutputBuilder(Dictionary<string, 
            DictionaryArgsForGitDiff> dictionaryManager)
        {
            this.dictionaryManager = dictionaryManager;
        }
        public void ManageDataFromGitDiff(string oneLineFormStdOut)
        {
            var componentNewId = string.Empty;
            string[] separateGitDiffOutput = oneLineFormStdOut.Split(new char[] { ' ', '\t' });

            var addedChanges = CheckIfStringIsNumber(separateGitDiffOutput[0]);
            var removedChanges = CheckIfStringIsNumber(separateGitDiffOutput[1]);
            DictionaryArgsForGitDiff DictionaryArg = 
                new DictionaryArgsForGitDiff(addedChanges,removedChanges);

            JsonConfigManager JsonDataManager = new JsonConfigManager();

            if (JsonDataManager.CheckIfPathMatchesPathsInJson(separateGitDiffOutput[2],out componentNewId))
            {
                if (CheckIfObjectExists(componentNewId, separateGitDiffOutput)) { }
                else
                {
                    dictionaryManager.Add(componentNewId, DictionaryArg);
                }
            }
        }
        public void ShowGitDiffDictionary()             
        {
            foreach (var dictionaryItem in dictionaryManager)
            {
                Console.WriteLine("Component id: {0}\nCode added in component: " +
                    "{1}\nCode removed in component: {2}\n", dictionaryItem.Key,
                    dictionaryItem.Value.ChangesAdded, dictionaryItem.Value.ChangesRemoved);
            }
        }
        private bool CheckIfObjectExists(string temporaryKey, string[] separateGitDiffOutput)
        {
            foreach (var dictionaryItem in dictionaryManager)
            {
                if(dictionaryManager.ContainsKey(temporaryKey))
                {
                    dictionaryItem.Value.ChangesAdded += Int32.Parse(separateGitDiffOutput[0]);
                    dictionaryItem.Value.ChangesRemoved += Int32.Parse(separateGitDiffOutput[1]);
                    return true;
                }
            }
            return false;
        }
        private int CheckIfStringIsNumber(string numberOfChanges)
        {
            int tempForFormatCheck = 0;
            if (Int32.TryParse(numberOfChanges,out tempForFormatCheck))
            {
                return tempForFormatCheck;
            }
            else
            {
                return tempForFormatCheck;
            }
        }
    }
}
