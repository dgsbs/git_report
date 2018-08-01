namespace GitReport.CLI
{
    class DictionaryArgsForGitDiff
    {
        public DictionaryArgsForGitDiff(int ChangesAdded, int ChangesRemoved)
        {
            this.ChangesAdded = ChangesAdded;
            this.ChangesRemoved = ChangesRemoved;
        }
        public int ChangesAdded { get; set; }
        public int ChangesRemoved { get; set; }
    }
}
