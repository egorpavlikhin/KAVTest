using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ploeh.AutoFixture;
using Xunit;

namespace KAVTest
{
    public class CollectionFinderTests
    {
        private Fixture fixture = new Fixture();

        [Theory]
        [InlineData(new object[] { 1, 2, 3, 4, 5, 6 }, 10)]
        public void CanFindPairsWithSumSpefic(object[] collectionInts, int sum)
        {
            IEnumerable<int> collection = collectionInts.Cast<int>();

            var result = CollectionFinder.FindPairsWithSum(sum, collection);
            Assert.Equal(new Tuple<int, int>(4, 6), result[0]);
            Assert.Equal(1, result.Count);
        }

        [Theory]
        [InlineData(new object[] { 1, 2, 3, 4, 5, 6 }, 10, 1)]
        [InlineData(new object[] { 1, 2, 3, 4, 5, 6, 7, 8 }, 10, 3)]
        [InlineData(new object[] { 1, 2, 3, 4, 5, 6 }, 4, 1)]
        [InlineData(new object[] { 1, 2, 3, 4, 5, 6 }, 6, 2)]
        [InlineData(new object[] { }, 11, 0)]
        public void CanFindPairsWithSumGeneral(object[] collectionInts, int sum, int expectedCount)
        {
            IEnumerable<int> collection = collectionInts.Cast<int>();

            var result = CollectionFinder.FindPairsWithSum(sum, collection);
            Assert.Equal(expectedCount, result.Count);
        }
    }
}