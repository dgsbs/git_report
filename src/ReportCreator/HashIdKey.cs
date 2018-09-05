using System;

namespace ReportCreator
{
    public class HashIdKey : IEquatable<HashIdKey>
    {
        public string CommitHash { get; set; }
        public string ComponentId { get; set; }

        public bool Equals(HashIdKey otherKey)
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
            var key = obj as HashIdKey;
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
            return base.GetHashCode();
        }
    }
}
