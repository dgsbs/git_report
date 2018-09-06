using System;

namespace ReportCreator
{
    public class HashId : IEquatable<HashId>
    {
        public string CommitHash { get; set; }
        public string ComponentId { get; set; }

        public bool Equals(HashId otherKey)
        {
            if (otherKey == null)
            {
                return false;
            }
            if (CommitHash == otherKey.CommitHash &&
                ComponentId == otherKey.ComponentId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override bool Equals(object obj)
        {
            var key = obj as HashId;
            if (key != null)
            {
                return Equals(key);
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return 17 * 23 + CommitHash.GetHashCode() ^ 17 * 23 + ComponentId.GetHashCode(); ;
        }
    }
}
