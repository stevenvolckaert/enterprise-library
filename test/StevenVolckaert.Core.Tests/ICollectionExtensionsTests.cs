namespace StevenVolckaert.Tests
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ICollectionExtensionsTests
    {
        [TestMethod]
        public void AddRangeTest()
        {
            var source = new List<string> { "foo", "bar", "baz" };
            var other = new List<string> { "bar", "foo", "qux" };
            var expected = new List<string> { "foo", "bar", "baz", "bar", "foo", "qux" };

            source.AddRange(other);

            CollectionAssert.AreEqual(expected, source);
        }
            
        [TestMethod]
        public void AddRangeIndistinctTest()
        {
            var source = new List<string> { "foo", "bar", "baz" };
            var other = new List<string> { "bar", "foo", "qux" };
            var expected = new List<string> { "foo", "bar", "baz", "bar", "foo", "qux" };

            source.AddRange(other, isDistinct: false);

            CollectionAssert.AreEqual(expected, source);
        }

        [TestMethod]
        public void AddRangeDistinctTest()
        {
            var source = new List<string> { "foo", "bar", "baz" };
            var other = new List<string> { "bar", "foo", "qux" };
            var expected = new List<string> { "foo", "bar", "baz", "qux" };

            source.AddRange(other, isDistinct: true);

            CollectionAssert.AreEqual(expected, source);
        }
    }
}
