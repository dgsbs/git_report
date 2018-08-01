namespace GitReport.CLI
{
    class DictionaryArgsForGitDiff
    {
        public DictionaryArgsForGitDiff(int changesAdded, int changesRemoved)
        {
            this.ChangesAdded = changesAdded;
            this.ChangesRemoved = changesRemoved;
        }
        public int ChangesAdded { get; set; }
        public int ChangesRemoved { get; set; }
    }
}
