namespace StevenVolckaert.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class IEnumerableExtensionsTests
    {
        [Fact]
        public void AppendTest()
        {
            var source = new string[] { "foo", "bar" };
            var expected = new string[] { "foo", "bar", "baz" };
            var actual = source.Append("baz").ToArray();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PrependTest()
        {
            var source = new string[] { "bar", "baz" };
            var expected = new string[] { "foo", "bar", "baz" };
            var actual = source.Prepend("foo").ToArray();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ExceptTest()
        {
            var source = new string[] { "foo", "bar", "baz" };

            var actual = source.Except("xxx");
            Assert.True(actual.Count() == 3);
            Assert.DoesNotContain("xxx", collection: actual);

            foreach (var item in source)
            {
                actual = source.Except(item);
                Assert.True(actual.Count() == 2);
                Assert.DoesNotContain(item, collection: actual);
            }
        }

        [Fact]
        public void IsSubsetOf_ReturnsTrue()
        {
            var source = new string[] { "foo", "bar", "baz" };
            var other = new string[] { "x", "baz", "1", "bar", "foo", "y", "something", "z", "else" };
            Assert.True(source.IsSubsetOf(other));
        }

        [Fact]
        public void IsSubsetOf_ReturnsFalse()
        {
            var source = new string[] { "foo", "bar", "baz" };
            var other = new string[] { "x", "bax", "1", "bar", "foo", "y", "something", "z", "else" };
            Assert.False(source.IsSubsetOf(other));
        }

        [Fact]
        public void IsEmpty_ReturnsTrue()
        {
            var source = new string[] { };
            Assert.True(source.IsEmpty());
        }

        [Fact]
        public void IsEmpty_ReturnsFalse()
        {
            var source = new string[] { "foo" };
            Assert.False(source.IsEmpty());
        }

        [Fact]
        public void IsEmpty_ThrowsArgumentNullException()
        {
            IEnumerable<string> source = null;
            Assert.Throws<ArgumentNullException>(() => source.IsEmpty());
        }

        [Fact]
        public void IsNullOrEmpty_ReturnsTrue()
        {
            IEnumerable<string> source = null;
            Assert.True(source.IsNullOrEmpty());

            source = Enumerable.Empty<string>();
            Assert.True(source.IsNullOrEmpty());
        }

        [Fact]
        public void IsNullOrEmpty_ReturnsFalse()
        {
            IEnumerable<string> source = new string[] { "foo" };
            Assert.False(source.IsNullOrEmpty());
        }

        [Fact]
        public void OrderByOrdinal_ThrowsImmediatelyWhenArgumentIsNull()
        {
            IEnumerable<string> source = null;
            Assert.Throws<ArgumentNullException>(() => source.OrderByOrdinal(x => x));
        }

        [Fact]
        public void OrderByOrdinal_ThrowsImmediatelyWhenKeySelectorIsNull()
        {
            var source = new string[] { "foo 11", "baz 11", "foo", "bar", "baz", "foo 1", "bar 20", "foo 10", "bar 1", "baz 2" };

            Assert.Throws<ArgumentNullException>(() => source.OrderByOrdinal(null));
        }

        [Fact]
        public void OrderByOrdinalTest()
        {
            var source = new string[] { "foo 11", "baz 11", "foo", "bar", "baz", "foo 1", "bar 20", "foo 10", "bar 1", "baz 2" };
            var expected = new string[] { "bar", "bar 1", "bar 20", "baz", "baz 2", "baz 11", "foo", "foo 1", "foo 10", "foo 11" };
            var actual = source.OrderByOrdinal(x => x).ToArray();

            Assert.Equal(expected, actual);

            source = new string[] { "1", "10", "100", "101", "102", "11", "12", "13", "14", "15", "16", "17", "18", "19", "2", "20", "3", "4", "5", "6", "7", "8", "9" };
            expected = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "100", "101", "102" };
            actual = source.OrderByOrdinal(x => x).ToArray();

            Assert.Equal(expected, actual);

            source = new string[] { "File 1.txt", "File 10.txt", "File 11.csv", "File 2.jpg", "File 20.xls", "File 21.ppt", "File 3.doc" };
            expected = new string[] { "File 1.txt", "File 2.jpg", "File 3.doc", "File 10.txt", "File 11.csv", "File 20.xls", "File 21.ppt" };
            actual = source.OrderByOrdinal(x => x).ToArray();

            Assert.Equal(expected, actual);

            // Verify case-sensitive ordering.
            source = new string[] { "File 1.txt", "file 10.txt", "File 11.csv", "file 2.jpg", "File 20.xls", "File 21.ppt", "File 3.doc" };
            expected = new string[] { "file 2.jpg", "file 10.txt", "File 1.txt", "File 3.doc", "File 11.csv", "File 20.xls", "File 21.ppt" };
            actual = source.OrderByOrdinal(x => x).ToArray();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OrderByOrdinalCaseInsensitiveTest()
        {
            var source = new string[] { "File 1.txt", "file 10.txt", "File 11.csv", "file 2.jpg", "file 20.xls", "File 21.ppt", "File 3.doc" };
            var expected = new string[] { "File 1.txt", "file 2.jpg", "File 3.doc", "file 10.txt", "File 11.csv", "file 20.xls", "File 21.ppt" };
            var actual = source.OrderByOrdinal(x => x, ignoreCase: true).ToArray();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OrderByOrdinalDescendingTest()
        {
            var source = new string[] { "foo 11", "baz 11", "foo", "bar", "baz", "foo 1", "bar 20", "foo 10", "bar 1", "baz 2" };
            var expected = new string[] { "foo 11", "foo 10", "foo 1", "foo", "baz 11", "baz 2", "baz", "bar 20", "bar 1", "bar" };
            var actual = source.OrderByOrdinalDescending(x => x).ToArray();

            Assert.Equal(expected, actual);

            source = new string[] { "1", "10", "100", "101", "102", "11", "12", "13", "14", "15", "16", "17", "18", "19", "2", "20", "3", "4", "5", "6", "7", "8", "9" };
            expected = new string[] { "102", "101", "100", "20", "19", "18", "17", "16", "15", "14", "13", "12", "11", "10", "9", "8", "7", "6", "5", "4", "3", "2", "1" };
            actual = source.OrderByOrdinalDescending(x => x).ToArray();

            Assert.Equal(expected, actual);

            source = new string[] { "File 1.txt", "File 10.txt", "File 11.csv", "File 2.jpg", "File 20.xls", "File 21.ppt", "File 3.doc" };
            expected = new string[] { "File 21.ppt", "File 20.xls", "File 11.csv", "File 10.txt", "File 3.doc", "File 2.jpg", "File 1.txt" };
            actual = source.OrderByOrdinalDescending(x => x).ToArray();

            Assert.Equal(expected, actual);

            // Verify case-sensitive ordering.
            source = new string[] { "File 1.txt", "file 10.txt", "File 11.csv", "file 2.jpg", "File 20.xls", "File 21.ppt", "File 3.doc" };
            expected = new string[] { "File 21.ppt", "File 20.xls", "File 11.csv", "File 3.doc", "File 1.txt", "file 10.txt", "file 2.jpg" };
            actual = source.OrderByOrdinalDescending(x => x).ToArray();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OrderByOrdinalDescendingCaseInsensitiveTest()
        {
            var source = new string[] { "File 1.txt", "file 10.txt", "File 11.csv", "file 2.jpg", "file 20.xls", "File 21.ppt", "File 3.doc" };
            var expected = new string[] { "File 21.ppt", "file 20.xls", "File 11.csv", "file 10.txt", "File 3.doc", "file 2.jpg", "File 1.txt" };
            var actual = source.OrderByOrdinalDescending(x => x, ignoreCase: true).ToArray();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToObservableCollectionTest_ThrowsImmediatelyWhenArgumentIsNull()
        {
            string[] source = null;

            Assert.Throws<ArgumentNullException>(
                () => source.ToObservableCollection()
            );
        }

        [Fact]
        public void ToStringTest()
        {
            var source = new string[] { "File 1.txt", "file 10.txt", "File 11.csv" };
            var expected = "File 1.txt; file 10.txt; File 11.csv";
            var actual = source.ToString(separator: "; ");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UnionTest()
        {
            var subject = new string[] { "foo", "bar" };
            var actual = subject.Union("foo").Union("baz");
            Assert.True(actual.Count() == 3);
            Assert.Contains("baz", actual);
        }
    }
}
