using System;

namespace GitCounter
{
    public class GitDiffArguments
    {
        public string GitPath { get; set;}
        public DateTime DateSince { get; set;}
        public DateTime DateBefore { get; set;}
    }
}
