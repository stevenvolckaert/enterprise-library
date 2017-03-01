namespace StevenVolckaert.Tests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void IsGuidTest()
        {
            var validGuidStrings = new List<string>
            {
                // Lower case, no dashes, no braces.
                "dfdc04eb167941cd8e23ef66d6cc756e",
                // Upper case, no dashes, no braces.
                "DFDC04EB167941CD8E23EF66D6CC756E",
                // lower case, with dashes, no braces.
                "dfdc04eb-1679-41cd-8e23-ef66d6cc756e",
                // Upper case, with dashes, no braces.
                "DFDC04EB-1679-41CD-8E23-EF66D6CC756E",
                // Lower case, with dashes, with braces.
                "{dfdc04eb-1679-41cd-8e23-ef66d6cc756e}",
                // Upper case, with dashes, with braces.
                "{DFDC04EB-1679-41CD-8E23-EF66D6CC756E}",
            };

            validGuidStrings.ForEach(
                x => Assert.IsTrue(x.IsGuid(), $"Expected string value '{x}' to be a valid GUID.")
            );

            var invalidGuidStrings = new List<string>
            {
                // Contains invalid character ('g').
                "gfdc04eb167941cd8e23ef66d6cc756e",
                // Not enough characters.
                "FDC04EB167941CD8E23EF66D6CC756E",
                // Not enough characters.
                "{dfdc04eb167941cd8e23ef66d6cc756}",
                // Dash in the wrong place.
                "DFDC04EB-16794-1CD-8E23EF66D6CC756E",
                // A random string.
                "Hello world!",
            };

            invalidGuidStrings.ForEach(
                x => Assert.IsFalse(x.IsGuid(), $"Expected string value '{x}' to be an invalid GUID.")
            );
        }

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
