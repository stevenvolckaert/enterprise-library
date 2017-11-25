namespace StevenVolckaert.Tests
{
    using System.Collections.Generic;
    using Xunit;

    public class ICollectionExtensionsTests
    {
        [Fact]
        public void AddRangeTest()
        {
            var subject = new List<string> { "foo", "bar", "baz" };
            var other = new string[] { "bar", "foo", "qux" };
            var expected = new string[] { "foo", "bar", "baz", "bar", "foo", "qux" };

            subject.AddRange(other);

            Assert.Equal(expected, subject);
        }
            
        [Fact]
        public void AddRangeIndistinctTest()
        {
            var subject = new List<string> { "foo", "bar", "baz" };
            var other = new string[] { "bar", "foo", "qux" };
            var expected = new string[] { "foo", "bar", "baz", "bar", "foo", "qux" };

            subject.AddRange(other, isDistinct: false);

            Assert.Equal(expected, subject);
        }

        [Fact]
        public void AddRangeDistinctTest()
        {
            var subject = new List<string> { "foo", "bar", "baz" };
            var other = new string[] { "bar", "foo", "qux" };
            var expected = new string[] { "foo", "bar", "baz", "qux" };

            subject.AddRange(other, isDistinct: true);

            Assert.Equal(expected, subject);
        }
    }
}
