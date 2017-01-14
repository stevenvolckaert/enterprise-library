namespace StevenVolckaert.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class StringExtensionsTests
    {
        public enum MockedEnumeration
        {
            Foo,
            Bar
        }

        [TestMethod]
        public void ParseAs_Succeeds()
        {
            Assert.AreEqual(MockedEnumeration.Foo, "Foo".ParseAs<MockedEnumeration>());
            Assert.AreEqual(MockedEnumeration.Bar, "Bar".ParseAs<MockedEnumeration>());
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void ParseAs_ThrowsArgumentException()
        {
            // "Baz" is not en enumeration value of MockedEnumeration.
            "Baz".ParseAs<MockedEnumeration>();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ParseAs_ThrowsArgumentNullException()
        {
            string str = null;
            str.ParseAs<MockedEnumeration>();
        }

        [TestMethod]
        public void ParseAsWhileIgnoringCase_Succeeds()
        {
            Assert.AreEqual(MockedEnumeration.Foo, "foo".ParseAs<MockedEnumeration>(ignoreCase: true));
            Assert.AreEqual(MockedEnumeration.Bar, "bar".ParseAs<MockedEnumeration>(ignoreCase: true));
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void ParseAsWhileIgnoringCase_ThrowsArgumentException()
        {
            "baz".ParseAs<MockedEnumeration>(ignoreCase: true);
        }

        [TestMethod]
        public void TryParseAs_Succeeds()
        {
            Assert.AreEqual(MockedEnumeration.Foo, "Foo".TryParseAs(MockedEnumeration.Foo));
            Assert.AreEqual(MockedEnumeration.Bar, "Bar".TryParseAs(MockedEnumeration.Foo));
            Assert.AreEqual(MockedEnumeration.Foo, "Baz".TryParseAs(MockedEnumeration.Foo));

            string str = null;
            Assert.AreEqual(MockedEnumeration.Bar, str.TryParseAs(MockedEnumeration.Bar));
        }

        [TestMethod]
        public void TryParseAsWhileIgnoringCase_Succeeds()
        {
            Assert.AreEqual(MockedEnumeration.Foo, "foo".TryParseAs(MockedEnumeration.Foo, ignoreCase: true));
            Assert.AreEqual(MockedEnumeration.Bar, "bar".TryParseAs(MockedEnumeration.Foo, ignoreCase: true));
            Assert.AreEqual(MockedEnumeration.Foo, "baz".TryParseAs(MockedEnumeration.Foo, ignoreCase: true));
        }

        [TestMethod]
        public void TryTrimTest()
        {
            Assert.AreEqual("Foo", " Foo    ".TryTrim());
            Assert.AreEqual("Bar", "Bar".TryTrim());
            Assert.AreEqual(null, ((String)null).TryTrim());
        }
    }
}
