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
        public void ManageDataFromGitDiff(string oneLineFromStdOut)
        {
            var componentNewId = string.Empty;
            string[] separatedGitDiffOutput = oneLineFromStdOut.Split(new char[] { ' ', '\t' });

            var addedChanges = CheckIfStringIsNumber(separatedGitDiffOutput[0]);
            var removedChanges = CheckIfStringIsNumber(separatedGitDiffOutput[1]);
            DictionaryArgsForGitDiff DictionaryArg = 
                new DictionaryArgsForGitDiff(addedChanges,removedChanges);

            JsonConfigManager jsonDataManager = new JsonConfigManager();

            if (jsonDataManager.CheckIfPathMatchesPathsInJson
                (separatedGitDiffOutput[2],out componentNewId))
            {
                if (CheckIfObjectExists(componentNewId, separatedGitDiffOutput)) { }
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
        private bool CheckIfObjectExists(string temporaryKey, string[] separatedGitDiffOutput)
        {
            foreach (var dictionaryItem in dictionaryManager)
            {
                if(dictionaryManager.ContainsKey(temporaryKey))
                {
                    dictionaryItem.Value.ChangesAdded += Int32.Parse(separatedGitDiffOutput[0]);
                    dictionaryItem.Value.ChangesRemoved += Int32.Parse(separatedGitDiffOutput[1]);
                    return true;
                }
            }
            return false;
        }
        private int CheckIfStringIsNumber(string numberOfChanges)
        {
            if (Int32.TryParse(numberOfChanges, out int tempForFormatCheck))
            {
                return tempForFormatCheck;
            }
            else
            {
                return 0;
            }
        }
    }
}
