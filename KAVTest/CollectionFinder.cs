using System;
using System.Collections;
using System.Collections.Generic;

namespace KAVTest
{
    public static class CollectionFinder
    {
        public static IList<Tuple<int, int>> FindPairsWithSum(int sum, IEnumerable<int> collection)
        {
            var result = new List<Tuple<int, int>>();
            var compliments = new HashSet<int>();

            foreach (int item in collection)
            {
                if (compliments.Contains(item))
                {
                    result.Add(new Tuple<int, int>(sum - item, item));
                }

                compliments.Add(sum - item);
            }

            return result;
        }
    }
}