namespace StevenVolckaert.Tests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class IEnumerableExtensionsTests
    {
        [TestMethod]
        public void ExceptTest()
        {
            var source = new string[] { "foo", "bar", "baz" };

            var actual = source.Except("xxx");
            Assert.IsTrue(actual.Count() == 3);
            Assert.IsFalse(actual.Contains("xxx"));

            foreach (var item in source)
            {
                actual = source.Except(item);
                Assert.IsTrue(actual.Count() == 2);
                Assert.IsFalse(actual.Contains(item));
            }
        }

        [TestMethod]
        public void IsSubsetOf_ReturnsTrue()
        {
            var source = new string[] { "foo", "bar", "baz" };
            var other = new string[] { "x", "baz", "1", "bar", "foo", "y", "something", "z", "else" };
            Assert.IsTrue(source.IsSubsetOf(other));
        }

        [TestMethod]
        public void IsSubsetOf_ReturnsFalse()
        {
            var source = new string[] { "foo", "bar", "baz" };
            var other = new string[] { "x", "bax", "1", "bar", "foo", "y", "something", "z", "else" };
            Assert.IsFalse(source.IsSubsetOf(other));
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void OrderByOrdinal_ThrowsImmediatelyWhenArgumentIsNull()
        {
            string[] source = null;
            source.OrderByOrdinal(x => x);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void OrderByOrdinal_ThrowsImmediatelyWhenKeySelectorIsNull()
        {
            var source = new string[] { "foo 11", "baz 11", "foo", "bar", "baz", "foo 1", "bar 20", "foo 10", "bar 1", "baz 2" };
            source.OrderByOrdinal(null);
        }

        [TestMethod]
        public void OrderByOrdinalTest()
        {
            var source = new string[] { "foo 11", "baz 11", "foo", "bar", "baz", "foo 1", "bar 20", "foo 10", "bar 1", "baz 2" };
            var expected = new string[] { "bar", "bar 1", "bar 20", "baz", "baz 2", "baz 11", "foo", "foo 1", "foo 10", "foo 11" };
            var actual = source.OrderByOrdinal(x => x).ToArray();

            CollectionAssert.AreEqual(expected, actual);

            source = new string[] { "1", "10", "100", "101", "102", "11", "12", "13", "14", "15", "16", "17", "18", "19", "2", "20", "3", "4", "5", "6", "7", "8", "9" };
            expected = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "100", "101", "102" };
            actual = source.OrderByOrdinal(x => x).ToArray();

            CollectionAssert.AreEqual(expected, actual);

            source = new string[] { "File 1.txt", "File 10.txt", "File 11.csv", "File 2.jpg", "File 20.xls", "File 21.ppt", "File 3.doc" };
            expected = new string[] { "File 1.txt", "File 2.jpg", "File 3.doc", "File 10.txt", "File 11.csv", "File 20.xls", "File 21.ppt" };
            actual = source.OrderByOrdinal(x => x).ToArray();

            CollectionAssert.AreEqual(expected, actual);

            // Verify case-sensitive ordering.
            source = new string[] { "File 1.txt", "file 10.txt", "File 11.csv", "file 2.jpg", "File 20.xls", "File 21.ppt", "File 3.doc" };
            expected = new string[] { "file 2.jpg", "file 10.txt", "File 1.txt", "File 3.doc", "File 11.csv", "File 20.xls", "File 21.ppt" };
            actual = source.OrderByOrdinal(x => x).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void OrderByOrdinalCaseInsensitiveTest()
        {
            var source = new string[] { "File 1.txt", "file 10.txt", "File 11.csv", "file 2.jpg", "file 20.xls", "File 21.ppt", "File 3.doc" };
            var expected = new string[] { "File 1.txt", "file 2.jpg", "File 3.doc", "file 10.txt", "File 11.csv", "file 20.xls", "File 21.ppt" };
            var actual = source.OrderByOrdinal(x => x, ignoreCase: true).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void OrderByOrdinalDescendingTest()
        {
            var source = new string[] { "foo 11", "baz 11", "foo", "bar", "baz", "foo 1", "bar 20", "foo 10", "bar 1", "baz 2" };
            var expected = new string[] { "foo 11", "foo 10", "foo 1", "foo", "baz 11", "baz 2", "baz", "bar 20", "bar 1", "bar" };
            var actual = source.OrderByOrdinalDescending(x => x).ToArray();

            CollectionAssert.AreEqual(expected, actual);

            source = new string[] { "1", "10", "100", "101", "102", "11", "12", "13", "14", "15", "16", "17", "18", "19", "2", "20", "3", "4", "5", "6", "7", "8", "9" };
            expected = new string[] { "102", "101", "100", "20", "19", "18", "17", "16", "15", "14", "13", "12", "11", "10", "9", "8", "7", "6", "5", "4", "3", "2", "1" };
            actual = source.OrderByOrdinalDescending(x => x).ToArray();

            CollectionAssert.AreEqual(expected, actual);

            source = new string[] { "File 1.txt", "File 10.txt", "File 11.csv", "File 2.jpg", "File 20.xls", "File 21.ppt", "File 3.doc" };
            expected = new string[] { "File 21.ppt", "File 20.xls", "File 11.csv", "File 10.txt", "File 3.doc", "File 2.jpg", "File 1.txt" };
            actual = source.OrderByOrdinalDescending(x => x).ToArray();

            CollectionAssert.AreEqual(expected, actual);

            // Verify case-sensitive ordering.
            source = new string[] { "File 1.txt", "file 10.txt", "File 11.csv", "file 2.jpg", "File 20.xls", "File 21.ppt", "File 3.doc" };
            expected = new string[] { "File 21.ppt", "File 20.xls", "File 11.csv", "File 3.doc", "File 1.txt", "file 10.txt", "file 2.jpg" };
            actual = source.OrderByOrdinalDescending(x => x).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void OrderByOrdinalDescendingCaseInsensitiveTest()
        {
            var source = new string[] { "File 1.txt", "file 10.txt", "File 11.csv", "file 2.jpg", "file 20.xls", "File 21.ppt", "File 3.doc" };
            var expected = new string[] { "File 21.ppt", "file 20.xls", "File 11.csv", "file 10.txt", "File 3.doc", "file 2.jpg", "File 1.txt" };
            var actual = source.OrderByOrdinalDescending(x => x, ignoreCase: true).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ToObservableCollectionTest_ThrowsImmediatelyWhenArgumentIsNull()
        {
            string[] source = null;
            var collection = source.ToObservableCollection();
        }

        [TestMethod]
        public void ToStringTest()
        {
            var source = new string[] { "File 1.txt", "file 10.txt", "File 11.csv" };
            var expected = "File 1.txt; file 10.txt; File 11.csv";
            var actual = source.ToString(separator: "; ");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UnionTest()
        {
            var source = new string[] { "foo", "bar" };
            var actual = source.Union("foo").Union("baz");
            Assert.IsTrue(actual.Count() == 3);
            Assert.IsTrue(actual.Contains("baz"));
        }
    }
}
