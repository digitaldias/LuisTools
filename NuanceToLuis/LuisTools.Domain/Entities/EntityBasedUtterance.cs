using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LuisTools.Domain.Entities
{
    [DebuggerDisplay("{EntityStrippedText}")]
    public class EntityBasedUtterance : IComparable
    {
        public string SearchableText { get; set; }

        public string EntityMarkedText { get; set; }

        public string EntityStrippedText { get; set; }

        public int CompareTo(object other)
        {
            var theOther = (EntityBasedUtterance)other;
            return this.EntityStrippedText.CompareTo(theOther.EntityStrippedText);
        }

        public override int GetHashCode()
        {
            return EntityStrippedText.GetHashCode();
        }
    }

    public class EntityBasedUtteranceComparer : IEqualityComparer<EntityBasedUtterance>
    {
        public bool Equals(EntityBasedUtterance x, EntityBasedUtterance y)
        {
            return x.EntityStrippedText.Equals(y.EntityStrippedText);
        }

        public int GetHashCode(EntityBasedUtterance obj)
        {
            return obj.EntityStrippedText.GetHashCode();
        }
    }
}
