namespace StevenVolckaert.Tests
{
    using System.Collections.Generic;
    using Xunit;

    public class ICollectionExtensionsTests
    {
        [Fact]
        public void AddRangeTest()
        {
            var source = new List<string> { "foo", "bar", "baz" };
            var other = new string[] { "bar", "foo", "qux" };
            var expected = new string[] { "foo", "bar", "baz", "bar", "foo", "qux" };

            source.AddRange(other);

            Assert.Equal(expected, source);
        }
            
        [Fact]
        public void AddRangeIndistinctTest()
        {
            var source = new List<string> { "foo", "bar", "baz" };
            var other = new string[] { "bar", "foo", "qux" };
            var expected = new string[] { "foo", "bar", "baz", "bar", "foo", "qux" };

            source.AddRange(other, isDistinct: false);

            Assert.Equal(expected, source);
        }

        [Fact]
        public void AddRangeDistinctTest()
        {
            var source = new List<string> { "foo", "bar", "baz" };
            var other = new string[] { "bar", "foo", "qux" };
            var expected = new string[] { "foo", "bar", "baz", "qux" };

            source.AddRange(other, isDistinct: true);

            Assert.Equal(expected, source);
        }
    }
}
