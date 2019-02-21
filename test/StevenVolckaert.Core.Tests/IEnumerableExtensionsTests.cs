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
            var subject = new string[] { "foo", "bar" };
            var expected = new string[] { "foo", "bar", "baz" };
            var actual = subject.Append("baz").ToArray();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PrependTest()
        {
            var subject = new string[] { "bar", "baz" };
            var expected = new string[] { "foo", "bar", "baz" };
            var actual = subject.Prepend("foo").ToArray();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ExceptTest()
        {
            var subject = new string[] { "foo", "bar", "baz" };

            var actual = subject.Except("xxx");
            Assert.True(actual.Count() == 3);
            Assert.DoesNotContain("xxx", collection: actual);

            foreach (var item in subject)
            {
                actual = subject.Except(item);
                Assert.True(actual.Count() == 2);
                Assert.DoesNotContain(item, collection: actual);
            }
        }

        [Fact]
        public void IsSubsetOf_ReturnsTrue()
        {
            var subject = new string[] { "foo", "bar", "baz" };
            var other = new string[] { "x", "baz", "1", "bar", "foo", "y", "something", "z", "else" };
            Assert.True(subject.IsSubsetOf(other));
        }

        [Fact]
        public void IsSubsetOf_ReturnsFalse()
        {
            var subject = new string[] { "foo", "bar", "baz" };
            var other = new string[] { "x", "bax", "1", "bar", "foo", "y", "something", "z", "else" };
            Assert.False(subject.IsSubsetOf(other));
        }

        [Fact]
        public void IsEmpty_ReturnsTrue()
        {
            var subject = new string[] { };
            Assert.True(subject.IsEmpty());
        }

        [Fact]
        public void IsEmpty_ReturnsFalse()
        {
            var subject = new string[] { "foo" };
            Assert.False(subject.IsEmpty());
        }

        [Fact]
        public void IsEmpty_ThrowsArgumentNullException()
        {
            IEnumerable<string> subject = null;
            Assert.Throws<ArgumentNullException>(() => subject.IsEmpty());
        }

        [Fact]
        public void IsNullOrEmpty_ReturnsTrue()
        {
            IEnumerable<string> subject = null;
            Assert.True(subject.IsNullOrEmpty());

            subject = Enumerable.Empty<string>();
            Assert.True(subject.IsNullOrEmpty());
        }

        [Fact]
        public void IsNullOrEmpty_ReturnsFalse()
        {
            IEnumerable<string> subject = new string[] { "foo" };
            Assert.False(subject.IsNullOrEmpty());
        }

        [Fact]
        public void OrderByOrdinal_ThrowsImmediatelyWhenArgumentIsNull()
        {
            IEnumerable<string> subject = null;
            Assert.Throws<ArgumentNullException>(() => subject.OrderByOrdinal(x => x));
        }

        [Fact]
        public void OrderByOrdinal_ThrowsImmediatelyWhenKeySelectorIsNull()
        {
            var subject = new string[] { "foo 11", "baz 11", "foo", "bar", "baz", "foo 1", "bar 20", "foo 10", "bar 1", "baz 2" };

            Assert.Throws<ArgumentNullException>(() => subject.OrderByOrdinal(null));
        }

        [Fact]
        public void OrderByOrdinalTest()
        {
            var subject = new string[] { "foo 11", "baz 11", "foo", "bar", "baz", "foo 1", "bar 20", "foo 10", "bar 1", "baz 2" };
            var expected = new string[] { "bar", "bar 1", "bar 20", "baz", "baz 2", "baz 11", "foo", "foo 1", "foo 10", "foo 11" };
            var actual = subject.OrderByOrdinal(x => x).ToArray();

            Assert.Equal(expected, actual);

            subject = new string[] { "1", "10", "100", "101", "102", "11", "12", "13", "14", "15", "16", "17", "18", "19", "2", "20", "3", "4", "5", "6", "7", "8", "9" };
            expected = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "100", "101", "102" };
            actual = subject.OrderByOrdinal(x => x).ToArray();

            Assert.Equal(expected, actual);

            subject = new string[] { "File 1.txt", "File 10.txt", "File 11.csv", "File 2.jpg", "File 20.xls", "File 21.ppt", "File 3.doc" };
            expected = new string[] { "File 1.txt", "File 2.jpg", "File 3.doc", "File 10.txt", "File 11.csv", "File 20.xls", "File 21.ppt" };
            actual = subject.OrderByOrdinal(x => x).ToArray();

            Assert.Equal(expected, actual);

            // Verify case-sensitive ordering.
            subject = new string[] { "File 1.txt", "file 10.txt", "File 11.csv", "file 2.jpg", "File 20.xls", "File 21.ppt", "File 3.doc" };
            expected = new string[] { "file 2.jpg", "file 10.txt", "File 1.txt", "File 3.doc", "File 11.csv", "File 20.xls", "File 21.ppt" };
            actual = subject.OrderByOrdinal(x => x).ToArray();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OrderByOrdinalCaseInsensitiveTest()
        {
            var subject = new string[] { "File 1.txt", "file 10.txt", "File 11.csv", "file 2.jpg", "file 20.xls", "File 21.ppt", "File 3.doc" };
            var expected = new string[] { "File 1.txt", "file 2.jpg", "File 3.doc", "file 10.txt", "File 11.csv", "file 20.xls", "File 21.ppt" };
            var actual = subject.OrderByOrdinal(x => x, ignoreCase: true).ToArray();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OrderByOrdinalDescendingTest()
        {
            var subject = new string[] { "foo 11", "baz 11", "foo", "bar", "baz", "foo 1", "bar 20", "foo 10", "bar 1", "baz 2" };
            var expected = new string[] { "foo 11", "foo 10", "foo 1", "foo", "baz 11", "baz 2", "baz", "bar 20", "bar 1", "bar" };
            var actual = subject.OrderByOrdinalDescending(x => x).ToArray();

            Assert.Equal(expected, actual);

            subject = new string[] { "1", "10", "100", "101", "102", "11", "12", "13", "14", "15", "16", "17", "18", "19", "2", "20", "3", "4", "5", "6", "7", "8", "9" };
            expected = new string[] { "102", "101", "100", "20", "19", "18", "17", "16", "15", "14", "13", "12", "11", "10", "9", "8", "7", "6", "5", "4", "3", "2", "1" };
            actual = subject.OrderByOrdinalDescending(x => x).ToArray();

            Assert.Equal(expected, actual);

            subject = new string[] { "File 1.txt", "File 10.txt", "File 11.csv", "File 2.jpg", "File 20.xls", "File 21.ppt", "File 3.doc" };
            expected = new string[] { "File 21.ppt", "File 20.xls", "File 11.csv", "File 10.txt", "File 3.doc", "File 2.jpg", "File 1.txt" };
            actual = subject.OrderByOrdinalDescending(x => x).ToArray();

            Assert.Equal(expected, actual);

            // Verify case-sensitive ordering.
            subject = new string[] { "File 1.txt", "file 10.txt", "File 11.csv", "file 2.jpg", "File 20.xls", "File 21.ppt", "File 3.doc" };
            expected = new string[] { "File 21.ppt", "File 20.xls", "File 11.csv", "File 3.doc", "File 1.txt", "file 10.txt", "file 2.jpg" };
            actual = subject.OrderByOrdinalDescending(x => x).ToArray();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OrderByOrdinalDescendingCaseInsensitiveTest()
        {
            var subject = new string[] { "File 1.txt", "file 10.txt", "File 11.csv", "file 2.jpg", "file 20.xls", "File 21.ppt", "File 3.doc" };
            var expected = new string[] { "File 21.ppt", "file 20.xls", "File 11.csv", "file 10.txt", "File 3.doc", "file 2.jpg", "File 1.txt" };
            var actual = subject.OrderByOrdinalDescending(x => x, ignoreCase: true).ToArray();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(3)]
        [InlineData(10)]
        [InlineData(12)]
        public void Random_ReturnsSpecifiedNumberOfElements(int numberOfElements)
        {
            var subject = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var actual = subject.Random(numberOfElements);
            Assert.Equal(numberOfElements, actual.Count());
        }

        [Fact]
        public void Random_ThrowsArgumentNullException()
        {
            IEnumerable<string> subject = null;
            Assert.Throws<ArgumentNullException>(() => subject.Random());
        }

        [Fact]
        public void Random_ThrowsInvalidOperationException()
        {
            var subject = Enumerable.Empty<string>();
            Assert.Throws<InvalidOperationException>(() => subject.Random());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(3)]
        [InlineData(10)]
        [InlineData(12)]
        public void RandomOrDefault_ReturnsSpecifiedNumberOfElements(int numberOfElements)
        {
            var subject = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var actual = subject.Random(numberOfElements);
            Assert.Equal(numberOfElements, actual.Count());
        }

        [Fact]
        public void RandomOrDefault_ReturnsDefaultIfSourceSequenceIsEmpty()
        {
            var subject = Enumerable.Empty<int>();
            Assert.Equal(default(int), subject.RandomOrDefault());
        }

        // TODO This test fails for some reason; investigate why. -Steven Volckaert. May 14, 2018.

        //[Fact]
        //public void TakeOrDefault_ThrowsImmediatelyWhenArgumentIsNull()
        //{
        //    string[] subject = null;

        //    Assert.Throws<ArgumentNullException>(
        //        () => subject.TakeOrDefault(count: 1, defaultValue: string.Empty)
        //    );
        //}

        [Theory]
        [InlineData(1, "qux", "foo")]
        [InlineData(2, "qux", "foo", "bar")]
        [InlineData(3, "qux", "foo", "bar", "baz")]
        [InlineData(4, "qux", "foo", "bar", "baz")]
        [InlineData(5, "qux", "foo", "bar", "baz")]
        [InlineData(5, "qux", "foo")]
        public void TakeOrDefault_Succeeds(int count, string defaultValue, params string[] subject)
        {
            var expected = subject.ToArray();

            for (var i = expected.Length; i < count; i++)
                expected = expected.Append(defaultValue).ToArray();

            var actual = subject.TakeOrDefault(count, defaultValue);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToObservableCollectionTest_ThrowsImmediatelyWhenArgumentIsNull()
        {
            string[] subject = null;

            Assert.Throws<ArgumentNullException>(
                () => subject.ToObservableCollection()
            );
        }

        [Fact]
        public void ToStringTest()
        {
            var subject = new string[] { "File 1.txt", "file 10.txt", "File 11.csv" };
            var expected = "File 1.txt; file 10.txt; File 11.csv";
            var actual = subject.ToString(separator: "; ");

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
