using System;
using System.Collections.Generic;

namespace Vector
{
    // Interface for sorting functionality
    public interface ISorter
    {
        void Sort<K>(K[] sequence, IComparer<K> comparer) where K : IComparable<K>;
    }

    // Implementation of the ISorter interface using Array.Sort
    public class ArraySorter : ISorter
    {
        public void Sort<K>(K[] sequence, IComparer<K> comparer) where K : IComparable<K>
        {
            Array.Sort(sequence, comparer);
        }
    }
}
