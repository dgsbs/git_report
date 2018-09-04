using System;

namespace ReportCreator
{
    public class ComponentKey : IEquatable<ComponentKey>
    {
        public string CommitHash { get; set; }
        public string ComponentId { get; set; }

        public bool Equals(ComponentKey otherKey)
        {
            if (otherKey == null)
            {
                return false;
            }
            if (this.CommitHash == otherKey.CommitHash &&
                this.ComponentId == otherKey.ComponentId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
